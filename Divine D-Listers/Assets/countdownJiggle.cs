using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class countdownJiggle : MonoBehaviour
{
    GameObject number;
    float xChange;
    bool changeCheck;

    // Start is called before the first frame update
    void Start()
    {
        xChange = 5;
        number = GameObject.Find("countdown");
    }

    // Update is called once per frame
    void Update()
    {
        if (xChange == 5)
        {
            xChange = 0;
            changeCheck= true;
        }
        else if (xChange == -5) {
            xChange = 0;
            changeCheck= false;
        }
        else
        {
            if (changeCheck)
            {
                xChange = -5;
            }
            else
            {
                xChange = 5;
            }
        }
        
        number.transform.Rotate(0,0,xChange);
    }
}
