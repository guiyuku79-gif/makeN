using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class NumPrefabController : MonoBehaviour
{
    int index;
    int number;

    GameManager gameManager;

    [SerializeField] TextMeshProUGUI textUI;
    [SerializeField] SpriteRenderer sr;


    public  void Init(int index, int number,GameManager gameManager)
    {
        this.index = index;
        this.number = number;
        this.gameManager = gameManager;
        textUI.text = number.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnMouseDown()
    {
        print($"オブジェクトがクリックされたよ！");
        sr.color = Color.gray;
    }
}
