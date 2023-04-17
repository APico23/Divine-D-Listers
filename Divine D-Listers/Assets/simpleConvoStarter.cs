using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class simpleConvoStarter : MonoBehaviour
{
    public int index;
    public convoTracker tracker;

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

}
