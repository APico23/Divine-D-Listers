using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameStartController : MonoBehaviour
{

    public playerMove move;
    public convoTracker tracker;
    public GameObject passOut;

    void Start()
    {
        if (tracker.convoAt == 0)
        {
            dialogueStarter.startConvo();
        }
    }

    void LateUpdate()
    {
        if (tracker.convoAt == 0)
        {
            if (move.canMove)
            {
                tracker.continueConvo();
                StartCoroutine(blackOut());                
            }
        }
    }

    IEnumerator blackOut()
    {
        move.canMove= false;
        GameObject temp = Instantiate(passOut);
        yield return new WaitForSeconds(4f);
        Destroy(temp);
        move.canMove= true;
        dialogueStarter.startConvo();
    }
}
