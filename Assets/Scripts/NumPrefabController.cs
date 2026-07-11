using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class NumPrefabController : MonoBehaviour
{
    int index;
    Fraction number;

    GameManager gameManager;

    [SerializeField] TextMeshProUGUI textUI;
    [SerializeField] SpriteRenderer sr;


    public  void Init(int index, Fraction number,GameManager gameManager)
    {
        this.index = index;
        this.number = number;
        this.gameManager = gameManager;
        textUI.text = number.ToString();
        gameManager.EquationChange += UpdateUI;
    }

    private void OnMouseDown()
    {
        gameManager.InputNumberProcess(index);
    }

    void UpdateUI()
    {
        if(gameManager.currentEquation.firstNumberIndex == index ||
           gameManager.currentEquation.secondNumberIndex == index)
        {
            sr.color = Color.gray;
        }
        else
        {
            sr.color = Color.white;
        }
    }

    void OnDisable()
    {
        gameManager.EquationChange -= UpdateUI;
    }
}
