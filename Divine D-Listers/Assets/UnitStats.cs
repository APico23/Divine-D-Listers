using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class UnitStats : ScriptableObject
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

    public Sprite attack;
    public Sprite hurt;
}
