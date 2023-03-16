using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class VectorValue : ScriptableObject, ISerializationCallbackReceiver
{
    public Vector2 initialValue;
    public string currentScene;
    public Vector2 defaultValue;
    public string defaultScene;

    public void OnAfterDeserialize()
    {
        initialValue = defaultValue;
        currentScene = defaultScene;
    }

    public void OnBeforeSerialize()
    {

    }
}
