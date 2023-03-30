using UnityEngine;
using System.Collections;


public class Unit: MonoBehaviour
{

    public Animator unitAnimator;
    public string unitName;
    public int unitLevel;

    public int exp;
    public int expNeeded;

    public int damage;
    public int luck;
    public int defence;
    public int speed;

    public int maxHP;
    public int currentHp;

    public bool isDead = false;
    public bool isAttacking = false;
    public bool isHit = false; 

    void Update()
    {
        if (isDead) 
        {
            unitAnimator.SetBool("UnitIsDowned", true);
        }
        if (isAttacking) 
        {
            StartCoroutine(animationAttack());  
        }
        if (isHit) 
        {
            StartCoroutine(animationHit());
        }
    }

    private IEnumerator animationAttack() 
    {
        unitAnimator.SetBool("UnitIsAttacking", true);
        yield return new WaitForSeconds(0.5f);
        unitAnimator.SetBool("UnitIsAttacking", false);
        isAttacking = false;
    }

    private IEnumerator animationHit()
    {
        unitAnimator.SetBool("UnitIsHit", true);
        yield return new WaitForSeconds(0.5f);
        unitAnimator.SetBool("UnitIsHit", false);
        isHit = false; 
    }

    public void AttemptLevelUp(int playerNum) 
    {
        exp += 10;
        if (exp >= expNeeded) 
        {
            unitLevel++;
            exp = 0; 
            if (playerNum == 1)
            {
                maxHP += 2;
                damage += 1;
                luck += 2;
                defence += 3;
                speed += 2;
            }
            else if (playerNum == 2)
            {
                maxHP += 1;
                damage += 3;
                luck += 2;
                defence += 1;
                speed += 2;
            }
            else if (playerNum == 3)
            {
                maxHP += 1;
                damage += 2;
                luck += 3;
                defence += 2;
                speed += 1;
            }
            expNeeded += 10;
        } 
    }


}
