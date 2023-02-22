using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, JORMTURN, HAMEEDATURN, EXOUNOSTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public BattleState state;

    public GameObject jormPrefab;
    public GameObject hameedaPrefab;
    public GameObject exounosPrefab;
    public GameObject enemyPrefab;

    public GameObject jormHUD;
    public GameObject hameedaHUD;
    public GameObject exounosHUD;

    private int jormDamage;

    public Transform enemyBattleSpawn;
    public Transform jormBattleSpawn;
    public Transform hameedaBattleSpawn;
    public Transform exounosBattleSpawn;

    Unit playerUnit1;
    Unit playerUnit2;
    Unit playerUnit3;
    Unit enemyUnit;

    public Text dialougeText;

    // Start is called before the first frame update

    void Start()
    {
        state = BattleState.START;
        setupBattle();
    }

    void setupBattle()
    {
        jormHUD.SetActive(false);
        hameedaHUD.SetActive(false);
        exounosHUD.SetActive(false);

        GameObject playerGO = Instantiate(jormPrefab, jormBattleSpawn);
        playerUnit1 = playerGO.GetComponent<Unit>();

        GameObject player2GO = Instantiate(hameedaPrefab, hameedaBattleSpawn);
        playerUnit2 = player2GO.GetComponent<Unit>();

        GameObject player3GO = Instantiate(exounosPrefab, exounosBattleSpawn);
        playerUnit3 = player3GO.GetComponent<Unit>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleSpawn);
        enemyUnit = enemyGO.GetComponent<Unit>();

        dialougeText.text = enemyUnit.unitName + " approaches!";

        state = BattleState.JORMTURN;
        jormTurn();
    }

    void jormTurn()
    {

    }

    public void attackButton()
    {
        if (state == BattleState.JORMTURN)
        {
            jormHUD.SetActive(true);
        }
        else if (state == BattleState.HAMEEDATURN)
        {
            hameedaHUD.SetActive(true);
        }
        else if (state == BattleState.EXOUNOSTURN)
        {
            exounosHUD.SetActive(true);
        }
    }
    
    void isBattleWon()
    {
        //if (enemyUnit.currentHp <= 0) 
        //{
            state = BattleState.WON;
            dialougeText.text = "The " + enemyUnit.unitName + " has been slain! YOU WIN!";
        Debug.Log("You win");
        //}   
    }

    public void takeASeat()
    {
        jormHUD.SetActive(false);
        jormDamage = 10 * (playerUnit1.damage/10);
        enemyUnit.currentHp -= jormDamage;
        dialougeText.text = "Hit! " + enemyUnit.unitName + " takes " + jormDamage + " damage!";
        isBattleWon();
    }
        
    

}