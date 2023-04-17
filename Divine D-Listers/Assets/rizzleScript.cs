using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class rizzleScript : MonoBehaviour
{

    public string sceneToLoad;
    public Vector2 playerPosition;
    public VectorValue playerStorage;
    public GameObject fadeInPanel;
    public GameObject fadeOutPanel;
    public float fadeWait;

    public playerMove move;

    public int index;
    public convoTracker tracker;

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (tracker.convoAt != 10)
            {
                int temp = tracker.convoAt;
                tracker.convoAt = index;
                dialogueStarter.startConvo();
            }
            else {
                playerStorage.initialValue = playerPosition;
                playerStorage.currentScene = sceneToLoad;
                StartCoroutine(fadeCo());

            }
                        
        }
    }

   
    public IEnumerator fadeCo()
    {
        if (fadeOutPanel != null)
        {
            Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity);
        }
        yield return new WaitForSeconds(fadeWait);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }
}
