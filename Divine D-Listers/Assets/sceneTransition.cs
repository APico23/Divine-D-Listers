using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneTransition : MonoBehaviour
{

    public string sceneToLoad;
    public Vector2 playerPosition;
    public VectorValue playerStorage;

   public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            playerStorage.initialValue = playerPosition;
            playerStorage.currentScene = sceneToLoad;
            Debug.Log("Player Moved");
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
