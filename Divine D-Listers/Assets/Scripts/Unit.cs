using UnityEngine;
using System.Collections;
using UnityEditor;

public class Unit: MonoBehaviour
{
    public UnitStats unitStats;
    //public Animator unitAnimator;
    //public string unitName;
    //public int unitLevel;

    //public int exp;
    //public int expNeeded;

    //public int damage;
    //public int luck;
    //public int defence;
    //public int speed;

    //public int maxHP;
    //public int currentHp;

    //public bool isDead = false;
    //public bool isAttacking = false;
    //public bool isHit = false;

    //public Sprite attack;
    //public Sprite hurt;

    void Update()
    {
        //if (unitStats.isDead) 
        //{
         //   unitStats.unitAnimator.SetBool("UnitIsDowned", true);
       // }
        //if (unitStats.isAttacking)
        //{
            //StartCoroutine(animationAttack());
       // }
       // if (unitStats.isHit)
       // {
            //StartCoroutine(animationHit());
       // }
    }

    //private IEnumerator animationAttack()
    //{
    //    unitAnimator.SetBool("UnitIsAttacking", true);
    //    yield return new WaitForSeconds(0.5f);
    //    unitAnimator.SetBool("UnitIsAttacking", false);
    //    isAttacking = false;
    //}

    //private IEnumerator animationHit()
    //{
    //    unitAnimator.SetBool("UnitIsHit", true);
    //    yield return new WaitForSeconds(0.5f);
    //    unitAnimator.SetBool("UnitIsHit", false);
    //    isHit = false;
    //}

    public void AttemptLevelUp(int playerNum) 
    {
        unitStats.exp += 10;
        if (unitStats.exp >= unitStats.expNeeded) 
        {
            unitStats.unitLevel++;
            unitStats.exp = 0; 
            if (playerNum == 1)
            {
                unitStats.maxHP += 2;
                unitStats.damage += 1;
                unitStats.luck += 2;
                unitStats.defence += 3;
                unitStats.speed += 2;
            }
            else if (playerNum == 2)
            {
                unitStats.maxHP += 1;
                unitStats.damage += 3;
                unitStats.luck += 2;
                unitStats.defence += 1;
                unitStats.speed += 2;
            }
            else if (playerNum == 3)
            {
                unitStats.maxHP += 1;
                unitStats.damage += 2;
                unitStats.luck += 3;
                unitStats.defence += 2;
                unitStats.speed += 1;
            }
            unitStats.expNeeded += 10;
        } 
    }


}
