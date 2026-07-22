using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Correct : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI correctText;
    void Start()
    {
        UpdateCorrectUI();
    }

    public void UpdateCorrectUI()
    {
        correctText.text = $"Correct: {SaveData.SolvedCount}";
    }

}
