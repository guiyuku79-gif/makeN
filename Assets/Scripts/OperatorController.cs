using UnityEngine;

public class OperatorController : MonoBehaviour
{
    int index;
    Fraction number;

    [SerializeField] GameManager gameManager;
    [SerializeField] SpriteRenderer sr;

    [SerializeField] string operatorName;



    void Start()
    {
        gameManager.EquationChange += UpdateUI;        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnMouseDown()
    {
        gameManager.InputOperatorProcess(operatorName);

    }

    void UpdateUI()
    {
        if(gameManager.currentEquation.selectedOperator == operatorName)
        {
            sr.color = Color.gray;
        }
        else
        {
            sr.color = Color.white;
        }
    }


}
