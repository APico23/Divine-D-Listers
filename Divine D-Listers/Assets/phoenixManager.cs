using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class phoenixManager : MonoBehaviour
{
    public convoTracker tracker;
    public Quest riddle1;
    public VectorValue playerStorage;
    public Vector2 playerPosition;
    public Sprite egg;
    public randomEncounters phoenixEnemy;
    public Sprite background;
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (riddle1.isStarted)
            {   if (tracker.convoAt == 0)
                {
                    tracker.continueConvo();
                }  
                if (tracker.convoAt == 1)
                {
                    dialogueStarter.startConvo();
                    tracker.continueConvo();
                }
                else if ((tracker.convoAt == 2) && (riddle1.isCompleted == false))
                {
                    playerStorage.initialValue = playerPosition;
                    GameObject.Find("BattleStarter").GetComponent<battleStarter>().setEnemy(phoenixEnemy);
                    GameObject.Find("BattleStarter").GetComponent<battleStarter>().background= background;
                    SceneManager.LoadScene("battleScene");
                    tracker.continueConvo();
                }                
                else if (tracker.convoAt == 3)
                {
                    tracker.convoAt = 2;
                    dialogueStarter.startConvo();
                    gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    riddle1.isCompleted = true;
                    tracker.continueConvo();
                    tracker.continueConvo();
                }
            }
            else
            {
                dialogueStarter.startConvo();
            }
        }

    }

    public void Start()
    {

        Debug.Log(tracker.convoAt);
        if (tracker.convoAt == 3) {
            Debug.Log("Hit");
            gameObject.GetComponent<SpriteRenderer>().sprite = egg;
        }else if (tracker.convoAt >= 4)
        {
            Debug.Log("Hit2");
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
