using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
using TMPro;

public class GameManager : MonoBehaviour
{
    //保存用
    private List<int> InitNumbers = new List<int>{};
    private List<Fraction> Numbers = new List<Fraction>{};
    private List<GameObject> NumberObjects = new List<GameObject>();

    [SerializeField] GameObject NumbersGameObject;
    [SerializeField] Vector3 initNumberPos;
    [SerializeField] Vector3 NumbersInterval;

    [SerializeField] TextMeshProUGUI EquationUI;

    //現在の入力状況を示す
    public struct CurrentEquation
    {
        public int firstNumberIndex;
        public int secondNumberIndex;

        public string selectedOperator;
    }
    
    [NonSerialized] public CurrentEquation currentEquation;

    public event Action EquationChange;


    //現在の状態を保存
    void Start()
    {
        currentEquation.firstNumberIndex = -1;
        currentEquation.secondNumberIndex = -1;
        currentEquation.selectedOperator = "";
        for(int i = 0; i < 4; i++)
        {
            int seed  = UnityEngine.Random.Range(1,10);
            InitNumbers.Add(seed);
            Numbers.Add(new Fraction(seed, 1));
        }

        CreateNumberPrefabs();
    }

    void CreateNumberPrefabs()
    {
        for(int i = 0;i< 4; i++)
        {
            GameObject numbersPrefab = Instantiate(NumbersGameObject,initNumberPos+ i * NumbersInterval,Quaternion.identity);
            numbersPrefab.GetComponent<NumPrefabController>().Init(i,Numbers[i],this);
            NumberObjects.Add(numbersPrefab);
        }
    }
    
    public void InputOperatorProcess(string operatorName)
    {
        if(operatorName == currentEquation.selectedOperator)
        {
            currentEquation.selectedOperator = "";
        }
        else
        {
            currentEquation.selectedOperator = operatorName;
        }

        EquationChange.Invoke();
        UpdateEquationUI();
    }

    public void InputNumberProcess(int index)
    {
        if( currentEquation.firstNumberIndex == index)
        {
            currentEquation.firstNumberIndex = -1;
        }
        else if(currentEquation.secondNumberIndex == index)
        {
            currentEquation.secondNumberIndex = -1;
        }
        else if(currentEquation.firstNumberIndex == -1)
        {
            currentEquation.firstNumberIndex = index;
        }
        else if(currentEquation.secondNumberIndex == -1)
        {
            currentEquation.secondNumberIndex = index;
        }
        EquationChange.Invoke();
        UpdateEquationUI();        
    }

    void UpdateEquationUI()
    {
        string firstNumber = currentEquation.firstNumberIndex != -1 ? Numbers[currentEquation.firstNumberIndex].ToString() : "";
        string secondNumber = currentEquation.secondNumberIndex != -1 ? Numbers[currentEquation.secondNumberIndex].ToString() : "";
        EquationUI.text = firstNumber + " " + 
                          currentEquation.selectedOperator + " " + 
                          secondNumber; 

        if(firstNumber != "" && secondNumber != "" && currentEquation.selectedOperator != "")
        {
            Calculation();
        }
    }

    void Calculation()
    {

        //分数リストに新しいものを作成
        List<Fraction> newFractions = new List<Fraction>{};
        for( int i = 0; i < Numbers.Count; i++)
        {
            if( i != currentEquation.firstNumberIndex && i != currentEquation.secondNumberIndex)
            {
                newFractions.Add(new Fraction(Numbers[i].Numerator,Numbers[i].Denominator));
            }
        }
        if(currentEquation.selectedOperator == "+")newFractions.Add(Numbers[currentEquation.firstNumberIndex]+Numbers[currentEquation.secondNumberIndex]);
        if(currentEquation.selectedOperator == "-")newFractions.Add(Numbers[currentEquation.firstNumberIndex]-Numbers[currentEquation.secondNumberIndex]);
        if(currentEquation.selectedOperator == "*")newFractions.Add(Numbers[currentEquation.firstNumberIndex]*Numbers[currentEquation.secondNumberIndex]);
        if(currentEquation.selectedOperator == "/")newFractions.Add(Numbers[currentEquation.firstNumberIndex]/Numbers[currentEquation.secondNumberIndex]);


        //もともとあるのを削除

        foreach (GameObject obj in NumberObjects)
        {
            Destroy(obj);
        }
        NumberObjects.Clear();

        Numbers = newFractions;

        currentEquation.firstNumberIndex = -1;
        currentEquation.secondNumberIndex = -1;
        currentEquation.selectedOperator = "";

        for(int i = 0;i< Numbers.Count; i++)
        {
            GameObject numbersPrefab = Instantiate(NumbersGameObject,initNumberPos+ i * NumbersInterval,Quaternion.identity);
            numbersPrefab.GetComponent<NumPrefabController>().Init(i,Numbers[i],this);
            NumberObjects.Add(numbersPrefab);
        }

    }

}
