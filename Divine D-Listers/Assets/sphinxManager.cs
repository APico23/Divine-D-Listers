using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


public class sphinxManager : MonoBehaviour
{

    public convoTracker tracker;
    public Quest riddle1;
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
                }
                else
                {
                    dialogueStarter.startConvo();
                }
            }
            
        }
    }
}
