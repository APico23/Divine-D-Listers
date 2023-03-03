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
        
    //private IEnumerator timerCoroutine()
    //{
    //    //any code before the yield runs immediately on the frame it is called
    //    //yield return null;
    //    //Runs up to this point, waits 5 seconds. 
    //    yield return new WaitForSeconds(5);
    //    //any code after the yield runs on the frame after the first frame

    //    //Doesn't actually return anything 
    //}

}
