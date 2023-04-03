using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


public class sphinxManager : MonoBehaviour
{

    public convoTracker tracker;
    public Quest riddle1;
    public Quest riddle2;
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (tracker.convoAt == 0)
            {
                dialogueStarter.startConvo();
                tracker.continueConvo();
                riddle1.isStarted = true;
            }
            else
            {
                if (riddle1.isCompleted)
                {
                    tracker.continueConvo();
                    dialogueStarter.startConvo();
                    riddle2.isStarted = true;
                    return;
                }
                else if (!riddle2.isStarted)
                {
                    dialogueStarter.startConvo();
                }
            }
            if (riddle2.isStarted)
            {
                if (riddle2.isCompleted)
                {
                    tracker.convoAt = 4;
                    dialogueStarter.startConvo();
                }
                else
                {
                    tracker.convoAt = 1;
                    dialogueStarter.startConvo();
                    tracker.convoAt = 2;
                }
                
            }
            
        }
    }
}
