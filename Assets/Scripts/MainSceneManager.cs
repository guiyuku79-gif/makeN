using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneManager : MonoBehaviour
{
    public void ToTitleScene()
    {
        SceneManager.LoadScene("StartScene");
    }
}
