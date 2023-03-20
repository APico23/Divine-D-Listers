using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Gameplay/New Quest")]

public class Quest : ScriptableObject, ISerializationCallbackReceiver
{

    public string qName;
    public bool isCompleted;
    public bool isStarted;

    public void OnAfterDeserialize()
    {
        isCompleted = false;
        isStarted = false;
    }

    public void OnBeforeSerialize()
    {

    }

}
