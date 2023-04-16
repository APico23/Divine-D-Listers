using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rockingChair : MonoBehaviour
{
    public counterSpecial count;
    AudioSource creak;
    bool leftNow;

    void Start()
    {
        leftNow= true;
        creak = gameObject.GetComponent<AudioSource>();
    }

    
    void Update()
    {
        if (Input.GetKeyUp("a")|| Input.GetKeyUp("left"))
        {
            if (leftNow)
            {
                count.count++;
                leftNow= false;
                Vector3 newRotation = new Vector3(0, 0, -10);
                transform.eulerAngles = newRotation;
                creak.Play();
            }
        }

        if (Input.GetKeyUp("d")|| Input.GetKeyUp("right"))
        {
            if (!leftNow)
            {
                count.count++;
                leftNow = true;
                Vector3 newRotation = new Vector3(0, 0, 10);
                transform.eulerAngles = newRotation;
                creak.Play();
            }
        }
    }
}
