using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathChecker : MonoBehaviour
{

    UnitStats me;


    void Start()
    {
        me = gameObject.GetComponent<UnitStats>();
    }

    
    void Update()
    {
        if (me.isDead)
        {
            me.unitAnimator.SetBool("UnitIsDowned", true);
        }
        if (!me.isDead)
        {
            me.unitAnimator.SetBool("UnitIsDowned", false);
        }
    }
}
