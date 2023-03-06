using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class phoenixManager : MonoBehaviour
{
    public convoTracker tracker;
    public playerMissions missionsDone;
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (missionsDone.riddle1Start)
            {
                tracker.continueConvo();
                if (tracker.convoAt == 1)
                {
                    dialogueStarter.startConvo();
                }
                else
                {
                    SceneManager.LoadScene("battleScene");
                }
            }
            else
            {
                dialogueStarter.startConvo();
            }
        }

    }
}
