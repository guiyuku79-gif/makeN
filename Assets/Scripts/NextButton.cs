using UnityEngine;

public class NextButton : MonoBehaviour
{
    [SerializeField] GameObject nextButton;
    void Start()
    {
        if (TitleManager.isDesignedMode)
        {
            nextButton.SetActive(false);
        }
    }


}
