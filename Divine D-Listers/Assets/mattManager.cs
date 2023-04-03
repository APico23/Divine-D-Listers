using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class mattManager : MonoBehaviour
{

    public VectorValue playerStorage;
    public Vector2 playerPosition;
    public randomEncounters trainingBag;
    public Sprite background;

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
                playerStorage.initialValue = playerPosition;
                GameObject.Find("BattleStarter").GetComponent<battleStarter>().setEnemy(trainingBag);
                GameObject.Find("BattleStarter").GetComponent<battleStarter>().background = background;
                SceneManager.LoadScene("battleScene");
             
        }
    }
}
