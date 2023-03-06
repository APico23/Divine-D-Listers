using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New ConvoTracker", menuName = "Dialogue/New Tracker")]

public class convoTracker : ScriptableObject
{
    public int convoAt;

    public void continueConvo()
    {
        convoAt++;
    }
}
