using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonTimer : MonoBehaviour
{
    public float targetTime = 5.0f;
    bool endedRan = false;

    void Update()
    {

        targetTime -= Time.deltaTime;

        if (targetTime <= 0.0f && !endedRan)
        {
            endedRan = true;
            timerEnded();
        }

    }

    void timerEnded()
    {
        Debug.Log("Times Up");
    }
}
