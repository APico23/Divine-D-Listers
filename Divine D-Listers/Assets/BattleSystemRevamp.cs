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

    public Quest ammitBeat;
    public Quest raBeat;
    public Quest phoenixBeat;

    private inventory playerInventory;

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
    public GameObject attackLocked;
    public GameObject runLocked;

    private int jormDamage;
    private int hameedaDamage;
    private int exounosDamage;
    private int enemyDamage;
    private int damageDone;
    private float rounded;
    private int crit;
    private bool isCrit;
    private float enemyhp;
    private float enemy2hp;
    private bool isEnemy1=false;
    private bool isEnemy1dead = false;
    private bool isEnemy2dead = false;
    private int qualityCounter = 0;
    private int PhaseCounter = 0;
    private int builtCounter=0;
    private int kholCounter = 0;
    private int randNum;
    private int turnTracker = 0;
    private bool isTutorial=false;
    private bool isBoss = false;

    public GameObject ragnarockingChair;
    public GameObject mythiKohl;
    public GameObject foodComa;
    public GameObject mainUI;

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

    SpriteRenderer enemysprite;
    SpriteRenderer enemy2sprite;

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
        playerInventory = GameObject.Find("PlayerInfo").GetComponent<PlayerInfo>().inventory;
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

        if(battleStart.enemyMain.getLength() == 1) { 
            battleStart.isMultiple= false;
        }
        if (battleStart.enemyMain.getLength() == 2)
        {
            battleStart.isMultiple = true;
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

        attackLocked.SetActive(false);
        runLocked.SetActive(false);

        winScreen = GameObject.Find("Game Win");
        winScreen.SetActive(false);

        loseScreen = GameObject.Find("Game Lose");
        loseScreen.SetActive(false);

        enemyPrefab = battleStart.enemyMain.getRandomFighter();
        enemyPrefab2 = battleStart.enemyMain.getRandomFighter();

        if (battleStart.enemyMain.getLength() == 2)
        {
            enemyPrefab = battleStart.enemyMain.getFighter(0);
            enemyPrefab2 = battleStart.enemyMain.getFighter(1);
        }
        else
        {
            enemyPrefab = battleStart.enemyMain.getRandomFighter();
            enemyPrefab2 = battleStart.enemyMain.getRandomFighter();
        }

            background.sprite = battleStart.background;

        GameObject playerGO = Instantiate(jormPrefab, jormBattleSpawn);
        playerUnit1 = playerGO.GetComponent<Unit>().unitStats;

        GameObject player2GO = Instantiate(hameedaPrefab, hameedaBattleSpawn);
        playerUnit2 = player2GO.GetComponent<Unit>().unitStats;

        GameObject player3GO = Instantiate(exounosPrefab, exounosBattleSpawn);
        playerUnit3 = player3GO.GetComponent<Unit>().unitStats;

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleSpawn);
        enemyUnit = enemyGO.GetComponent<Unit>().unitStats;
        enemysprite= enemyGO.GetComponent<SpriteRenderer>();

        if (battleStart.isMultiple)
        {
            GameObject enemyGO2 = Instantiate(enemyPrefab2, enemyBattleSpawn2);
            enemyUnit2 = enemyGO2.GetComponent<Unit>().unitStats;
            enemy2sprite = enemyGO2.GetComponent<SpriteRenderer>();
        }

        playerUnit1.currentHp = playerUnit1.maxHP; 
        playerUnit2.currentHp=playerUnit2.maxHP;
        playerUnit3.currentHp = playerUnit3.maxHP;
        playerUnit1.isDead= false;
        playerUnit2.isDead= false;
        playerUnit3.isDead= false;
        playerUnit1.statusEffect = false;
        playerUnit2.statusEffect = false;
        playerUnit3.statusEffect = false;
        playerUnit1.onFire = false;
        playerUnit2.onFire = false;
        playerUnit3.onFire = false;
        playerUnit1.poisoned = false;
        playerUnit2.poisoned = false;
        playerUnit3.poisoned = false;
        playerUnit1.statusCounter = 0;
        playerUnit2.statusCounter = 0;
        playerUnit3.statusCounter = 0;


        if (qualityCounter > 0)
        {
            playerUnit1.defence -= qualityCounter;
            playerUnit2.defence -= qualityCounter;
            playerUnit3.defence -= qualityCounter;
            qualityCounter= 0;
        }
        if (PhaseCounter > 0)
        {
            playerUnit2.damage-=PhaseCounter;
            PhaseCounter = 0;
        }
        if (builtCounter > 0)
        {
            playerUnit1.defence -= builtCounter*2;
            builtCounter = 0;
        }
        if (kholCounter > 0)
        {
            playerUnit2.luck -= kholCounter;
            kholCounter = 0;
        }
        enemyhp =enemyUnit.maxHP;
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
        attackLocked.SetActive(true);
        runLocked.SetActive(true);
        if (battleStart.isMultiple)
        {
        if (turnNum > 4)
        {
            turnNum = 0;
            speeds = getInitiative();
            turnTracker++;
        }
        }
        else
        {
            if (turnNum > 3)
            {
                turnNum = 0;
                speeds = getInitiative();
                turnTracker++;
            }
        }
        
        Debug.Log(speeds[turnNum]);
        //make this a switch case later
        if (state != BattleState.WON)
        {
            exounosStats.SetActive(true);
            jormStats.SetActive(true);
            hameedaStats.SetActive(true);
            if (speeds[turnNum] == "Jorm")
            {               
                if (playerUnit1.statusEffect)
                {
                    if(playerUnit1.onFire)
                    {
                        burn(playerUnit1, 0);
                    }
                    else if(playerUnit1.poisoned)
                    {
                        melt(playerUnit1, 0);
                    }
                }
                if (playerUnit1.isDead)
                {
                    turnNum++;
                    battleSequence();
                }
                else
                {
                    turnNum++;
                    state = BattleState.JORMTURN;
                    attackLocked.SetActive(false);
                    if (!isBoss)
                    {
                        runLocked.SetActive(false);
                        runButton.SetActive(true);
                    }
                    attack.SetActive(true);
                    if (playerUnit1.onFire )
                    {
                        StartCoroutine(yetAnotherCR("Jorm"));
                    }
                    else if (playerUnit1.poisoned)
                    {
                        StartCoroutine(yetAnotherCR("Jorm"));
                    }
                    else
                    {
                        jormTurn();
                    }
                }
            }
            else if (speeds[turnNum] == "Ham.")
            {
                if (playerUnit2.statusEffect)
                {
                    if (playerUnit2.onFire)
                    {
                        burn(playerUnit2, 1);
                    }
                    else if (playerUnit2.poisoned)
                    {
                        melt(playerUnit2, 1);
                    }
                }
                if (playerUnit2.isDead)
                {
                    turnNum++;
                    battleSequence();
                }
                else
                {
                    turnNum++;
                    state = BattleState.HAMEEDATURN;
                    attackLocked.SetActive(false);
                    if (!isBoss)
                    {
                        runLocked.SetActive(false);
                        runButton.SetActive(true);
                    }
                    attack.SetActive(true);
                    if (playerUnit2.onFire || isTutorial)
                    {
                        StartCoroutine(yetAnotherCR("Hameeda"));
                    }
                    else if (playerUnit2.poisoned)
                    {
                        StartCoroutine(yetAnotherCR("Hameeda"));
                    }
                    else
                    {
                        hameedaTurn();
                    }
                }
            }
            else if (speeds[turnNum] == "Ex.")
            {
                if (playerUnit3.statusEffect)
                {
                    if (playerUnit3.onFire)
                    {
                        burn(playerUnit3, 2);
                    }
                    else if (playerUnit3.poisoned)
                    {
                        melt(playerUnit3, 2);
                    }
                }
                if (playerUnit3.isDead)
                {
                    turnNum++;
                    battleSequence();
                }
                else
                {
                    turnNum++;
                    state = BattleState.EXOUNOSTURN;
                    attackLocked.SetActive(false);
                    if (!isBoss)
                    {
                        runLocked.SetActive(false);
                        runButton.SetActive(true);
                    }
                    attack.SetActive(true);
                    if (playerUnit3.onFire)
                    {
                        StartCoroutine(yetAnotherCR("Exounos"));
                    }
                    else if (playerUnit3.poisoned)
                    {
                        StartCoroutine(yetAnotherCR("Exounos"));
                    }
                    else
                    {
                        exounosTurn();
                    }
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
            playerUnit1.attemptLvlUp(enemyUnit.exp, 1);
            playerUnit2.attemptLvlUp(enemyUnit.exp, 2);
            playerUnit3.attemptLvlUp(enemyUnit.exp, 3);
            if (playerUnit1.leveledUp)
            {
                Jexp.text = "+"+ enemyUnit.exp + " - LEVEL UP!";
                playerUnit1.leveledUp = false;
            }
            else
            {
                Jexp.text = "+"+ enemyUnit.exp;
            }
            if (playerUnit2.leveledUp)
            {
                    Hexp.text = "+" + enemyUnit.exp + " - LEVEL UP!";
                    playerUnit2.leveledUp = false;
            }
            else
            {
                    Hexp.text = "+" + enemyUnit.exp;
            }
            if (playerUnit3.leveledUp)
            {
                    Eexp.text = "+" + enemyUnit.exp + " - LEVEL UP!";
                    playerUnit3.leveledUp = false;
            }
            else
            {
                    Eexp.text = "+" + enemyUnit.exp;
            }
            playerInventory.playerGold += enemyUnit.gold;
            //winScreen.transform.Find("Gold count").GetComponent<Text>().text = "" + enemyUnit.gold;
            StartCoroutine(winCoroutineWait());
        }
        else if (isEnemy1dead && isEnemy2dead)
        {
            state = BattleState.WON;
            StartCoroutine(TypeText("The " + enemyUnit.unitName + " has been slain! YOU WIN!"));
            playerUnit1.attemptLvlUp(enemyUnit.exp + enemyUnit2.exp, 1);
            playerUnit2.attemptLvlUp(enemyUnit.exp + enemyUnit2.exp, 2);
            playerUnit3.attemptLvlUp(enemyUnit.exp + enemyUnit2.exp, 3);
            if (playerUnit1.leveledUp)
            {
                Jexp.text = "+" + (enemyUnit.exp + enemyUnit2.exp) + " - LEVEL UP!";
                playerUnit1.leveledUp = false;
            }
            else {
                Jexp.text = "+" + (enemyUnit.exp + enemyUnit2.exp);
            }
            if (playerUnit2.leveledUp)
            {
                Hexp.text = "+" + (enemyUnit.exp + enemyUnit2.exp) + " - LEVEL UP!";
                playerUnit2.leveledUp = false;
            }
            else
            {
                Hexp.text = "+" + (enemyUnit.exp + enemyUnit2.exp);
            }
            if (playerUnit3.leveledUp)
            {
                Eexp.text = "+" + (enemyUnit.exp + enemyUnit2.exp) + " - LEVEL UP!";
                playerUnit3.leveledUp = false;
            }
            else
            {
                Eexp.text = "+" + (enemyUnit.exp + enemyUnit2.exp);
            }
            playerInventory.playerGold += (enemyUnit.gold + enemyUnit2.gold);
            //winScreen.transform.Find("Gold count").GetComponent<Text>().text = "" + (enemyUnit.gold + enemyUnit2.gold);
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
        //specialMeter.setMeter(specialMeter.getMaxMeter());

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
        attackLocked.SetActive(true);
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
        else if (currentAttack == "Sleep")
        {
            sleep(enemyUnit);
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
        else if (currentAttack == "Sleep")
        {
            sleep(enemyUnit2);
        }
    }

    public void qualityAssurance()
    {

        playerUnit1.isAttacking = true;
        jormHUD.SetActive(false);
        jormStats.SetActive(true);
        if (qualityCounter<5)
        {
            playerUnit1.defence += 1;
        }
        if (qualityCounter < 5)
        {
            playerUnit2.defence += 1;
        }
        if (qualityCounter < 5)
        {
            playerUnit3.defence += 1;
        }
        StartCoroutine(TypeText("Jorm makes sure the party is safe. Just a few extra nails in place."));
        //code for attacks goes here
        if(qualityCounter < 5)
        {
            qualityCounter++;
        }
        StartCoroutine(playerCoroutineNeutral());

    }
    public void builtToLast()
    {
        playerUnit1.isAttacking = true;
        jormHUD.SetActive(false);
       jormStats.SetActive(true);
        if (builtCounter<5)
        {
            playerUnit1.defence += 2;
            StartCoroutine(TypeText(playerUnit1.unitName + " builds up his defence with chairs."));
        }
        else
        {
            StartCoroutine(TypeText(playerUnit1.unitName + " has become as fortified as possible."));
        }
        //code for attacks goes here
        if (builtCounter < 5)
        {
            builtCounter++;
        }
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
       
        StartCoroutine(playerCoroutineNeutral());
    }

    public void SleepButton()
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
        currentAttack = "Sleep";
    }

    void sleep(UnitStats u)
    {
        playerUnit3.isAttacking = true;
        Instantiate(hitHurtScreen, Vector3.zero, Quaternion.identity);
        hitHurtManager = GameObject.Find("Hit-Hurt(Clone)").GetComponent<hitHurtManager>();
        hitHurtManager.playerHit(playerUnit3, u);


        exounosHUD.SetActive(false);
        exounosStats.SetActive(true);
        specialMeter.setMeter(specialMeter.getMeter() + 1f);
        isCrit = false;
        crit = Random.Range(1, 201);
        rounded = 6 * (playerUnit3.damage / 100f);
        if (rounded < 1) rounded = 1;
        exounosDamage = Mathf.RoundToInt(6 * rounded);
        damageDone = exounosDamage - Mathf.RoundToInt(exounosDamage * (u.defence / 100f));
        Debug.Log(crit);
        if (crit <= playerUnit3.luck)
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
        StartCoroutine(playerCoroutineAttack(playerUnit3, damageDone, isCrit, u));

    }
    public void powerNap()
    {
        playerUnit3.isAttacking = true;
        exounosHUD.SetActive(false);
        exounosStats.SetActive(true);
        if (playerUnit1.currentHp < playerUnit1.maxHP)
        {
            playerUnit1.currentHp += (4 + playerUnit3.unitLevel);
            if (playerUnit1.currentHp > playerUnit1.maxHP && !playerUnit1.isDead)
            {
                playerUnit1.currentHp = playerUnit1.maxHP;
            }
            jormHp.text = playerUnit1.currentHp + "/" + playerUnit1.maxHP;
            jormHealthBar.setHealth(playerUnit1.currentHp);
        }
        if (playerUnit2.currentHp < playerUnit2.maxHP && !playerUnit2.isDead)
        {
            playerUnit2.currentHp += (4 + playerUnit3.unitLevel);
            if (playerUnit2.currentHp > playerUnit2.maxHP)
            {
                playerUnit2.currentHp = playerUnit2.maxHP;
            }
            hameedaHp.text = playerUnit2.currentHp + "/" + playerUnit2.maxHP;
            hameedaHealthBar.setHealth(playerUnit2.currentHp);
        }
        if (playerUnit3.currentHp < playerUnit3.maxHP && !playerUnit3.isDead)
        {
            playerUnit3.currentHp += (4+playerUnit3.unitLevel);
            if (playerUnit3.currentHp > playerUnit3.maxHP)
            {
                playerUnit3.currentHp = playerUnit3.maxHP;
            }
            exounosHp.text = playerUnit3.currentHp + "/" + playerUnit3.maxHP;
            exounosHealthBar.setHealth(playerUnit3.currentHp);
        }
        StartCoroutine(TypeText("The party dozes off for a moment before waking rejuvinated."));
        //code for attacks goes here
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
        StartCoroutine(playerCoroutineAttack(playerUnit2, damageDone, isCrit,u));
        //code for attacks goes here
    }

    public void itsNotAPhase()
    {
        playerUnit2.isAttacking = true;       
        hameedaHUD.SetActive(false);
        hameedaStats.SetActive(true);
        if (PhaseCounter<5)
        {
            playerUnit2.damage += 1;
            StartCoroutine(TypeText(playerUnit2.unitName + " gathers magical energy."));
        }
        else
        {
            StartCoroutine(TypeText(playerUnit2.unitName + " has gained the most energy she can handle."));
        }
        //code for attacks goes here
        if (PhaseCounter < 5)
        {
            PhaseCounter++;
        }
        StartCoroutine(playerCoroutineNeutral());
    }

    public void kholLuck()
    {
        playerUnit2.isAttacking = true;
        hameedaHUD.SetActive(false);
        hameedaStats.SetActive(true);
        if (kholCounter<5)
        {
            playerUnit2.luck += 1;
            StartCoroutine(TypeText(playerUnit2.unitName + " applies more khol."));
        }
        else
        {
            StartCoroutine(TypeText(playerUnit2.unitName + " is fabulous enough."));
        }
        //code for attacks goes here
        if (kholCounter < 5)
        {
            kholCounter++;
        }
        StartCoroutine(playerCoroutineNeutral());
    }

    private IEnumerator playerCoroutineAttack(UnitStats u, int d, bool b, UnitStats e)
    {
        attack.SetActive(false);
        attackLocked.SetActive(true);
        runButton.SetActive(false);
        runLocked.SetActive(true);
        exounosHUD.SetActive(false);
        jormHUD.SetActive(false);
        hameedaHUD.SetActive(false);
        enemy1Select.SetActive(false);
        enemy2select.SetActive(false);
        StartCoroutine(TypeText("Hit! " + u.unitName + " attacks " + e.unitName + " for " + d + " damage!"));

        if(isEnemy1 && enemyhp<=0)
        {
            isEnemy1dead = true;
            enemysprite.GetComponent<SpriteRenderer>().enabled = false;
        }
        else if (battleStart.isMultiple && enemy2hp<=0)
        {
            isEnemy2dead = true;
            enemy2sprite.GetComponent<SpriteRenderer>().enabled = false;
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
        attack.SetActive(false);
        attackLocked.SetActive(true);
        runButton.SetActive(false);
        runLocked.SetActive(true);
        exounosHUD.SetActive(false);
        jormHUD.SetActive(false);
        hameedaHUD.SetActive(false);
        enemy1Select.SetActive(false);
        enemy2select.SetActive(false);
        yield return new WaitForSeconds(3);
            state = BattleState.PAUSE;
            isBattleWon();
            battleSequence();
    }

    private IEnumerator winCoroutineWait()
    {
        if (enemyUnit.name == "Phoenix")
        {
            phoenixBeat.isCompleted= true;
        }
        if (enemyUnit.name == "Ammit")
        {
            ammitBeat.isCompleted= true;
        }
        if (enemyUnit.name == "Ra")
        {
            raBeat.isCompleted= true;
        }
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
            isBoss = true;
            Phoenix(randNum);
        }
        else if (u.unitName == "Ammit")
        {
            isBoss= true;
            Ammit(randNum);
        }
        else if (u.unitName == "Ra")
        {
            isBoss= true;
            Ra(randNum);
        }
        else if (u.unitName == "Punching Bag")
        {
            isTutorial = true;
            isBoss= true;
            punchingBag();
        }
        else if (u.unitName == "Anubis")
        {
            isBoss = true;
            Anubis(randNum);
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
        runLocked.SetActive(true);
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
        player.currentHp -= damage;
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

    void Phoenix(int randNum)
    {
        randNum = Random.Range(0, 10);        
        if (randNum<7 && enemyhp/enemyUnit.maxHP<.5)
        {
            enemyhp += 15;
            if(enemyhp>enemyUnit.maxHP)
            {
                enemyhp = enemyUnit.maxHP;
            }
            StartCoroutine(TypeText(enemyUnit.unitName + " wraps itself in fire and some of its wounds heal"));
        }
        else if (randNum < 7)
        {
            rounded = 7 * (enemyUnit.damage / 100f);
            if (rounded < 1) rounded = 1;
            enemyDamage = Mathf.RoundToInt(7 * rounded);
            StartCoroutine(TypeText(enemyUnit.unitName + " breathes fire"));
            damageDone = enemyDamage - Mathf.RoundToInt(enemyDamage * (playerUnit1.defence / 100f));
            damaged(playerUnit1, 0, damageDone);
            if (!playerUnit1.isDead)
            {
                ignite(playerUnit1);
            }
            damageDone = enemyDamage - Mathf.RoundToInt(enemyDamage * (playerUnit2.defence / 100f));
            damaged(playerUnit2, 1, damageDone);
            if (!playerUnit2.isDead)
            {
                ignite(playerUnit2);
            }
            damageDone = enemyDamage - Mathf.RoundToInt(enemyDamage * (playerUnit3.defence / 100f));
            damaged(playerUnit3, 2, damageDone);
            if (!playerUnit3.isDead)
            {
                ignite(playerUnit3);
            }
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
    void Ammit(int randNum)
    {
        randNum = Random.Range(0, 10);
        if (randNum < 5)
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
                if (!playerUnit1.isDead)
                {
                    poison(playerUnit1);
                }
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
                if (!playerUnit2.isDead)
                {
                    poison(playerUnit2);
                }
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
                if (!playerUnit3.isDead)
                {
                    poison(playerUnit3);
                }
            }
            else
            {
                StartCoroutine(TypeText("The attack missed!"));
            }
        }
    }

    void Ra(int randNum)
    {
        randNum = Random.Range(0, 10);
        if (randNum < 7)
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
    }

    void Anubis(int randNum)
    {

        if (isEnemy1dead)
        {
            enemyhp = enemyUnit.maxHP;
            enemyUnit.isDead = false;
            isEnemy1dead = false;
            enemysprite.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            randNum = Random.Range(0, 3);
            crit = Random.Range(1, 201);
            rounded = 10 * (enemyUnit2.damage / 100f);
            if (rounded < 1) rounded = 1;
            enemyDamage = Mathf.RoundToInt(10 * rounded);

            if (randNum == 0 && !playerUnit1.isDead)
            {
                Instantiate(hitHurtScreen, Vector3.zero, Quaternion.identity);
                hitHurtManager = GameObject.Find("Hit-Hurt(Clone)").GetComponent<hitHurtManager>();
                hitHurtManager.playerHurt(playerUnit1, enemyUnit2);
                damageDone = enemyDamage - Mathf.RoundToInt(enemyDamage * (playerUnit1.defence / 100f));
                if (crit <= enemyUnit2.luck)
                {
                    damageDone *= 2;
                }
                damaged(playerUnit1, 0, damageDone);
                StartCoroutine(TypeText(enemyUnit2.unitName + " attacks Jorm for " + damageDone + " damage!"));

            }
            else if (randNum == 1 && !playerUnit2.isDead)
            {
                Instantiate(hitHurtScreen, Vector3.zero, Quaternion.identity);
                hitHurtManager = GameObject.Find("Hit-Hurt(Clone)").GetComponent<hitHurtManager>();
                hitHurtManager.playerHurt(playerUnit2, enemyUnit2);
                damageDone = enemyDamage - Mathf.RoundToInt(enemyDamage * (playerUnit2.defence / 100f));
                if (crit <= enemyUnit2.luck)
                {
                    damageDone *= 2;
                }
                damaged(playerUnit2, 1, damageDone);
                StartCoroutine(TypeText(enemyUnit2.unitName + " attacks Hameeda for " + damageDone + " damage!"));

            }
            else if (randNum == 2 && !playerUnit3.isDead)
            {
                Instantiate(hitHurtScreen, Vector3.zero, Quaternion.identity);
                hitHurtManager = GameObject.Find("Hit-Hurt(Clone)").GetComponent<hitHurtManager>();
                hitHurtManager.playerHurt(playerUnit3, enemyUnit2);
                damageDone = enemyDamage - Mathf.RoundToInt(enemyDamage * (playerUnit3.defence / 100f));
                if (crit <= enemyUnit2.luck)
                {
                    damageDone *= 2;
                }
                damaged(playerUnit3, 2, damageDone);
                StartCoroutine(TypeText(enemyUnit2.unitName + " attacks Exounos for " + damageDone + " damage!"));

            }
            else
            {
                StartCoroutine(TypeText("The attack missed!"));
            }
        }
    }

    public void special()
    {
        Debug.Log("Hit");
        if (specialMeter.getMeter() == specialMeter.getMaxMeter())
        {
            specialMeter.setMeter(0);
            if (state == BattleState.JORMTURN)
            {
                StartCoroutine(jormSpecial());
            }
            else if (state == BattleState.HAMEEDATURN)
            {
                StartCoroutine(hameedaSpecial());
            }
            else if (state == BattleState.EXOUNOSTURN)
            {
                StartCoroutine(exounousSpecial());
            }
            //do the cool attack thing
        }
    }

    private IEnumerator jormSpecial()
    {
        mainUI.SetActive(false);
        GameObject ult = Instantiate(ragnarockingChair);
        yield return new WaitForSeconds(5.2f);
        int add = ult.GetComponent<counterSpecial>().getCount();
        damageDone = 18 + add;
        isCrit = false;
        Destroy(ult.gameObject);
        mainUI.SetActive(true);
        if (battleStart.isMultiple)
        {
            enemy2hp -= damageDone;
        }
        enemyhp -= damageDone;
        
        StartCoroutine(playerCoroutineAttack(playerUnit1, damageDone, isCrit, enemyUnit));
    }

    private IEnumerator exounousSpecial()
    {
        mainUI.SetActive(false);
        GameObject ult = Instantiate(foodComa);
        yield return new WaitForSeconds(5.2f);
        int add = ult.GetComponent<counterSpecial>().getCount();
        playerUnit1.isDead = false;
        playerUnit2.isDead = false;
        playerUnit3.isDead = false;
        playerUnit1.currentHp = playerUnit1.maxHP + add;
        playerUnit2.currentHp = playerUnit2.maxHP + add;
        playerUnit3.currentHp = playerUnit3.maxHP + add;
        playerUnit1.statusEffect = false;
        playerUnit2.statusEffect = false;
        playerUnit3.statusEffect = false;
        playerUnit1.statusCounter = 0;
        playerUnit2.statusCounter = 0;
        playerUnit3.statusCounter = 0;
        playerUnit1.onFire = false;
        playerUnit2.onFire = false;
        playerUnit3.onFire = false;
        playerUnit1.poisoned = false;
        playerUnit2.poisoned = false;
        playerUnit3.poisoned = false;
        exounosHp.text = playerUnit3.currentHp + "/" + playerUnit3.maxHP;
        exounosHealthBar.setHealth(playerUnit3.currentHp);
        hameedaHp.text = playerUnit2.currentHp + "/" + playerUnit2.maxHP;
        hameedaHealthBar.setHealth(playerUnit2.currentHp);
        jormHp.text = playerUnit1.currentHp + "/" + playerUnit1.maxHP;
        jormHealthBar.setHealth(playerUnit1.currentHp);
        Destroy(ult.gameObject);
        mainUI.SetActive(true);
        StartCoroutine(playerCoroutineNeutral());
    }

    private IEnumerator hameedaSpecial()
    {
        mainUI.SetActive(false);
        GameObject ult = Instantiate(mythiKohl);
        yield return new WaitForSeconds(5.2f);
        int add = ult.GetComponent<counterSpecial>().getCount();
        damageDone = 22 + add;
        isCrit = false;
        Destroy(ult.gameObject);
        mainUI.SetActive(true);
        if (battleStart.isMultiple)
        {
            enemy2hp -= damageDone;
        }
        enemyhp -= damageDone;        
        StartCoroutine(playerCoroutineAttack(playerUnit2, damageDone, isCrit, enemyUnit));
    }

    void punchingBag()
    {
        if (turnTracker == 0)
        {
            StartCoroutine(TypeText("On your turn you can click the attack button."));
            StartCoroutine(tutorialCR(turnTracker));
        }
        else if (turnTracker == 1)
        {
            StartCoroutine(TypeText("The special meter on the right lets you unlease a powerful ability."));
            StartCoroutine(tutorialCR(turnTracker));
        }
        else if (turnTracker == 2)
        {
            StartCoroutine(TypeText("Jorm and Hameeda both deal a lot of damage with theirs."));
            StartCoroutine(tutorialCR(turnTracker));
        }
        else if (turnTracker == 3)
        {
            StartCoroutine(TypeText("Charge Jorm and Exounus special with the A and D keys"));
            StartCoroutine(tutorialCR(turnTracker));
        }
        else if (turnTracker == 4)
        {
            StartCoroutine(TypeText("The run button can be used to avoid certain fights."));
            StartCoroutine(tutorialCR(turnTracker));
        }
        else
        {
            StartCoroutine(TypeText("You can beat the bag now."));
        }

    }
    
    void ignite(UnitStats u)
    {
        u.statusEffect = true;
        u.onFire= true;
    }
    void burn(UnitStats u,int unit)
    {
        if (u.statusCounter >= 3)
        {
            u.statusEffect = false;
            u.onFire= false;
            u.statusCounter = 0;
        }
        else
        {
            StartCoroutine(TypeText(u.unitName+" is hurt by their burns."));
            damaged(u,unit,2);
            u.statusCounter++;
        }
    }
    void poison(UnitStats u)
    {
        u.statusEffect = true;
        u.poisoned = true;
    }
    void melt(UnitStats u, int unit)
    {
        if (u.statusCounter >= 2)
        {
            u.statusEffect = false;
            u.onFire = false;
            u.statusCounter = 0;
        }
        else
        {
            StartCoroutine(TypeText(u.unitName + " is hurt by the poison."));
            damaged(u, unit, 3);
            u.statusCounter++;

        }
    }
    public IEnumerator yetAnotherCR(string name) {
        if (isTutorial)
        {
            yield return new WaitForSeconds(5);
        }
        else
        {
            yield return new WaitForSeconds(3);
        }
        if (name == "Jorm")
        {
            jormTurn();
        }
        else if (name == "Hameeda")
        {
            hameedaTurn();
        }
        else if (name == "Exounos")
        {
            exounosTurn();   
        }
    }
    public IEnumerator tutorialCR(int turn)
    {
        yield return new WaitForSeconds(4);
        if (turn == 0)
        {
            StartCoroutine(TypeText("A menu of options will open and on click it might let you select a target."));
        }
        else if (turn == 1)
        {
            StartCoroutine(TypeText("It fills whenever a party member is attacked or attacks."));
        }
        else if (turn == 2)
        {
            StartCoroutine(TypeText("Exounos fully heals all party members, even when they're knocked down."));
        }
        else if(turn==3)
        {
            StartCoroutine(TypeText("Charge Hameeda's by clicking the orb."));
        }
        else if (turn == 4)
        {
            StartCoroutine(TypeText("You are now all set to take on the pantheon. Good luck."));
            enemyhp = 1;
        }
    }
}

