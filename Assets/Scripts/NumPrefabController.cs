using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class NumPrefabController : MonoBehaviour
{
    int index;
    Fraction number;


    [SerializeField] TextMeshProUGUI textUI;
    [SerializeField] SpriteRenderer sr;


    public  void Init(int index, Fraction number){
        this.index = index;
        this.number = number;
        textUI.text = number.ToString();
        GameManager.Instance.EquationChange += UpdateUI;
    }

    public void UpdateFraction(Fraction fraction)
    {
        this.number = fraction;
        textUI.text = number.ToString();
    }


    private void OnMouseDown()
    {
        GameManager.Instance.InputNumberProcess(index);
    }

    void UpdateUI()
    {
        if(GameManager.Instance.currentEquation.firstNumberIndex == index ||
           GameManager.Instance.currentEquation.secondNumberIndex == index)
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
        GameManager.Instance.EquationChange -= UpdateUI;
    }
}
