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

    public bool isDead = false;
    public bool isAttacking = false;
    public bool isHit = false;
    public bool leveledUp = false;

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



}
