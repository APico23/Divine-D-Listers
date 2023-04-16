using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boulderCheck : MonoBehaviour
{

    public Quest gateUnlock;

    void Start()
    {
        if (gateUnlock.isCompleted)
        {
            Destroy(gameObject);
        }
    }

   
}
