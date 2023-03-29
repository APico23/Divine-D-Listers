using UnityEngine;
using System.Collections;


public class Unit: MonoBehaviour
{

    public Animator unitAnimator;
    public string unitName;
    public int unitLevel;

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
        unitAnimator.SetBool("UnitIsHit", true);
        isHit = false; 
    }
}
