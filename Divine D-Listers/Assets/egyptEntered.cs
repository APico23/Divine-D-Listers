using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class egyptEntered : MonoBehaviour
{
    public convoTracker tracker;
    void Start()
    {
        if (tracker.convoAt == 0)
        {
            dialogueStarter.startConvo();
            tracker.continueConvo();
        }
    }

}
