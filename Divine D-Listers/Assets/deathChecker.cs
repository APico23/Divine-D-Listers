using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathChecker : MonoBehaviour
{

    UnitStats me;
    Animator unitAnimator;


    void Start()
    {
        me = gameObject.GetComponent<UnitStats>();
        unitAnimator = gameObject.GetComponent<Animator>();
    }

    
    void Update()
    {
        if (me.isDead)
        {
            unitAnimator.SetBool("UnitIsDowned", true);
        }
        if (!me.isDead)
        {
            unitAnimator.SetBool("UnitIsDowned", false);
        }
    }
}
