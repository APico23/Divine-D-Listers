using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class TaskLog : MonoBehaviour
{
    public TextMeshProUGUI text;
    public string currentObjective;
    public Vector3 startingPos;
    public GameObject self;
    private bool isMovedUp;
    private bool isMovedDown;

    void Start()
    {
        isMovedUp = false;
        isMovedDown = true;
        startingPos = self.transform.position;
    }

    void Update() 
    {
        text.text = currentObjective;
    }

    public void moveTaskLog() 
    {
        if (isMovedUp)
        {
            StartCoroutine(moveDown());
        }
        else if (isMovedDown)
        {
            StartCoroutine(moveUp());
        }
    }

    public IEnumerator moveUp() 
    {
        int counter = 0;
        while (counter < 50) {
            self.transform.Translate(0, 1f, 0);
            yield return new WaitForSeconds(0.01f);
            counter++;
        }
        isMovedDown = false;
        isMovedUp = true; 
    }

    public IEnumerator moveDown()
    {
        int counter = 0;
        while (counter < 50)
        {
            self.transform.Translate(0, -1f, 0);
            yield return new WaitForSeconds(0.01f);
            counter++;
        }
        isMovedUp = false;
        isMovedDown = true; 
    }


}
