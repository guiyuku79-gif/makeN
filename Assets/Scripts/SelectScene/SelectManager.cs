using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI inputText;

    public static SelectManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void AddSelectNumber(int num)
    {
        if(TitleManager.inputNumbers.Count <= 3)
        {
            TitleManager.inputNumbers.Add(num);
        }

        UpdateUI();
    }

    public void DeleteNumber()
    {
        if(TitleManager.inputNumbers.Count >= 1)
        {
            TitleManager.inputNumbers.RemoveAt(TitleManager.inputNumbers.Count-1);
        }

        UpdateUI();
    }

    public void Enter()
    {
        if(TitleManager.inputNumbers.Count == 4)
        {
            SceneManager.LoadScene("MainScene");
        }
    }

    void UpdateUI()
    {
        string text = "";

        for(int i = 0; i < TitleManager.inputNumbers.Count; i++)
        {
            text += TitleManager.inputNumbers[i] + " ";
        }

        inputText.text = text;
    }


}
