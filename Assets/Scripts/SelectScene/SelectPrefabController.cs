using TMPro;
using UnityEngine;

public class SelectPrefabController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI numberText;

    [SerializeField] int number;

    void Start()
    {
        numberText.text = number.ToString();
    }

    private void OnMouseDown()
    {
        SelectManager.Instance.AddSelectNumber(number);
    }
}