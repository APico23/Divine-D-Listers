using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class signManager : MonoBehaviour
{
    public int index;
    public convoTracker tracker;
    public Quest riddle2;

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (tracker.convoAt == 0)
            {
                dialogueStarter.startConvo();
            }


            int temp = tracker.convoAt;
            tracker.convoAt = index;
            dialogueStarter.startConvo();
            tracker.convoAt = temp;
        }
    }
}
