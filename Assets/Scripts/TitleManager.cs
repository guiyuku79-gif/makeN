using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class TitleManager : MonoBehaviour
{

    [SerializeField] TMP_InputField inputField;
    // static変数を初期化する処理  
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]  
    private static void Clear()  
    {
        inputNumbers = new List<int>();

    }

    public static List<int> inputNumbers = new List<int>();
    public void StartRandomMode()
    {
        inputNumbers = new List<int>();
;
        SceneManager.LoadScene("MainScene");
    }

    public void StartDesignedMode()
    {

        List<string> strings = inputField.text.Split(" ").ToList();

        inputNumbers = new List<int>();

        foreach (string s in strings)
        {
            if (int.TryParse(s, out int value))
            {
                inputNumbers.Add(value);
            }
        } 

        if(inputNumbers.Count != 4) return;

        foreach (int i in inputNumbers)
        {
            if(i <= 0 || 10 <= i) return;
        }

        SceneManager.LoadScene("MainScene");
    }
}
