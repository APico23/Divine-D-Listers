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

    private void Start()
    {
        move = GetComponent<playerMove>();
        player = GetComponent<Transform>();
    }


    void Update()
    {
        if (move.isMoving == true)
        {
            int rand = Random.Range(0, 2500);
            if (rand == 0)
            {
                playerStorage.initialValue = new Vector2(player.position.x , player.position.y);
                GameObject.Find("BattleStarter").GetComponent<battleStarter>().setEnemy(worldEncounters);
                GameObject.Find("BattleStarter").GetComponent<battleStarter>().background = background;
                SceneManager.LoadScene("battleScene");
            }
        }
    }
}
