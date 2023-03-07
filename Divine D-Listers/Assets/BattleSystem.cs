using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, JORMTURN, HAMEEDATURN, EXOUNOSTURN, ENEMYTURN, WON, LOST, PAUSE }

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
    public GameObject attack;

    private int jormDamage;
    private int hameedaDamage;
    private int damageDone;
    private float rounded;
    private int crit;

    public Transform enemyBattleSpawn;
    public Transform jormBattleSpawn;
    public Transform hameedaBattleSpawn;
    public Transform exounosBattleSpawn;

    Unit playerUnit1;
    Unit playerUnit2;
    Unit playerUnit3;
    Unit enemyUnit;

    private string[] speeds = new string[3];
   
    public Text dialougeText;
    public Text initiaviteHUD;
    public Text jormLevel;
    public Text hameedaLevel;
    public Text exounosLevel;

    private int turnNum = 0;

    public healthBar jormHealthBar;
    public healthBar jormManaBar;
    public healthBar hameedaHealthBar;
    public healthBar hameedaManaBar;
    public healthBar exounosHealthBar;
    public healthBar exounosManaBar;

    public IEnumerator testing;


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

        jormHealthBar.setMaxHealth(playerUnit1.maxHP);
        jormHealthBar.setHealth(playerUnit1.maxHP);
        jormManaBar.setMaxHealth(playerUnit1.mana);
        jormManaBar.setHealth(playerUnit1.mana);

        hameedaHealthBar.setMaxHealth(playerUnit2.maxHP);
        hameedaHealthBar.setHealth(playerUnit2.maxHP);
        hameedaManaBar.setMaxHealth(playerUnit2.mana);
        hameedaManaBar.setHealth(playerUnit2.mana);

        exounosHealthBar.setMaxHealth(playerUnit3.maxHP);
        exounosHealthBar.setHealth(playerUnit3.maxHP);
        exounosManaBar.setMaxHealth(playerUnit3.mana);
        exounosManaBar.setHealth(playerUnit3.mana);

        jormLevel.text = "Lv. " + playerUnit1.unitLevel;
        hameedaLevel.text = "Lv. " + playerUnit2.unitLevel;
        exounosLevel.text = "Lv. " + playerUnit3.unitLevel;

        dialougeText.text = enemyUnit.unitName + " approaches!";

        speeds = getInitiative();
        battleSequence();
    }

    void battleSequence()
    {
        initiaviteHUD.text = "Turn order: " + speeds[0] + " " + speeds[1] + " " + speeds[2] + " " + speeds[3];
        attack.SetActive(false);
        if (turnNum > 3)
        {
            turnNum = 0;
            speeds = getInitiative();
        }
        Debug.Log(speeds[turnNum]);
        //make this a switch case later
        if (state != BattleState.WON){
            if ((speeds[turnNum] == "Jorm") && (!playerUnit1.isDead))
            {
                state = BattleState.JORMTURN;
                attack.SetActive(true);
                jormTurn();
            }
            else if ((speeds[turnNum] == "Hameeda") && (!playerUnit2.isDead))
            {
                state = BattleState.HAMEEDATURN;
                attack.SetActive(true);
                hameedaTurn();
            }
            else if ((speeds[turnNum] == "Exounos") && (!playerUnit3.isDead))
            {
                state = BattleState.EXOUNOSTURN;
                attack.SetActive(true);
                exounosTurn();
            }
            else if ((speeds[turnNum] == "Enemy") && (!enemyUnit.isDead))
            {
                state = BattleState.ENEMYTURN;
                StartCoroutine(enemyCoroutine());
            }

            turnNum++;
        }
    }

    string[] getInitiative() 
        {
        //take in all player and enemy names and speeds
        string[] tempArr = { "Jorm", "Hameeda", "Exounos", "Enemy" };
        int[] entitySpeeds = { playerUnit1.speed, playerUnit2.speed, playerUnit3.speed, enemyUnit.speed };
        //sort it from highest to lowest

        bool swappedSomething = true;
        while (swappedSomething) { 
            swappedSomething = false;   
            for (int i = 0; i < 3; i++) { 
                if (entitySpeeds[i] < entitySpeeds[i + 1]) {
                    
                    int tempSpeed = entitySpeeds[i];
                    entitySpeeds[i] = entitySpeeds[i + 1];
                    entitySpeeds[i + 1] = tempSpeed;

                    string tempState = tempArr[i];
                    tempArr[i] = tempArr[i + 1];
                    tempArr[i + 1] = tempState; 

                    swappedSomething = true; 
                }
            }
        }
        //spit it back out    
       
        return tempArr; 
    }

    void jormTurn()
    {
        dialougeText.text = "What will Jorm do?";
        
    }

    void hameedaTurn()
    {
        dialougeText.text = "What will Hameeda do?";
    }

    void exounosTurn()
    {
        dialougeText.text = "What will Exounos do?"; 
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
       if (enemyUnit.currentHp <= 0) 

       {
            state = BattleState.WON;
            dialougeText.text = "The " + enemyUnit.unitName + " has been slain! YOU WIN!";
       }    

    }

    public void takeASeat()
    {
        crit=Random.Range(1, 201);
        jormHUD.SetActive(false);
        rounded =8 * (playerUnit1.damage / 100f);
        if (rounded < 1) rounded = 1;
        jormDamage = Mathf.RoundToInt(8 * rounded);
        damageDone = jormDamage - Mathf.RoundToInt(jormDamage * (enemyUnit.defence / 100f));
        Debug.Log(crit);
        if (crit <= playerUnit1.luck)
        {
            damageDone *= 2;
        }
        enemyUnit.currentHp -= damageDone;
        dialougeText.text = "Hit! " + enemyUnit.unitName + " takes " + damageDone + " damage!";
        state=BattleState.PAUSE;
        isBattleWon();
        battleSequence();
    }

    public void qualityAssurance() 
    {
        jormHUD.SetActive(false);

        //code for attacks goes here
        isBattleWon();
        battleSequence();
    }

    public void yawn() 
    {
        exounosHUD.SetActive(false);
        if (enemyUnit.speed >= (enemyUnit.speed - Mathf.RoundToInt(enemyUnit.speed * .25f)))
        {
            enemyUnit.speed -= 1;
        }
        else
        {
            dialougeText.text = "The " + enemyUnit.unitName + " is drowzy enough.";
        }
        //code for attacks goes here
        state = BattleState.PAUSE;
        isBattleWon();
        battleSequence();
    }

    public void powerNap() 
    {
        exounosHUD.SetActive(false);
        //code for attacks goes here
        isBattleWon();
        battleSequence();
    }

    public void kohldShoulder() 
    {
        hameedaHUD.SetActive(false);
        crit = Random.Range(1, 201);
        rounded = 10 * (playerUnit2.damage / 100f);
        if (rounded < 1) rounded = 1;
        hameedaDamage = Mathf.RoundToInt(10 * rounded);
        damageDone = hameedaDamage - Mathf.RoundToInt(hameedaDamage * (enemyUnit.defence / 100f));
        Debug.Log(crit);
        if (crit <= playerUnit2.luck)
        {
            damageDone *= 2;
        }
        enemyUnit.currentHp -= damageDone;
        dialougeText.text = "Hit! " + enemyUnit.unitName + " takes " + damageDone + " damage!";
        //code for attacks goes here
        state = BattleState.PAUSE;
        isBattleWon();
        battleSequence();
    }

    public void itsNotAPhase()
    {
        hameedaHUD.SetActive(false);
        //code for attacks goes here
        isBattleWon();
        battleSequence();
    }


    private IEnumerator enemyCoroutine() 
    {
        //any code before yeild runs on first frame
        //yield return null;
        dialougeText.text = "The " + enemyUnit.unitName + " attacks!";
        //runs code up to this point on first frame then waits 3 seconds.
        yield return new WaitForSeconds(3);
        //after 3 seconds, picks up from here

        int randNum = Random.Range(0, 3);

        if (randNum == 0)
        {
            playerUnit1.currentHp -= enemyUnit.damage;
            jormHealthBar.setHealth(playerUnit1.currentHp);
            dialougeText.text = enemyUnit.unitName + " attacks Jorm for " + enemyUnit.damage + " damage!";
            isDead(playerUnit1);
        }
        else if (randNum == 1)
        {
            playerUnit2.currentHp -= enemyUnit.damage;
            hameedaHealthBar.setHealth(playerUnit2.currentHp);
            dialougeText.text = enemyUnit.unitName + " attacks Hameeda for " + enemyUnit.damage + " damage!";
            isDead(playerUnit1);
        }
        else if (randNum == 2)
        {
            playerUnit3.currentHp -= enemyUnit.damage;
            exounosHealthBar.setHealth(playerUnit3.currentHp);
            dialougeText.text = enemyUnit.unitName + " attacks Exounos for " + enemyUnit.damage + " damage!";
            isDead(playerUnit1);
        }

        yield return new WaitForSeconds(2);
        //any code after runs one frame after the first frame;
        battleSequence();
    }


    void isDead(Unit player) 
    {
        if (player.currentHp <= 0) {
            player.isDead = true;
        }
        if (playerUnit1.isDead && playerUnit2.isDead && playerUnit3.isDead) 
        {
            state = BattleState.LOST;
            dialougeText.text = "You lose!";
        }
    }


}