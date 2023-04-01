using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class battleStarter : MonoBehaviour
{

    public randomEncounters enemyMain;

    public bool isMultiple;

    public Sprite background;

    private void Awake()
    {
        GameObject[] battleStarters = GameObject.FindGameObjectsWithTag("BattleStarter");
        if (battleStarters.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void setEnemy(randomEncounters newEnemy)
    {
        enemyMain = newEnemy;
    }


}
