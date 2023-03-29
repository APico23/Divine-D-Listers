using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleConvoStarter : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            dialogueStarter.startConvo();
        }
    }
}
