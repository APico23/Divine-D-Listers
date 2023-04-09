using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rockingChair : MonoBehaviour
{

    bool leftNow;

    void Start()
    {
        leftNow= true;
    }

    
    void Update()
    {
        if (Input.GetKeyUp("a")|| Input.GetKeyUp("left"))
        {
            if (leftNow)
            {
                leftNow= false;
                Vector3 newRotation = new Vector3(0, 0, -10);
                transform.eulerAngles = newRotation;
            }
        }

        if (Input.GetKeyUp("d")|| Input.GetKeyUp("right"))
        {
            if (!leftNow)
            {
                leftNow = true;
                Vector3 newRotation = new Vector3(0, 0, 10);
                transform.eulerAngles = newRotation;
            }
        }
    }
}
