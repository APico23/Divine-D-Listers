using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class randomEncounterMaker : MonoBehaviour
{

    private playerMove move;
    public VectorValue playerStorage;
    public randomEncounters worldEncounters;
    private Transform player;
    public Sprite background;
    public GameObject transition;
    public float waitTime = 2f;

    private void Start()
    {
        move = GetComponent<playerMove>();
        player = GetComponent<Transform>();
    }


    void Update()
    {
        if (move.isMoving == true)
        {
            int rand = Random.Range(0, 3500);
            if (rand == 0)
            {
                playerStorage.initialValue = new Vector2(player.position.x , player.position.y);
                int rand2 = Random.Range(0, 4);
                if (rand2 == 0)
                {
                    GameObject.Find("BattleStarter").GetComponent<battleStarter>().isMultiple = true;
                }
                else
                {
                    GameObject.Find("BattleStarter").GetComponent<battleStarter>().isMultiple = false;
                }
                GameObject.Find("BattleStarter").GetComponent<battleStarter>().setEnemy(worldEncounters);
                GameObject.Find("BattleStarter").GetComponent<battleStarter>().background = background;
                move.canMove = false;
                StartCoroutine(startBattle());
            }
        }
    }


    public IEnumerator startBattle()
    {
        if (transition != null)
        {
            Instantiate(transition, Vector3.zero, Quaternion.identity);
        }
        yield return new WaitForSeconds(waitTime);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("battleScene");
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }

}
