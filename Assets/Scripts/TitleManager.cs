using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    public static bool isDesignedMode = false;
    public void StartRandomMode()
    {
        inputNumbers = new List<int>();

        isDesignedMode = false;

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

        isDesignedMode = true;

        SceneManager.LoadScene("MainScene");
    }
}
