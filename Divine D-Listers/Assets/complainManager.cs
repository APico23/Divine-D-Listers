using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class complainManager : MonoBehaviour
{
    public Quest fiveCheck;
    public convoTracker richTracker;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            if (fiveCheck.isStarted && !fiveCheck.isCompleted)
            {
                int temp = richTracker.convoAt;
                richTracker.convoAt = 6;
                dialogueStarter.startConvo();
                richTracker.convoAt = temp;
                fiveCheck.isCompleted= true;
            }
           
        }
    }
}
