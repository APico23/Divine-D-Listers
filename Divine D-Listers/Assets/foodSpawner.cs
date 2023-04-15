using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foodSpawner : MonoBehaviour
{

    public GameObject[] food;

    //Time it takes to spawn food
    [Space(3)]
    public float waitingForNextSpawn = 10;
    public float theCountdown = 10;

    // the range of X
    [Header("X Spawn Range")]
    public float xMin;
    public float xMax;


    // the range of y
    [Header("Y Spawn Range")]
    public float yMin;
    public float yMax;

    public GameObject tracked;

    public void Update()
    {
        // timer to spawn the next food Object
        theCountdown -= Time.deltaTime;
        if (theCountdown <= 0)
        {
            spawnFood();
            theCountdown = waitingForNextSpawn;
        }
        yMin = tracked.transform.position.y + 10;
        yMax = yMin;
        xMin = tracked.transform.position.x - 10;
        xMax = tracked.transform.position.x + 10;
    }

    void spawnFood()
    {
        // Defines the min and max ranges for x and y
        Vector2 pos = new Vector2(Random.Range(xMin, xMax), Random.Range(yMin, yMax));

        // Creates the random object at the random 2D position.
        int rand = Random.Range(0, food.Length);
        Quaternion rot = transform.rotation;
        rot = Quaternion.Euler(0,0,Random.Range(0,360));
        Instantiate(food[rand], pos, rot);

        // If I wanted to get the result of instantiate and fiddle with it, I might do this instead:
        //GameObject newRock = (GameObject)Instantiate(rock, pos)
        //rock.something = somethingelse;
    }
}

