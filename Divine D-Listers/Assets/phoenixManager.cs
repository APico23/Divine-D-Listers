using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class phoenixManager : MonoBehaviour
{
    public convoTracker tracker;
    public playerMissions missionsDone;
    public VectorValue playerStorage;
    public Vector2 playerPosition;
    public Sprite egg;
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (missionsDone.riddle1Start)
            {   if (tracker.convoAt == 0)
                {
                    tracker.continueConvo();
                }  
                if (tracker.convoAt == 1)
                {
                    dialogueStarter.startConvo();
                    tracker.continueConvo();
                }
                else if ((tracker.convoAt == 2) && (missionsDone.riddle1Done == false))
                {
                    playerStorage.initialValue = playerPosition;
                    SceneManager.LoadScene("battleScene");
                    tracker.continueConvo();
                }                
                else if (tracker.convoAt == 3)
                {
                    tracker.convoAt -= 1;
                    dialogueStarter.startConvo();
                    gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    missionsDone.riddle1Done = true;
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
