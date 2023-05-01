using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class TaskLog : MonoBehaviour
{
    public TextMeshProUGUI text;
    public string currentObjective;
    public PlayerInfo playerInfo;
    public Vector3 startingPos;
    public GameObject self;
    private bool isMovedUp;
    private bool isMovedDown;
    private bool isMoving;

    void Start()
    {
        isMovedUp = false;
        isMovedDown = true;
        startingPos = self.transform.position; 
        playerInfo= GameObject.Find("PlayerInfo").GetComponent<PlayerInfo>();
    }

    void Update() 
    {
        if (checkObjective())
        {
            text.text = currentObjective;
        }
    }

    public void moveTaskLog() 
    {
        if (!isMoving)
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
    }

    public IEnumerator moveUp() 
    {
        isMoving = true;
        int counter = 0;
        while (counter < 175) {
            self.transform.Translate(0, 2f, 0);
            yield return new WaitForSeconds(0.01f);
            counter++;
        }
        isMovedDown = false;
        isMovedUp = true;
        isMoving = false;
    }

    public IEnumerator moveDown()
    {
        isMoving = true;
        int counter = 0;
        while (counter < 175)
        {
            self.transform.Translate(0, -2f, 0);
            yield return new WaitForSeconds(0.01f);
            counter++;
        }
        isMovedUp = false;
        isMovedDown = true;
        isMoving = false;
    }

    bool checkObjective()
    {
        if (!playerInfo.quests[0].isStarted)
        {
            currentObjective = "Train up and go to Egypt";
            return true;
        }
        else if (playerInfo.quests[0].isStarted && !playerInfo.quests[0].isCompleted)
        {
            currentObjective = "Find the Sphinx's Favorite Food";
            return true;
        }
        else if (playerInfo.quests[0].isCompleted && !playerInfo.quests[1].isStarted)
        {
            currentObjective = "Return to the sphinx";
            return true;
        }
        else if (playerInfo.quests[1].isStarted && !playerInfo.quests[1].isCompleted)
        {
            currentObjective = "Find who owes the Sphinx 20$";
            return true;
        }
        else if (playerInfo.quests[1].isCompleted && !playerInfo.quests[3].isStarted)
        {
            currentObjective = "Return to the sphinx";
            return true;
        }
        else if (playerInfo.quests[3].isStarted && !playerInfo.quests[3].isCompleted)
        {
            currentObjective = "Find the Sphinx's favorite color";
            return true;
        }
        else if (playerInfo.quests[3].isCompleted && !playerInfo.quests[5].isCompleted)
        {
            currentObjective = "Return to the sphinx";
            return true;
        }
        else if (playerInfo.quests[5].isCompleted && !playerInfo.quests[10].isCompleted)
        {
            currentObjective = "Enter the Pyramid";
            return true;
        }
        else if (playerInfo.quests[10].isCompleted && !playerInfo.quests[6].isCompleted)
        {
            currentObjective = "Defeat Ra and Anubis";
            return true;
        }
        else if (playerInfo.quests[6].isCompleted && !playerInfo.quests[9].isCompleted)
        {
            currentObjective = "Defeat Ra again";
            return true;
        }
        else if (playerInfo.quests[9].isCompleted)
        {
            currentObjective = "Bully Ra";
            return true;
        }

        return false;
    }

}
