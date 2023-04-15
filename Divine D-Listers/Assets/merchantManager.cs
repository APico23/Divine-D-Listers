using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class merchantManager : MonoBehaviour
{

    public GameObject shopWindow;



    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Instantiate(shopWindow);
        }
    }

}

