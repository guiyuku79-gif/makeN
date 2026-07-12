using UnityEngine;

public class OperatorController : MonoBehaviour
{
    int index;
    Fraction number;

    [SerializeField] SpriteRenderer sr;

    [SerializeField] string operatorName;



    void Start()
    {
        GameManager.Instance.EquationChange += UpdateUI;        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnMouseDown()
    {
        GameManager.Instance.InputOperatorProcess(operatorName);

    }

    void UpdateUI()
    {
        if(GameManager.Instance.currentEquation.selectedOperator == operatorName)
        {
            sr.color = Color.gray;
        }
        else
        {
            sr.color = Color.white;
        }
    }


}
