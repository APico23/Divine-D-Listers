using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winScript : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            Debug.Log("You Win!!!!");
        }
    }
}
