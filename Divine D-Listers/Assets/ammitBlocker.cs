using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammitBlocker : MonoBehaviour
{

    public int index;
    public convoTracker tracker;
    public Quest ammitUnlocked;

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            int temp = tracker.convoAt;
            tracker.convoAt = index;
            dialogueStarter.startConvo();
            tracker.convoAt = temp;
        }
    }

    void Start()
    {
        if (ammitUnlocked.isStarted)
        {
            Destroy(gameObject);
        }
    }

}
