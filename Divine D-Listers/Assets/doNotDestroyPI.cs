using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doNotDestroyPI : MonoBehaviour
{
    private void Awake()
    {
        GameObject[] playerInfo = GameObject.FindGameObjectsWithTag("PlayerInfo");
        if (playerInfo.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
