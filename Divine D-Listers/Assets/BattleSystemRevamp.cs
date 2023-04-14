using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum BattleState1 { START, JORMTURN, HAMEEDATURN, EXOUNOSTURN, ENEMYTURN, WON, LOST, PAUSE }


public class BattleSystemRevamp : MonoBehaviour
{
    public BattleState state;

    public GameObject jormPrefab;
    public GameObject hameedaPrefab;
    public GameObject exounosPrefab;

    public battleStarter battleStart;

    private GameObject enemyPrefab2;
    private GameObject enemyPrefab;

    public GameObject jormHUD;
    public GameObject hameedaHUD;
    public GameObject exounosHUD;
    public GameObject attack;
    public GameObject runButton;
    public GameObject jormStats;
    public GameObject hameedaStats;
    public GameObject exounosStats;
    public GameObject winScreen;
    public GameObject loseScreen;
    public GameObject enemy1Select;
    public GameObject enemy2select;
    private string currentAttack;
    public Animator actualTransition;
    public Image background;
    public GameObject transition;

    private int jormDamage;
    private int hameedaDamage;
    private int enemyDamage;
    private int damageDone;
    private float rounded;
    private int crit;
    private bool isCrit;
    private int enemyhp;
    private int enemy2hp;
    private bool isEnemy1=false;
    private bool isEnemy1dead = false;
    private bool isEnemy2dead = false;
    private int qualityCounter = 0;
    private int PhaseCounter = 0;
    private int randNum;
    


    public Transform enemyBattleSpawn;
    public Transform enemyBattleSpawn2;
    public Transform jormBattleSpawn;
    public Transform hameedaBattleSpawn;
    public Transform exounosBattleSpawn;

    UnitStats playerUnit1;
    UnitStats playerUnit2;
    UnitStats playerUnit3;
    UnitStats enemyUnit;
    UnitStats enemyUnit2;

    private string[] speeds;

    public Text dialougeText;
    public Text initiaviteHUD;
    public Text jormLevel;
    public Text hameedaLevel;
    public Text exounosLevel;
    public Text jormHp;
    public Text hameedaHp;
    public Text exounosHp;
    public Text Jexp;
    public Text Eexp;
    public Text Hexp; 

    private int turnNum = 0;

    public newHealthBar jormHealthBar;
    public newHealthBar hameedaHealthBar;
    public newHealthBar exounosHealthBar;
    public Special specialMeter;

    public VectorValue currentPosition;

    public IEnumerator testing;

    public GameObject hitHurtScreen;
    private hitHurtManager hitHurtManager;


    // Start is called before the first frame update

    void Start()
    {
        transition.SetActive(true);
        actualTransition.SetBool("Transition1Done", true);
        state = BattleState.START;
        battleStart = GameObject.Find("BattleStarter").GetComponent<battleStarter>();
        setupBattle();
    }

    void setupBattle()
    {
        randNum = Random.Range(0, 10);
        if (randNum > 5)
        {
            battleStart.isMultiple= true;
        }
        else
        {
            battleStart.isMultiple= false;
        }

        if (battleStart.isMultiple )
        {
            speeds = new string[4];
        }
        else
        {
            speeds = new string[3];
        }
        jormHUD.SetActive(false);
        hameedaHUD.SetActive(false);
        exounosHUD.SetActive(false);
        enemy1Select.SetActive(false);
        enemy2select.SetActive(false);


        winScreen = GameObject.Find("Game Win");
        winScreen.SetActive(false);

        loseScreen = GameObject.Find("Game Lose");
        loseScreen.SetActive(false);

        enemyPrefab = battleStart.enemyMain.getRandomFighter();
        enemyPrefab2 = battleStart.enemyMain.getRandomFighter();

        background.sprite = battleStart.background;

        GameObject playerGO = Instantiate(jormPrefab, jormBattleSpawn);
        playerUnit1 = playerGO.GetComponent<Unit>().unitStats;

        GameObject player2GO = Instantiate(hameedaPrefab, hameedaBattleSpawn);
        playerUnit2 = player2GO.GetComponent<Unit>().unitStats;

        GameObject player3GO = Instantiate(exounosPrefab, exounosBattleSpawn);
        playerUnit3 = player3GO.GetComponent<Unit>().unitStats;

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleSpawn);
        enemyUnit = enemyGO.GetComponent<Unit>().unitStats;

        if (battleStart.isMultiple)
        {
            GameObject enemyGO2 = Instantiate(enemyPrefab2, enemyBattleSpawn2);
            enemyUnit2 = enemyGO2.GetComponent<Unit>().unitStats;
        }

        playerUnit1.currentHp = playerUnit1.maxHP; 
        playerUnit2.currentHp=playerUnit2.maxHP;
        playerUnit3.currentHp = playerUnit3.maxHP;
        enemyhp=enemyUnit.maxHP;
        isEnemy1dead= false;
        enemyUnit.speed = enemyUnit.defaultSpeed;
        if (battleStart.isMultiple)
        {
            enemy2hp = enemyUnit2.maxHP;
            isEnemy2dead = false;
            enemyUnit2.speed = enemyUnit2.defaultSpeed;
        }

        jormHealthBar.setMaxHealth(playerUnit1.maxHP);
        jormHealthBar.setHealth(playerUnit1.currentHp);

        hameedaHealthBar.setMaxHealth(playerUnit2.maxHP);
        hameedaHealthBar.setHealth(playerUnit2.currentHp);

        exounosHealthBar.setMaxHealth(playerUnit3.maxHP);
        exounosHealthBar.setHealth(playerUnit3.currentHp);


        specialMeter.setMaxMeter(10);
        

        jormLevel.text = "" + playerUnit1.unitLevel;
        hameedaLevel.text = "" + playerUnit2.unitLevel;
        exounosLevel.text = "" + playerUnit3.unitLevel;
        jormHp.text = playerUnit1.currentHp + "/" + playerUnit1.maxHP;
        hameedaHp.text = playerUnit2.currentHp + "/" + playerUnit2.maxHP;
        exounosHp.text = playerUnit3.currentHp + "/" + playerUnit3.maxHP;


        StartCoroutine(BattleBegin()); 
    }

    void battleSequence()
    {
        if (battleStart.isMultiple)
        {
            initiaviteHUD.text = "Order: " + speeds[0] + " " + speeds[1] + " " + speeds[2] + " " + speeds[3] + " " + speeds[4];
        }
        else
        {
            initiaviteHUD.text = "Order: " + speeds[0] + " " + speeds[1] + " " + speeds[2] + " " + speeds[3];
        }
        attack.SetActive(false);
        runButton.SetActive(false);
        if (battleStart.isMultiple)
        {
        if (turnNum > 4)
        {
            turnNum = 0;
            speeds = getInitiative();
        }
        }
        else
        {
            if (turnNum > 3)
            {
                turnNum = 0;
                speeds = getInitiative();
            }
        }
        
        Debug.Log(speeds[turnNum]);
        //make this a switch case later
        if (state != BattleState.WON)
        {
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
                    runButton.SetActive(true);
                    jormTurn();
                }
            }
            else if (speeds[turnNum] == "Ham.")
            {
                if (playerUnit2.isDead)
                {
                    turnNum++;
                    battleSequence();
                }
                else
                {
                    turnNum++;
                    state = BattleState.HAMEEDATURN;
                    attack.SetActive(true);
                    runButton.SetActive(true);
                    hameedaTurn();
                }
            }
            else if (speeds[turnNum] == "Ex.")
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
                    runButton.SetActive(true);
                    exounosTurn();
                }
            }
            else if (speeds[turnNum] == "Enemy1")
            {
                if (isEnemy1dead)
                {
                    turnNum++;
                    battleSequence();
                }
                else
                {
                    turnNum++;
                    state = BattleState.ENEMYTURN;
                    StartCoroutine(enemyCoroutine(enemyUnit));
                }
            }
            else if (speeds[turnNum] == "Enemy2")
            {
                if (isEnemy2dead)
                {
                    turnNum++;
                    battleSequence();
                }
                else
                {
                    turnNum++;
                    state = BattleState.ENEMYTURN;
                    StartCoroutine(enemyCoroutine(enemyUnit2));
                }
            }

        }
    }

    string[] getInitiative()
    {
        //take in all player and enemy names and speeds
        string[] tempArr;
        int[] entitySpeeds;
        if (battleStart.isMultiple)
        {
        tempArr =new string[] { "Jorm", "Ham.", "Ex.", "Enemy1", "Enemy2" };
        entitySpeeds =new int[]{ playerUnit1.speed, playerUnit2.speed, playerUnit3.speed, enemyUnit.speed, enemyUnit2.speed};
        }
        else
        {
           tempArr = new string[]{ "Jorm", "Ham.", "Ex.", "Enemy1"};
           entitySpeeds = new int[]{ playerUnit1.speed, playerUnit2.speed, playerUnit3.speed, enemyUnit.speed};
        }
        //sort it from highest to lowest

        bool swappedSomething = true;
        while (swappedSomething)
        {
            swappedSomething = false;
            if (battleStart.isMultiple)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (entitySpeeds[i] < entitySpeeds[i + 1])
                    {

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
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    if (entitySpeeds[i] < entitySpeeds[i + 1])
                    {

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
        if (isEnemy1dead && !battleStart.isMultiple)
        {
            state = BattleState.WON;
            StartCoroutine(TypeText("The " + enemyUnit.unitName + " has been slain! YOU WIN!"));
            if (battleStart.isMultiple)
            {
                playerUnit1.attemptLvlUp(enemyUnit.exp+enemyUnit2.exp, 1);
                playerUnit2.attemptLvlUp(enemyUnit.exp + enemyUnit2.exp, 2);
                playerUnit3.attemptLvlUp(enemyUnit.exp + enemyUnit2.exp, 3);
            }
            else
            {
                playerUnit1.attemptLvlUp(enemyUnit.exp, 1);
                playerUnit2.attemptLvlUp(enemyUnit.exp, 2);
                playerUnit3.attemptLvlUp(enemyUnit.exp, 3);
            }
            if (playerUnit1.leveledUp)
            {
                if(battleStart.isMultiple)
                {
                    Jexp.text = "+" + (enemyUnit.exp+enemyUnit2.exp) + " - LEVEL UP!";
                    playerUnit1.leveledUp = false;
                }
                else
                {
                Jexp.text = "+"+ enemyUnit.exp + " - LEVEL UP!";
                    playerUnit1.leveledUp = false;
                }
                
            }
            else
            {
                if (battleStart.isMultiple)
                {
                    Jexp.text = "+" + (enemyUnit.exp+enemyUnit2.exp);
                }
                else
                {
                Jexp.text = "+"+ enemyUnit.exp;
                }
                
            }
            if (playerUnit2.leveledUp)
            {
                if (battleStart.isMultiple)
                {
                    Hexp.text = "+" + (enemyUnit.exp + enemyUnit2.exp) + " - LEVEL UP!";
                    playerUnit2.leveledUp = false;
                }
                else
                {
                    Hexp.text = "+" + enemyUnit.exp + " - LEVEL UP!";
                    playerUnit2.leveledUp = false;
                }
            }
            else
            {
                if (battleStart.isMultiple)
                {
                    Hexp.text = "+" + (enemyUnit.exp + enemyUnit2.exp);
                }
                else
                {
                    Hexp.text = "+" + enemyUnit.exp;
                }
            }
            if (playerUnit3.leveledUp)
            {
                if (battleStart.isMultiple)
                {
                    Eexp.text = "+" + (enemyUnit.exp + enemyUnit2.exp) + " - LEVEL UP!";
                    playerUnit3.leveledUp = false;
                }
                else
                {
                    Eexp.text = "+" + enemyUnit.exp + " - LEVEL UP!";
                    playerUnit3.leveledUp = false;
                }
            }
            else
            {
                if (battleStart.isMultiple)
                {
                    Eexp.text = "+" + (enemyUnit.exp + enemyUnit2.exp);
                }
                else
                {
                    Eexp.text = "+" + enemyUnit.exp;
                }
            }

            StartCoroutine(winCoroutineWait());
        }
        else if (isEnemy1dead && isEnemy2dead)
        {
            state = BattleState.WON;
            StartCoroutine(TypeText("The " + enemyUnit.unitName + " has been slain! YOU WIN!"));
            //playerUnit1.AttemptLevelUp(1);
            //playerUnit2.AttemptLevelUp(2);
            //playerUnit3.AttemptLevelUp(3);
            if (playerUnit1.leveledUp)
            {
                Jexp.text = "+10 - LEVEL UP!";
            }
            else {
                Jexp.text = "+10";
            }
            if (playerUnit2.leveledUp)
            {
                Hexp.text = "+10 - LEVEL UP!";
            }
            else
            {
                Hexp.text = "+10";
            }
            if (playerUnit3.leveledUp)
            {
                Eexp.text = "+10 - LEVEL UP!";
            }
            else
            {
                Eexp.text = "+10";
            }

            StartCoroutine(winCoroutineWait());
        }
    }

    public void exitBattle() 
    {
        SceneManager.LoadScene(currentPosition.currentScene);
    }

    public void takeASeatButton()
    {
        if (!isEnemy1dead)
        {
            enemy1Select.SetActive(true);
        }
        if (battleStart.isMultiple)
        {
            if (!isEnemy2dead)
            {
                enemy2select.SetActive(true);
            }
        }
        currentAttack = "Take a Seat";
    }

    void takeASeat(UnitStats u)
    {   
        playerUnit1.isAttacking = true;
        Instantiate(hitHurtScreen, Vector3.zero, Quaternion.identity);
        hitHurtManager = GameObject.Find("Hit-Hurt(Clone)").GetComponent<hitHurtManager>();
        hitHurtManager.playerHit(playerUnit1, u);


        
        specialMeter.setMeter(specialMeter.getMeter() + 1);

        isCrit = false;
        crit = Random.Range(1, 201);
        jormHUD.SetActive(false);
        jormStats.SetActive(true);
        rounded = 8 * (playerUnit1.damage / 100f);
        if (rounded < 1) rounded = 1;
        jormDamage = Mathf.RoundToInt(8 * rounded);
        damageDone = jormDamage - Mathf.RoundToInt(jormDamage * (u.defence / 100f));
        Debug.Log(crit);
        if (crit <= playerUnit1.luck)
        {
            isCrit = true;
            damageDone *= 2;
        }
        if (isEnemy1)
        {
            enemyhp -= damageDone;
        }
        else
        {
            enemy2hp -= damageDone;
        }
        specialMeter.increaseMeter(1);
        attack.SetActive(false);
        Debug.Log(damageDone);
        StartCoroutine(playerCoroutineAttack(playerUnit1, damageDone, isCrit, u));
    }

    public void buttonEnemyOne()
    {
        isEnemy1 = true;
        enemy1Select.SetActive(false);
        enemy2select.SetActive(false);
        if (currentAttack == "Yawn")
        {
            yawn(enemyUnit);
        }
        else if (currentAttack == "Take a Seat")
        {
            takeASeat(enemyUnit);
        }
        else if (currentAttack == "Kohld Shoulder")
        {
            kohldShoulder(enemyUnit);
        }
    }

   public void buttonEnemyTwo()
    {  
        isEnemy1= false;
        enemy1Select.SetActive(false);
        enemy2select.SetActive(false);
        if (currentAttack == "Yawn")
        {
            yawn(enemyUnit2);
        }
        else if (currentAttack == "Take a Seat")
        {
            takeASeat(enemyUnit2);
        }
        else if (currentAttack == "Kohld Shoulder")
        {
            kohldShoulder(enemyUnit2);
        }
    }

    public void qualityAssurance()
    {

        playerUnit1.isAttacking = true;
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

    public void yawnButton()
    {
        if (!isEnemy1dead)
        {
            enemy1Select.SetActive(true);
        }
        if (battleStart.isMultiple)
        {
            if (!isEnemy2dead)
            {
                enemy2select.SetActive(true);
            }
        }
            currentAttack = "Yawn";
    }

    void yawn(UnitStats u)
    {   
        playerUnit3.isAttacking = true;
        Instantiate(hitHurtScreen, Vector3.zero, Quaternion.identity);
        hitHurtManager = GameObject.Find("Hit-Hurt(Clone)").GetComponent<hitHurtManager>();
        hitHurtManager.playerHit(playerUnit3, u);

        
        exounosHUD.SetActive(false);
        exounosStats.SetActive(true);
        if (u.speed >= (u.speed - Mathf.RoundToInt(u.speed * .25f)))
        {
            u.speed -= 1;
            StartCoroutine(TypeText("The " + u.unitName + " grows drowzy."));
        }
        else
        {
            StartCoroutine(TypeText("The " + u.unitName + " is drowzy enough."));
        }
        //code for attacks goes here
        attack.SetActive(false);
        StartCoroutine(playerCoroutineNeutral());
    }

    public void powerNap()
    {
        playerUnit3.isAttacking = true;
        exounosHUD.SetActive(false);
        exounosStats.SetActive(true);
        if (playerUnit1.currentHp < playerUnit1.maxHP)
        {
            playerUnit1.currentHp += 4;
            if (playerUnit1.currentHp > playerUnit1.maxHP && !playerUnit1.isDead)
            {
                playerUnit1.currentHp = playerUnit1.maxHP;
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

    public void kohldShoulderButton()
    {
        if (!isEnemy1dead)
        {
            enemy1Select.SetActive(true);
        }
        if (battleStart.isMultiple)
        {
            if (!isEnemy2dead)
            {
                enemy2select.SetActive(true);
            }
        }
        currentAttack = "Kohld Shoulder";
    }


    void kohldShoulder(UnitStats u)
    {   
        playerUnit2.isAttacking = true;
        Instantiate(hitHurtScreen, Vector3.zero, Quaternion.identity);
        hitHurtManager = GameObject.Find("Hit-Hurt(Clone)").GetComponent<hitHurtManager>();
        hitHurtManager.playerHit(playerUnit2, u);

        
        specialMeter.setMeter(specialMeter.getMeter() + 1f);
        isCrit = false;
        hameedaHUD.SetActive(false);
        hameedaStats.SetActive(true);
        crit = Random.Range(1, 201);
        rounded = 10 * (playerUnit2.damage / 100f);
        if (rounded < 1) rounded = 1;
        hameedaDamage = Mathf.RoundToInt(10 * rounded);
        damageDone = hameedaDamage - Mathf.RoundToInt(hameedaDamage * (u.defence / 100f));
        Debug.Log(crit);
        if (crit <= playerUnit2.luck)
        {
            isCrit = true;
            damageDone *= 2;
        }
        if (isEnemy1)
        {
            enemyhp -= damageDone;
        }
        else
        {
            enemy2hp -= damageDone;
        }
        specialMeter.increaseMeter(1);
        attack.SetActive(false);
        StartCoroutine(playerCoroutineAttack(playerUnit2, damageDone, isCrit,u));
        //code for attacks goes here
    }

    public void itsNotAPhase()
    {
        playerUnit2.isAttacking = true;       
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

    private IEnumerator playerCoroutineAttack(UnitStats u, int d, bool b, UnitStats e)
    {
        StartCoroutine(TypeText("Hit! " + u.unitName + " attacks " + e.unitName + " for " + d + " damage!"));

        if(isEnemy1 && enemyhp<=0)
        {
            isEnemy1dead = true;
        }
        else if (battleStart.isMultiple && enemy2hp<=0)
        {
            isEnemy2dead = true;
        }
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
        winScreen.SetActive(true);
    }

    private IEnumerator enemyCoroutine(UnitStats u)
    {
        //any code before yeild runs on first frame
        //yield return null;
        yield return new WaitForSeconds(2);

        StartCoroutine(TypeText("The " + u.unitName + " attacks!"));
        randNum = 0;

        double unit1 = playerUnit1.maxHP;
        double unit2 = playerUnit2.maxHP;
        double unit3 = playerUnit3.maxHP;
        //runs code up to this point on first frame then waits 3 seconds.
        yield return new WaitForSeconds(3);
        //after 3 seconds, picks up from here
        
        if (u.unitName == "Phoenix")
        {

            Pheonix(randNum, unit1, unit2, unit3);
        }
        else if (u.unitName == "Ammit")
        {

            Ammit(randNum, unit1, unit2, unit3);
        }
        else if (u.unitName == "Ra")
        {

            Ra(randNum, unit1, unit2, unit3);
        }
        else
        {
            basic(randNum, unit1, unit2, unit3, u);

        }
        yield return new WaitForSeconds(2);
        //any code after runs one frame after the first frame;
        battleSequence();
    }

    public IEnumerator run() {
        
        int randNum = Random.Range(0, 3);

        if (randNum == 0)
        {
            StartCoroutine(TypeText("Jorm and the party got away safely!"));
            yield return new WaitForSeconds(3);
            exitBattle();
        }
        else {
            StartCoroutine(TypeText("You couldnt get away!"));
            yield return new WaitForSeconds(3);
            battleSequence();
        }

    }

    public void callTheRunFuntion() 
    {
        runButton.SetActive(false);
        StartCoroutine(run()); 
    }

    void isDead(UnitStats player)
    {
        if (player.currentHp <= 0)
        {
            player.isDead = true;
        }

        if (playerUnit1.isDead && playerUnit2.isDead && playerUnit3.isDead)
        {
            state = BattleState.LOST;
            loseScreen.SetActive(true);
        }
    }

    public IEnumerator BattleBegin() {
        yield return new WaitForSeconds(0.5f);
        transition.SetActive(false);
        yield return new WaitForSeconds(1);
        StartCoroutine(TypeText(enemyUnit.unitName + " approaches!"));
        yield return new WaitForSeconds(3);
        speeds = getInitiative();
        battleSequence();
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

    void damaged(UnitStats player, int target, int damage)
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
        specialMeter.increaseMeter(1);
        isDead(player);
    }

    void Pheonix(int randNum, double unit1, double unit2, double unit3)
    {
        randNum = Random.Range(0, 10);
        if (playerUnit1.currentHp / unit1 > .7 && playerUnit2.currentHp / unit2 > .7 && playerUnit3.currentHp / unit3 > .7 && randNum < 7)
        {
            rounded = 5 * (enemyUnit.damage / 100f);
            if (rounded < 1) rounded = 1;
            enemyDamage = Mathf.RoundToInt(5 * rounded);
            StartCoroutine(TypeText(enemyUnit.unitName + " breathes fire"));
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
                Instantiate(hitHurtScreen, Vector3.zero, Quaternion.identity);
                hitHurtManager = GameObject.Find("Hit-Hurt(Clone)").GetComponent<hitHurtManager>();
                hitHurtManager.playerHurt(playerUnit1, enemyUnit);
                damageDone = enemyDamage - Mathf.RoundToInt(enemyDamage * (playerUnit1.defence / 100f));
                if (crit <= enemyUnit.luck)
                {
                    damageDone *= 2;
                }
                damaged(playerUnit1, 0, damageDone);

                playerUnit1.isHit = true;
                StartCoroutine(TypeText(enemyUnit.unitName + " attacks Jorm for " + damageDone + " damage!"));


            }
            else if (randNum == 1 && !playerUnit2.isDead)
            {
                Instantiate(hitHurtScreen, Vector3.zero, Quaternion.identity);
                hitHurtManager = GameObject.Find("Hit-Hurt(Clone)").GetComponent<hitHurtManager>();
                hitHurtManager.playerHurt(playerUnit2, enemyUnit);
                damageDone = enemyDamage - Mathf.RoundToInt(enemyDamage * (playerUnit2.defence / 100f));
                if (crit <= enemyUnit.luck)
                {
                    damageDone *= 2;
                }
                damaged(playerUnit2, 1, damageDone);
				
                playerUnit2.isHit = true;
                StartCoroutine(TypeText(enemyUnit.unitName + " attacks Hameeda for " + damageDone + " damage!"));


            }
            else if (randNum == 2 && !playerUnit3.isDead)
            {
                Instantiate(hitHurtScreen, Vector3.zero, Quaternion.identity);
                hitHurtManager = GameObject.Find("Hit-Hurt(Clone)").GetComponent<hitHurtManager>();
                hitHurtManager.playerHurt(playerUnit3, enemyUnit);
                damageDone = enemyDamage - Mathf.RoundToInt(enemyDamage * (playerUnit3.defence / 100f));
                if (crit <= enemyUnit.luck)
                {
                    damageDone *= 2;
                }
                damaged(playerUnit3, 2, damageDone);

                playerUnit3.isHit = true;
                StartCoroutine(TypeText(enemyUnit.unitName + " attacks Exounos for " + damageDone + " damage!"));


            }
            else
            {
                StartCoroutine(TypeText("The attack missed!"));
            }
        }
    }
    void basic(int randNum, double unit1, double unit2, double unit3, UnitStats u)
    {
        randNum = Random.Range(0, 3);
        crit = Random.Range(1, 201);
        rounded = 10 * (u.damage / 100f);
        if (rounded < 1) rounded = 1;
        enemyDamage = Mathf.RoundToInt(10 * rounded);

        if (randNum == 0 && !playerUnit1.isDead)
        {
            Instantiate(hitHurtScreen, Vector3.zero, Quaternion.identity);
            hitHurtManager = GameObject.Find("Hit-Hurt(Clone)").GetComponent<hitHurtManager>();
            hitHurtManager.playerHurt(playerUnit1, u);
            damageDone = enemyDamage - Mathf.RoundToInt(enemyDamage * (playerUnit1.defence / 100f));
            if (crit <= u.luck)
            {
                damageDone *= 2;
            }
            damaged(playerUnit1, 0, damageDone);

            playerUnit1.isHit = true;
            StartCoroutine(TypeText(u.unitName + " attacks Jorm for " + damageDone + " damage!"));
        }
        else if (randNum == 1 && !playerUnit2.isDead)
        {
            Instantiate(hitHurtScreen, Vector3.zero, Quaternion.identity);
            hitHurtManager = GameObject.Find("Hit-Hurt(Clone)").GetComponent<hitHurtManager>();
            hitHurtManager.playerHurt(playerUnit2,u);
            damageDone = enemyDamage - Mathf.RoundToInt(enemyDamage * (playerUnit2.defence / 100f));
            if (crit <= u.luck)
            {
                damageDone *= 2;
            }
            damaged(playerUnit2, 1, damageDone);

            playerUnit2.isHit = true;

            StartCoroutine(TypeText(u.unitName + " attacks Hameeda for " + damageDone + " damage!"));

        }
        else if (randNum == 2 && !playerUnit3.isDead)
        {
            Instantiate(hitHurtScreen, Vector3.zero, Quaternion.identity);
            hitHurtManager = GameObject.Find("Hit-Hurt(Clone)").GetComponent<hitHurtManager>();
            hitHurtManager.playerHurt(playerUnit3, u);
            damageDone = enemyDamage - Mathf.RoundToInt(enemyDamage * (playerUnit3.defence / 100f));
            if (crit <= u.luck)
            {
                damageDone *= 2;
            }
            damaged(playerUnit3, 2, damageDone);

            playerUnit3.isHit = true;
            StartCoroutine(TypeText(u.unitName + " attacks Exounos for " + damageDone + " damage!"));

        }
        else
        {
            StartCoroutine(TypeText("The attack missed!"));
        }
    }
    void Ammit(int randNum, double unit1, double unit2, double unit3)
    {
        randNum = Random.Range(0, 10);
        if (playerUnit1.currentHp / unit1 > .6 && playerUnit2.currentHp / unit2 > .6 && playerUnit3.currentHp / unit3 > .6 && randNum < 7)
        {
            rounded = 8 * (enemyUnit.damage / 100f);
            if (rounded < 1) rounded = 1;
            enemyDamage = Mathf.RoundToInt(5 * rounded);
            StartCoroutine(TypeText(enemyUnit.unitName + " slams the ground and rocks fall from the ceiling."));
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
                Instantiate(hitHurtScreen, Vector3.zero, Quaternion.identity);
                hitHurtManager = GameObject.Find("Hit-Hurt(Clone)").GetComponent<hitHurtManager>();
                hitHurtManager.playerHurt(playerUnit1, enemyUnit);
                damageDone = enemyDamage - Mathf.RoundToInt(enemyDamage * (playerUnit1.defence / 100f));
                if (crit <= enemyUnit.luck)
                {
                    damageDone *= 2;
                }
                damaged(playerUnit1, 0, damageDone);
                StartCoroutine(TypeText(enemyUnit.unitName + " attacks Jorm for " + damageDone + " damage!"));

            }
            else if (randNum == 1 && !playerUnit2.isDead)
            {
                Instantiate(hitHurtScreen, Vector3.zero, Quaternion.identity);
                hitHurtManager = GameObject.Find("Hit-Hurt(Clone)").GetComponent<hitHurtManager>();
                hitHurtManager.playerHurt(playerUnit2, enemyUnit);
                damageDone = enemyDamage - Mathf.RoundToInt(enemyDamage * (playerUnit2.defence / 100f));
                if (crit <= enemyUnit.luck)
                {
                    damageDone *= 2;
                }
                damaged(playerUnit2, 1, damageDone);
                StartCoroutine(TypeText(enemyUnit.unitName + " attacks Hameeda for " + damageDone + " damage!"));

            }
            else if (randNum == 2 && !playerUnit3.isDead)
            {
                Instantiate(hitHurtScreen, Vector3.zero, Quaternion.identity);
                hitHurtManager = GameObject.Find("Hit-Hurt(Clone)").GetComponent<hitHurtManager>();
                hitHurtManager.playerHurt(playerUnit3, enemyUnit);
                damageDone = enemyDamage - Mathf.RoundToInt(enemyDamage * (playerUnit3.defence / 100f));
                if (crit <= enemyUnit.luck)
                {
                    damageDone *= 2;
                }
                damaged(playerUnit3, 2, damageDone);
                StartCoroutine(TypeText(enemyUnit.unitName + " attacks Exounos for " + damageDone + " damage!"));

            }
            else
            {
                StartCoroutine(TypeText("The attack missed!"));
            }
        }
    }

void Ra(int randNum, double unit1, double unit2, double unit3)
{
    randNum = Random.Range(0, 10);
    if (playerUnit1.currentHp / unit1 > .6 && playerUnit2.currentHp / unit2 > .6 && playerUnit3.currentHp / unit3 > .6 && randNum < 7)
    {
        rounded = 8 * (enemyUnit.damage / 100f);
        if (rounded < 1) rounded = 1;
        enemyDamage = Mathf.RoundToInt(5 * rounded);
        StartCoroutine(TypeText(enemyUnit.unitName + "'s staff shines with a blinding light as you feel why he controls the sun."));
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
                Instantiate(hitHurtScreen, Vector3.zero, Quaternion.identity);
                hitHurtManager = GameObject.Find("Hit-Hurt(Clone)").GetComponent<hitHurtManager>();
                hitHurtManager.playerHurt(playerUnit1, enemyUnit);
                damageDone = enemyDamage - Mathf.RoundToInt(enemyDamage * (playerUnit1.defence / 100f));
            if (crit <= enemyUnit.luck)
            {
                damageDone *= 2;
            }
            damaged(playerUnit1, 0, damageDone);
            StartCoroutine(TypeText(enemyUnit.unitName + " attacks Jorm for " + damageDone + " damage!"));

        }
        else if (randNum == 1 && !playerUnit2.isDead)
        {
                Instantiate(hitHurtScreen, Vector3.zero, Quaternion.identity);
                hitHurtManager = GameObject.Find("Hit-Hurt(Clone)").GetComponent<hitHurtManager>();
                hitHurtManager.playerHurt(playerUnit2, enemyUnit);
                damageDone = enemyDamage - Mathf.RoundToInt(enemyDamage * (playerUnit2.defence / 100f));
            if (crit <= enemyUnit.luck)
            {
                damageDone *= 2;
            }
            damaged(playerUnit2, 1, damageDone);
            StartCoroutine(TypeText(enemyUnit.unitName + " attacks Hameeda for " + damageDone + " damage!"));

        }
        else if (randNum == 2 && !playerUnit3.isDead)
        {
                Instantiate(hitHurtScreen, Vector3.zero, Quaternion.identity);
                hitHurtManager = GameObject.Find("Hit-Hurt(Clone)").GetComponent<hitHurtManager>();
                hitHurtManager.playerHurt(playerUnit3, enemyUnit);
                damageDone = enemyDamage - Mathf.RoundToInt(enemyDamage * (playerUnit3.defence / 100f));
            if (crit <= enemyUnit.luck)
            {
                damageDone *= 2;
            }
            damaged(playerUnit3, 2, damageDone);
            StartCoroutine(TypeText(enemyUnit.unitName + " attacks Exounos for " + damageDone + " damage!"));

        }
        else
        {
            StartCoroutine(TypeText("The attack missed!"));
        }
    }

    void special(string unit) { 
        if (specialMeter.getMeter() == specialMeter.getMaxMeter()) 
        {
            specialMeter.setMeter(0);
            if(unit == "Jorm")
                {
                    damageDone = 18;
                    isCrit = false;
                    if (battleStart.isMultiple)
                    {
                        StartCoroutine(playerCoroutineAttack(playerUnit1, damageDone, isCrit, enemyUnit2));
                    }
                    StartCoroutine(playerCoroutineAttack(playerUnit1, damageDone, isCrit, enemyUnit));
                    
                   
                }
            else if(unit== "Hameeda")
                {
                    damageDone = 22;
                    isCrit = false;
                    if (battleStart.isMultiple)
                    {
                        StartCoroutine(playerCoroutineAttack(playerUnit2, damageDone, isCrit, enemyUnit2));
                    }
                    StartCoroutine(playerCoroutineAttack(playerUnit2, damageDone, isCrit, enemyUnit));
                }
            else if(unit== "Exounos")
                {
                    playerUnit1.isDead= false;
                    playerUnit2.isDead= false;
                    playerUnit3.isDead = false;
                    playerUnit1.currentHp = playerUnit1.maxHP;
                    playerUnit2.currentHp = playerUnit2.maxHP;
                    playerUnit3.currentHp = playerUnit3.maxHP;
                }
            //do the cool attack thing
        }
    }
}
}
