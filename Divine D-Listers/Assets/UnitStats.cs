using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unit Stats", menuName = "Battle/New Unit Stats")]
public class UnitStats : ScriptableObject, ISerializationCallbackReceiver
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

    public int statusCounter;

    public bool isDead = false;
    public bool isAttacking = false;
    public bool isHit = false;
    public bool leveledUp = false;
    public bool onFire = false;
    public bool statusEffect = false;
    public bool poisoned = false;

    

    public Sprite attack;
    public Sprite hurt;

    public int defaultUnitLevel;

    public int defaultExp;
    public int defaultExpNeeded;

    public int defaultDamage;
    public int defaultLuck;
    public int defaultDefence;
    public int defaultSpeed;

    public int defaultMaxHP;
    public int defaultCurrentHp;


    public void OnAfterDeserialize()
    {
        unitLevel = defaultUnitLevel;
        exp = defaultExp;
        expNeeded = defaultExpNeeded; 
        damage = defaultDamage; 
        luck = defaultLuck;
        defence = defaultDefence; 
        speed = defaultSpeed;
        maxHP = defaultMaxHP;
        currentHp = defaultCurrentHp;


        isDead = false;
        isAttacking = false;
        isHit = false;
        leveledUp = false;
    }

    public void OnBeforeSerialize()
    {

    }

    public void attemptLvlUp(int xp,int unit)
    {
        exp += xp;
        if(exp>=expNeeded)
        {
            leveledUp = true;
            unitLevel++;
            if (unit == 1)
            {
                damage += 1;
                luck += 1;
                defence += 2;
                speed += 1;
                maxHP += 3;
            }
            if (unit == 2)
            {
                damage += 2;
                luck += 2;
                defence += 1;
                speed += 1;
                maxHP += 2;
            }
            if (unit == 3)
            {
                damage += 1;
                luck += 2;
                defence += 2;
                speed += 1;
                maxHP += 2;
            }
            exp -= expNeeded;
            expNeeded += 10;
            attemptLvlUp(exp,unit);
        }
    }

}
