using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goldManager : MonoBehaviour
{
    public Quest gold;
    public convoTracker tracker;
    public inventory playerInventory;

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (gold.isCompleted)
            {
                tracker.convoAt = 1;
                dialogueStarter.startConvo();
            }
            else
            {
                tracker.convoAt = 0;
                dialogueStarter.startConvo();
                playerInventory.playerGold += 5000;
                gold.isCompleted = true;
            }
        }
    }
}
