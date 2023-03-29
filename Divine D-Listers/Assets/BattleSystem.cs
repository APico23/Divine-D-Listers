using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum BattleState { START, JORMTURN, HAMEEDATURN, EXOUNOSTURN, ENEMYTURN, WON, LOST, PAUSE }


public class BattleSystem : MonoBehaviour
{
    public BattleState state;

    public GameObject jormPrefab;
    public GameObject hameedaPrefab;
    public GameObject exounosPrefab;

    public battleStarter battleStart;

    private GameObject enemyPrefab;

    public GameObject jormHUD;
    public GameObject hameedaHUD;
    public GameObject exounosHUD;
    public GameObject attack;
    public GameObject jormStats;
    public GameObject hameedaStats;
    public GameObject exounosStats;

    private int jormDamage;
    private int hameedaDamage;
    private int enemyDamage;
    private int damageDone;
    private float rounded;
    private int crit;
    private bool isCrit;

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
    public Text jormHp;
    public Text hameedaHp;
    public Text exounosHp;

    private int turnNum = 0;

    public newHealthBar jormHealthBar;
    public newHealthBar hameedaHealthBar;
    public newHealthBar exounosHealthBar;

    public VectorValue currentPosition;

    public IEnumerator testing;


    // Start is called before the first frame update

    void Start()
    {
        state = BattleState.START;
        battleStart = GameObject.Find("BattleStarter").GetComponent<battleStarter>();
        setupBattle();
    }

    void setupBattle()
    {
        jormHUD.SetActive(false);
        hameedaHUD.SetActive(false);
        exounosHUD.SetActive(false);

        enemyPrefab = battleStart.enemyMain.getRandomFighter();

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

        hameedaHealthBar.setMaxHealth(playerUnit2.maxHP);
        hameedaHealthBar.setHealth(playerUnit2.maxHP);
      
        exounosHealthBar.setMaxHealth(playerUnit3.maxHP);
        exounosHealthBar.setHealth(playerUnit3.maxHP);
      
        jormLevel.text = "" + playerUnit1.unitLevel;
        hameedaLevel.text = "" + playerUnit2.unitLevel;
        exounosLevel.text = "" + playerUnit3.unitLevel;
        jormHp.text = playerUnit1.maxHP + "/" + playerUnit1.currentHp;
        hameedaHp.text = playerUnit2.maxHP + "/" + playerUnit2.currentHp;
        exounosHp.text = playerUnit3.maxHP + "/" + playerUnit3.currentHp;

        StartCoroutine(TypeText(enemyUnit.unitName + " approaches!"));

        speeds = getInitiative();
        battleSequence();
    }

    void battleSequence()
    {
        initiaviteHUD.text = "Order: " + speeds[0] + " " + speeds[1] + " " + speeds[2] + " " + speeds[3];
        attack.SetActive(false);
        if (turnNum > 3)
        {
            turnNum = 0;
            speeds = getInitiative();
        }
        Debug.Log(speeds[turnNum]);
        //make this a switch case later
        if (state != BattleState.WON){
            if (speeds[turnNum] == "Jorm")
            {
                if (playerUnit1.isDead)
                {
                    turnNum++;
                    battleSequence();
                }
                else
                {
                    turnNum++;
                    state = BattleState.JORMTURN;
                    attack.SetActive(true);
                    jormTurn();
                }
            }
            else if (speeds[turnNum] == "Hameeda")
            {
                if (playerUnit2.isDead)
                {
                    turnNum++;
                    battleSequence();
                }
                else {
                    turnNum++;
                    state = BattleState.HAMEEDATURN;
                    attack.SetActive(true);
                    hameedaTurn();
                }
            }
            else if (speeds[turnNum] == "Exounos")
            {
                if (playerUnit3.isDead)
                {
                    turnNum++;
                    battleSequence();
                }
                else
                {
                    turnNum++;
                    state = BattleState.EXOUNOSTURN;
                    attack.SetActive(true);
                    exounosTurn();
                }
            }
            else if (speeds[turnNum] == "Enemy")
            {
                if (enemyUnit.isDead)
                {
                    turnNum++;
                    battleSequence();
                }
                else {
                    turnNum++;
                    state = BattleState.ENEMYTURN;
                    StartCoroutine(enemyCoroutine());
                }
            }
            
            
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
        StartCoroutine(TypeText("What will Jorm do?"));
        
    }

    void hameedaTurn()
    {
        StartCoroutine(TypeText("What will Hameeda do?"));
    }

    void exounosTurn()
    {
        StartCoroutine(TypeText("What will Exounos do?")); 
    }

    public void attackButton()
    {
        if (state == BattleState.JORMTURN)
        {
            jormStats.SetActive(false);
            jormHUD.SetActive(true);
        }
        else if (state == BattleState.HAMEEDATURN)
        {
            hameedaStats.SetActive(false);
            hameedaHUD.SetActive(true);
        }
        else if (state == BattleState.EXOUNOSTURN)
        {
            exounosStats.SetActive(false);
            exounosHUD.SetActive(true);
        }
    }
    
    void isBattleWon()
    {
       if (enemyUnit.currentHp <= 0) 

       {
            state = BattleState.WON;
            StartCoroutine(TypeText("The " + enemyUnit.unitName + " has been slain! YOU WIN!"));
            StartCoroutine(winCoroutineWait());
       }    

    }

    public void takeASeat()
    {
        
        isCrit = false;
        crit=Random.Range(1, 201);
        jormHUD.SetActive(false);
        jormStats.SetActive(true);
        rounded =8 * (playerUnit1.damage / 100f);
        if (rounded < 1) rounded = 1;
        jormDamage = Mathf.RoundToInt(8 * rounded);
        damageDone = jormDamage - Mathf.RoundToInt(jormDamage * (enemyUnit.defence / 100f));
        Debug.Log(crit);
        if (crit <= playerUnit1.luck)
        {
            isCrit = true;
            damageDone *= 2;
        }
        enemyUnit.currentHp -= damageDone;
        attack.SetActive(false);
        StartCoroutine(playerCoroutineAttack(playerUnit1, damageDone, isCrit));   
    }

    public void qualityAssurance() 
    {
        jormHUD.SetActive(false);
        jormStats.SetActive(true);
        if (playerUnit1.defence <= (playerUnit1.defence + Mathf.RoundToInt(playerUnit1.defence * .25f)))
        {
            playerUnit1.defence += 1;
        }
        if (playerUnit2.defence <= (playerUnit2.defence + Mathf.RoundToInt(playerUnit2.defence * .25f)))
        {
            playerUnit2.defence += 1;
        }
        if (playerUnit3.defence <= (playerUnit3.defence + Mathf.RoundToInt(playerUnit3.defence * .25f)))
        {
            playerUnit3.defence += 1;
        }
        StartCoroutine(TypeText("Jorm makes sure the party is safe. Just a few extra nails in place."));
        //code for attacks goes here
        attack.SetActive(false);
        StartCoroutine(playerCoroutineNeutral());
       
    }

    public void yawn() 
    {
        exounosHUD.SetActive(false);
        exounosStats.SetActive(true);
        if (enemyUnit.speed >= (enemyUnit.speed - Mathf.RoundToInt(enemyUnit.speed * .25f)))
        {
            enemyUnit.speed -= 1;
            StartCoroutine(TypeText("The " + enemyUnit.unitName + " grows drowzy."));
        }
        else
        {
            StartCoroutine(TypeText("The " + enemyUnit.unitName + " is drowzy enough."));
        }
        //code for attacks goes here
        attack.SetActive(false);
        StartCoroutine(playerCoroutineNeutral());
    }

    public void powerNap() 
    {
        exounosHUD.SetActive(false);
        exounosStats.SetActive(true);
        if (playerUnit1.currentHp < playerUnit1.maxHP)
        {
            playerUnit1.currentHp += 4;
            if (playerUnit1.currentHp > playerUnit1.maxHP && !playerUnit1.isDead)
            {
                playerUnit1.currentHp= playerUnit1.maxHP;
            }
            jormHp.text = playerUnit1.currentHp + "/" + playerUnit1.maxHP;
            jormHealthBar.setHealth(playerUnit1.currentHp);
        }
        if (playerUnit2.currentHp < playerUnit2.maxHP && !playerUnit2.isDead)
        {
            playerUnit2.currentHp += 4;
            if (playerUnit2.currentHp > playerUnit2.maxHP)
            {
                playerUnit2.currentHp = playerUnit2.maxHP;
            }
            hameedaHp.text = playerUnit2.currentHp + "/" + playerUnit2.maxHP;
            hameedaHealthBar.setHealth(playerUnit2.currentHp);
        }
        if (playerUnit3.currentHp < playerUnit3.maxHP && !playerUnit3.isDead)
        {
            playerUnit3.currentHp += 4;
            if (playerUnit3.currentHp > playerUnit3.maxHP)
            {
                playerUnit3.currentHp = playerUnit3.maxHP;
            }
            exounosHp.text = playerUnit3.currentHp + "/" + playerUnit3.maxHP;
            exounosHealthBar.setHealth(playerUnit3.currentHp);
        }
        StartCoroutine(TypeText("The party dozes off for a moment before waking rejuvinated."));
        //code for attacks goes here
        attack.SetActive(false);
        StartCoroutine(playerCoroutineNeutral());
    }

    public void kohldShoulder() 
    {
        isCrit = false;
        hameedaHUD.SetActive(false);
        hameedaStats.SetActive(true);
        crit = Random.Range(1, 201);
        rounded = 10 * (playerUnit2.damage / 100f);
        if (rounded < 1) rounded = 1;
        hameedaDamage = Mathf.RoundToInt(10 * rounded);
        damageDone = hameedaDamage - Mathf.RoundToInt(hameedaDamage * (enemyUnit.defence / 100f));
        Debug.Log(crit);
        if (crit <= playerUnit2.luck)
        {
            isCrit = true;
            damageDone *= 2;
        }
        enemyUnit.currentHp -= damageDone;
        attack.SetActive(false);
        StartCoroutine(playerCoroutineAttack(playerUnit2, damageDone, isCrit));
        //code for attacks goes here
    }

    public void itsNotAPhase()
    {
        hameedaHUD.SetActive(false);
        hameedaStats.SetActive(true);
        if (playerUnit2.damage <= (playerUnit2.damage + Mathf.RoundToInt(playerUnit2.damage * .25f)))
        {
            playerUnit2.damage += 1;
            StartCoroutine(TypeText(playerUnit2.unitName + " gathers magical energy."));
        }
        else
        {
            StartCoroutine(TypeText(playerUnit2.unitName + " has gained the most energy she can handle."));
        }
        //code for attacks goes here
        attack.SetActive(false);
        StartCoroutine(playerCoroutineNeutral());
    }

    private IEnumerator playerCoroutineAttack(Unit u, int d, bool b)
    {
        StartCoroutine(TypeText("Hit! " + u.unitName + " attacks " + enemyUnit.unitName + " for " + d + " damage!"));

        yield return new WaitForSeconds(3);

        if (b) 
        {
            StartCoroutine(TypeText("A CRITICAL HIT!"));
            yield return new WaitForSeconds(3);
        }
        state = BattleState.PAUSE;
        isBattleWon();
        battleSequence();
    }

    private IEnumerator playerCoroutineNeutral() 
    {
        yield return new WaitForSeconds(3);
        state = BattleState.PAUSE;
        isBattleWon();
        battleSequence();
    } 

    private IEnumerator winCoroutineWait()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(currentPosition.currentScene);
    }

    private IEnumerator enemyCoroutine() 
    {
        //any code before yeild runs on first frame
        //yield return null;

        StartCoroutine(TypeText("The " + enemyUnit.unitName + " attacks!"));
        int randNum;

        double unit1 = playerUnit1.maxHP;
        double unit2 = playerUnit2.maxHP;
        double unit3 = playerUnit3.maxHP;
        //runs code up to this point on first frame then waits 3 seconds.
        yield return new WaitForSeconds(3);
        //after 3 seconds, picks up from here
        if (enemyUnit.unitName == "Phoenix")
        {

            Pheonix(randNum, unit1, unit2, unit3);
        }
        else
        {
            basic(randNum, unit1,unit2,unit3);

        }
        yield return new WaitForSeconds(2);
        //any code after runs one frame after the first frame;
        battleSequence();
    }


    void isDead(Unit player) 
    {
        if (player.currentHp <= 0)
        {
            player.isDead = true;
        } 

        if (playerUnit1.isDead && playerUnit2.isDead && playerUnit3.isDead) 
        {
            state = BattleState.LOST;
            StartCoroutine(TypeText("You lose!"));
        }
    }


    private IEnumerator TypeText(string text)
    {
        dialougeText.text = "";
        bool complete = false;

        int index = 0;

        while (!complete)
        {
            dialougeText.text += text[index];
            yield return new WaitForSeconds(0.02f);
            index++;

            if (index == text.Length)
            {
                complete = true;
            }
        }

    }

    void damaged(Unit player, int target, int damage)
    {
        player.currentHp -= damageDone;
        if (player.currentHp <= 0)
        {
            player.currentHp = 0;
        }
        if (target == 0)
        {
            jormHealthBar.setHealth(player.currentHp);
            if (player.currentHp <= 0)
            {
                jormHp.text = "0/" + player.maxHP;
            }
            else
            {
                jormHp.text = player.currentHp + "/" + player.maxHP;
            }
        }
        else if (target == 1)
        {
            hameedaHealthBar.setHealth(player.currentHp);
            if (player.currentHp <= 0)
            {
                hameedaHp.text = "0/" + player.maxHP;
            }
            else
            {
                hameedaHp.text = player.currentHp + "/" + player.maxHP;
            }

        }
        else if (target == 2)
        {
            exounosHealthBar.setHealth(player.currentHp);
            if (player.currentHp <= 0)
            {
                exounosHp.text = "0/" + player.maxHP;
            }
            else
            {
                exounosHp.text = player.currentHp + "/" + player.maxHP;
            }
        }
        isDead(player);
    }

    void Pheonix(int randNum, double unit1, double unit2, double unit3 )
    {
        randNum = Random.Range(0, 10);
        if (playerUnit1.currentHp / unit1 > .7 && playerUnit2.currentHp / unit2 > .7 && playerUnit3.currentHp / unit3 > .7 && randNum < 7)
        {
            rounded = 5 * (enemyUnit.damage / 100f);
            if (rounded < 1) rounded = 1;
            enemyDamage = Mathf.RoundToInt(5 * rounded);
            dialougeText.text = enemyUnit.unitName + " breathes fire";
            damageDone = enemyDamage - Mathf.RoundToInt(enemyDamage * (playerUnit1.defence / 100f));
            damaged(playerUnit1, 0, damageDone);
            damageDone = enemyDamage - Mathf.RoundToInt(enemyDamage * (playerUnit2.defence / 100f));
            damaged(playerUnit2, 1, damageDone);
            damageDone = enemyDamage - Mathf.RoundToInt(enemyDamage * (playerUnit3.defence / 100f));
            damaged(playerUnit3, 2, damageDone);
        }
        else
        {
            randNum = Random.Range(0, 3);
            crit = Random.Range(1, 201);
            rounded = 10 * (enemyUnit.damage / 100f);
            if (rounded < 1) rounded = 1;
            enemyDamage = Mathf.RoundToInt(10 * rounded);

            if (randNum == 0 && !playerUnit1.isDead)
            {
                damageDone = enemyDamage - Mathf.RoundToInt(enemyDamage * (playerUnit1.defence / 100f));
                if (crit <= enemyUnit.luck)
                {
                    damageDone *= 2;
                }
                damaged(playerUnit1, 0, damageDone);
                dialougeText.text = enemyUnit.unitName + " attacks Jorm for " + damageDone + " damage!";

            }
            else if (randNum == 1 && !playerUnit2.isDead)
            {
                damageDone = enemyDamage - Mathf.RoundToInt(enemyDamage * (playerUnit2.defence / 100f));
                if (crit <= enemyUnit.luck)
                {
                    damageDone *= 2;
                }
                damaged(playerUnit2, 1, damageDone);
                dialougeText.text = enemyUnit.unitName + " attacks Hameeda for " + damageDone + " damage!";
            }
            else if (randNum == 2 && !playerUnit3.isDead)
            {

                damageDone = enemyDamage - Mathf.RoundToInt(enemyDamage * (playerUnit3.defence / 100f));
                if (crit <= enemyUnit.luck)
                {
                    damageDone *= 2;
                }
                damaged(playerUnit3, 2, damageDone);
                dialougeText.text = enemyUnit.unitName + " attacks Exounos for " + damageDone + " damage!";

            }
            else
            {
                dialougeText.text = "The attack missed!";
            }
        }
    }
    void basic(int randNum, double unit1, double unit2, double unit3)
    {
        randNum = Random.Range(0, 3);
        crit = Random.Range(1, 201);
        rounded = 10 * (enemyUnit.damage / 100f);
        if (rounded < 1) rounded = 1;
        enemyDamage = Mathf.RoundToInt(10 * rounded);

        if (randNum == 0 && !playerUnit1.isDead)
        {
            damageDone = enemyDamage - Mathf.RoundToInt(enemyDamage * (playerUnit1.defence / 100f));
            if (crit <= enemyUnit.luck)
            {
                damageDone *= 2;
            }
            damaged(playerUnit1, 0, damageDone);
            dialougeText.text = enemyUnit.unitName + " attacks Jorm for " + damageDone + " damage!";

        }
        else if (randNum == 1 && !playerUnit2.isDead)
        {
            damageDone = enemyDamage - Mathf.RoundToInt(enemyDamage * (playerUnit2.defence / 100f));
            if (crit <= enemyUnit.luck)
            {
                damageDone *= 2;
            }
            damaged(playerUnit2, 1, damageDone);
            dialougeText.text = enemyUnit.unitName + " attacks Hameeda for " + damageDone + " damage!";
        }
        else if (randNum == 2 && !playerUnit3.isDead)
        {

            damageDone = enemyDamage - Mathf.RoundToInt(enemyDamage * (playerUnit3.defence / 100f));
            if (crit <= enemyUnit.luck)
            {
                damageDone *= 2;
            }
            damaged(playerUnit3, 2, damageDone);
            dialougeText.text = enemyUnit.unitName + " attacks Exounos for " + damageDone + " damage!";

        }
        else
        {
            dialougeText.text = "The attack missed!";
        }
    }
}
    