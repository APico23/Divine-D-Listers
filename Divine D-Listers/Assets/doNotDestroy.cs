using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doNotDestroy : MonoBehaviour
{
    
    private void Awake()
    {
        GameObject[] musicList = GameObject.FindGameObjectsWithTag("GameMusic");
        if (musicList.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
