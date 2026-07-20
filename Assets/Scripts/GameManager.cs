using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
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

    [SerializeField] TextMeshProUGUI OrderUI;
    [SerializeField] TextMeshProUGUI EquationUI;

    //解答のUI用
    [SerializeField] Image AnswerImage;
    [SerializeField] TextMeshProUGUI AnswerUI;
    [SerializeField] Image WaitBarImage;

    bool isTimeOut;
    float leftTime;
    [SerializeField] float MaxTime = 180f;


    //現在の入力状況を示す
    public struct CurrentEquation
    {
        public int firstNumberIndex;
        public int secondNumberIndex;

        public string selectedOperator;
    }
    
    [NonSerialized] public CurrentEquation currentEquation;

    public event Action EquationChange;

    int targetNumber;
    string howToMakeAnswer;

    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }


    //現在の状態を保存
    void Start()
    {
        CreateNewQuestion();
        // currentEquation.firstNumberIndex = -1;
        // currentEquation.secondNumberIndex = -1;
        // currentEquation.selectedOperator = "";
        // leftTime = MaxTime;
        // isTimeOut = false;

        // InitNumbers.Clear();
        // for(int i = 0; i < 4; i++)
        // {
        //     int seed  = UnityEngine.Random.Range(1,10);
        //     InitNumbers.Add(seed);
        //     Numbers.Add(new Fraction(seed, 1));
        // }

        // CreateNumberPrefabs();



        // Fraction[] numbersForEvluator =
        // {
        //     new Fraction(InitNumbers[0],1),
        //     new Fraction(InitNumbers[1],1),
        //     new Fraction(InitNumbers[2],1),
        //     new Fraction(InitNumbers[3],1)
        // };

        // ExpressionSearcher expressionSearcher = new ExpressionSearcher();
        // (targetNumber,howToMakeAnswer) = expressionSearcher.Search(numbersForEvluator);
        // OrderUI.text = "Make" + targetNumber.ToString();
        // Debug.Log(howToMakeAnswer);


    }

    void Update()
    {
        if (!isTimeOut)
        {
            leftTime -= Time.deltaTime;
            WaitBarImage.fillAmount = leftTime / MaxTime;
            if(leftTime <= 0)
            {
                isTimeOut = true;
                AnswerImage.color = Color.white;

            }
        } 
    }

    void CreateNumberPrefabs()
    {
        for(int i = 0;i< 4; i++)
        {
            GameObject numbersPrefab = Instantiate(NumbersGameObject,initNumberPos+ i * NumbersInterval,Quaternion.identity);
            numbersPrefab.GetComponent<NumPrefabController>().Init(i,Numbers[i]);
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
        //secondの場所の分数を変更
        if(currentEquation.selectedOperator == "+") Numbers[currentEquation.secondNumberIndex] = Numbers[currentEquation.firstNumberIndex]+Numbers[currentEquation.secondNumberIndex];
        if(currentEquation.selectedOperator == "-") Numbers[currentEquation.secondNumberIndex] = Numbers[currentEquation.firstNumberIndex]-Numbers[currentEquation.secondNumberIndex];
        if(currentEquation.selectedOperator == "*") Numbers[currentEquation.secondNumberIndex] = Numbers[currentEquation.firstNumberIndex]*Numbers[currentEquation.secondNumberIndex];
        if(currentEquation.selectedOperator == "/") Numbers[currentEquation.secondNumberIndex] = Numbers[currentEquation.firstNumberIndex]/Numbers[currentEquation.secondNumberIndex];
        NumberObjects[currentEquation.secondNumberIndex].GetComponent<NumPrefabController>().UpdateFraction(Numbers[currentEquation.secondNumberIndex]);

        //firstの場所を削除
        GameObject obj = NumberObjects[currentEquation.firstNumberIndex];
        Destroy(obj);
        NumberObjects[currentEquation.firstNumberIndex] = null;
        Numbers[currentEquation.firstNumberIndex] = new Fraction(-9999,1);

        currentEquation.firstNumberIndex = -1;
        currentEquation.secondNumberIndex = -1;
        currentEquation.selectedOperator = "";

        EquationChange.Invoke();

        CheckCorrect();
        if( Numbers.Count == 1 && Numbers[0] == new Fraction(targetNumber, 1))
        {
        }

    }

    void CheckCorrect()
    {
        List<Fraction> restNumbers = new List<Fraction>{};
        for(int i = 3; i >= 0; i--)
        {
            if(Numbers[i] != new Fraction(-9999,1)) restNumbers.Add(Numbers[i]);
        }

        if(restNumbers.Count == 1 && restNumbers[0] == new Fraction(targetNumber, 1))
        {
            Debug.Log("正解!");
            EquationUI.text = "You can make " + targetNumber.ToString();            
        }

    }

    //計算をしたものを戻す
    public void Reset()
    {
        foreach(GameObject gameObject in NumberObjects)
        {
            Destroy(gameObject);
        }
        NumberObjects.Clear();
        Numbers.Clear();

        for(int i = 0; i < 4; i++)
        {
            Numbers.Add(new Fraction(InitNumbers[i], 1));
        }
        CreateNumberPrefabs();

        currentEquation.firstNumberIndex = -1;
        currentEquation.secondNumberIndex = -1;
        currentEquation.selectedOperator = "";

        EquationUI.text = "";

    }

    //新しい問題を作る
    public void CreateNewQuestion()
    {

        foreach( GameObject gameObject in NumberObjects)
        {
            Destroy(gameObject);
        }
        NumberObjects.Clear();

        currentEquation.firstNumberIndex = -1;
        currentEquation.secondNumberIndex = -1;
        currentEquation.selectedOperator = "";

        leftTime = MaxTime;
        isTimeOut = false;
        AnswerImage.color = new Color32(200,200,200,255);
        AnswerUI.text = "Answer";

        InitNumbers.Clear();
        Numbers.Clear();
        for(int i = 0; i < 4; i++)
        {
            int seed  = UnityEngine.Random.Range(1,10);
            InitNumbers.Add(seed);
            Numbers.Add(new Fraction(seed, 1));
        }

        CreateNumberPrefabs();



        Fraction[] numbersForEvluator =
        {
            new Fraction(InitNumbers[0],1),
            new Fraction(InitNumbers[1],1),
            new Fraction(InitNumbers[2],1),
            new Fraction(InitNumbers[3],1)
        };

        ExpressionSearcher expressionSearcher = new ExpressionSearcher();
        (targetNumber,howToMakeAnswer) = expressionSearcher.Search(numbersForEvluator);
        OrderUI.text = "Make" + targetNumber.ToString();
        Debug.Log(howToMakeAnswer);        
    }

    //一定時間後に答えを表示する
    public void DisplayAnswer()
    {
        if(!isTimeOut) return;

        AnswerUI.text = howToMakeAnswer;
    }
}
