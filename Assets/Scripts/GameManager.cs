using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{

    private List<Fraction> initNumbers = new List<Fraction>{};

    [SerializeField] GameObject NumbersGameObject;
    [SerializeField] Vector3 initNumberPos;
    [SerializeField] Vector3 NumbersInterval;
    void Start()
    {
        for(int i = 0; i < 4; i++)
        {
            initNumbers.Add(new Fraction( Random.Range(1,10), 1));
        }

        CreateNumberPrefabs();
    }

    void CreateNumberPrefabs()
    {
        for(int i = 0;i< 4; i++)
        {
            GameObject numbersPrefab = Instantiate(NumbersGameObject,initNumberPos+ i * NumbersInterval,Quaternion.identity);
            numbersPrefab.GetComponent<NumPrefabController>().Init(i,initNumbers[i],this);
        }
    }
    

    void Update()
    {
        
    }
}
