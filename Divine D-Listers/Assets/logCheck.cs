using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class logCheck : MonoBehaviour
{
    public Quest gateUnlock;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Log") && !other.isTrigger)
        {
            gateUnlock.isCompleted= true;
        }
    }
}
