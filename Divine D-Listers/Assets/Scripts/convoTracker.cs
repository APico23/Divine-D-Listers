using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New ConvoTracker", menuName = "Dialogue/New Tracker")]

public class convoTracker : ScriptableObject, ISerializationCallbackReceiver
{
    public int convoAt;

    public void continueConvo()
    {
        convoAt++;
    }

    public void OnAfterDeserialize()
    {
        convoAt = 0;
    }

    public void OnBeforeSerialize()
    {

    }
}
