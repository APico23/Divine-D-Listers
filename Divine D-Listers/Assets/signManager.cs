using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class signManager : MonoBehaviour
{
    public convoTracker tracker;
    public Quest riddle2;

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (tracker.convoAt != 0 && riddle2.isStarted && !riddle2.isCompleted)
            {
                tracker.convoAt = 2;
                dialogueStarter.startConvo();
            }
            if (tracker.convoAt == 0)
            {
                dialogueStarter.startConvo();
                tracker.continueConvo();
            }
            else
            {
                tracker.convoAt = 1;
                dialogueStarter.startConvo();
            }            
        }
    }
}
