using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class richManager : MonoBehaviour
{
    public Quest riddle3;
    public convoTracker tracker;
    public inventory playerInventory;
    public Quest fiveCheck;

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (riddle3.isStarted && !riddle3.isCompleted)
            {
                if (tracker.convoAt == 0)
                {
                    tracker.convoAt = 1;
                    fiveCheck.isStarted = true;
                    dialogueStarter.startConvo();
                }
                else
                {
                    if (playerInventory.playerGold >= 5000) { 
                        tracker.convoAt = 3;
                        dialogueStarter.startConvo();
                        playerInventory.playerGold -= 5000;
                        riddle3.isCompleted = true;
                    }
                    else
                    {
                        tracker.convoAt = 2;
                        dialogueStarter.startConvo();
                    }

                }
            }
            else
            {
                tracker.convoAt = 0;
                dialogueStarter.startConvo();
            }
        }
    }
}
