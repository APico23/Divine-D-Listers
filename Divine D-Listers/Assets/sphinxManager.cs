using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


public class sphinxManager : MonoBehaviour
{

    public convoTracker tracker;
    public Quest riddle1;
    public Quest riddle2;
    public Quest riddle3;
    public Quest riddleDone;
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
            else if (!riddle2.isStarted)
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
            else if (!riddle3.isStarted)
            {
                if (riddle2.isCompleted)
                {
                    tracker.convoAt = 4;
                    dialogueStarter.startConvo();
                    riddle3.isStarted = true;
                }
                else
                {
                    tracker.convoAt = 1;
                    dialogueStarter.startConvo();
                    tracker.convoAt = 2;
                }
                
            }
            if (riddle3.isStarted)
            {
                if (riddle3.isCompleted)
                {
                    tracker.convoAt = 6;
                    dialogueStarter.startConvo();
                    riddleDone.isCompleted= true;
                }
                else
                {
                    tracker.convoAt = 1;
                    dialogueStarter.startConvo();
                    tracker.convoAt = 4;
                }
            }
            
        }
    }

    private void Update()
    {
        if (riddleDone.isCompleted)
        {
           gameObject.GetComponent<SpriteRenderer>().enabled = false;
           gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

}
