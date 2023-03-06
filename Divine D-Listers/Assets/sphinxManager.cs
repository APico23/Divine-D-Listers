using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


public class sphinxManager : MonoBehaviour
{

    public convoTracker tracker;
    public playerMissions missionsDone;
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (tracker.convoAt == 0)
            {
                dialogueStarter.startConvo();
                tracker.continueConvo();
                missionsDone.riddle1Start= true;
            }
            else
            {
                if (missionsDone.riddle1Done)
                {
                    tracker.continueConvo();
                    dialogueStarter.startConvo();
                }
                else
                {
                    dialogueStarter.startConvo();
                }
            }
            
        }
    }
}
