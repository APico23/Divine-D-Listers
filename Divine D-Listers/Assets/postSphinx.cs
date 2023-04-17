using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class postSphinx : MonoBehaviour
{
    public convoTracker tracker;
    public convoTracker sphinxCheck;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            if (tracker.convoAt == 0 && sphinxCheck.convoAt == 1)
            {
                sphinxCheck.convoAt = 3;
                dialogueStarter.startConvo();
                sphinxCheck.convoAt = 1;
                tracker.continueConvo();
            }
            if (tracker.convoAt == 1 && sphinxCheck.convoAt == 4)
            {
                sphinxCheck.convoAt = 5;
                dialogueStarter.startConvo();
                sphinxCheck.convoAt = 4;
                tracker.continueConvo();
            }
        }
    }
}
