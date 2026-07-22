using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EffectManager : MonoBehaviour
{
    [SerializeField] Image GameOverEffectImage;
    [SerializeField] float MoveTime = 0.3f;
    public void GameOverEffect(List<GameObject> gameObjects) 
    {

        foreach( GameObject gameObject in gameObjects)
        {
            if( gameObject != null)
            {        
                StartCoroutine(BackGroundMove(gameObject.transform.position));
            }
        }
    }

    IEnumerator ObjectMove(GameObject gameObject)
    {
        Vector3 startPosition = gameObject.transform.position;
        float elapsed = 0f;

        while (elapsed < MoveTime)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / MoveTime;

            gameObject.transform.position = Vector3.Lerp(startPosition, new Vector3(0,1,0), t);

            yield return null;
        }

        transform.position = new Vector3(0,1,0);
    }

    IEnumerator BackGroundMove(Vector3 pos)
    {
        GameOverEffectImage.transform.position = pos;
        for( int i = 0; i <= 25; i++)
        {
            GameOverEffectImage.fillAmount = i * i / 625f;
            yield return null;
        }

        GetComponent<Correct>().UpdateCorrectUI();
    }

    public void ResetUI()
    {
        GameOverEffectImage.fillAmount = 0f;
    }
}
