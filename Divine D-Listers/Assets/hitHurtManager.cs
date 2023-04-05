using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class hitHurtManager : MonoBehaviour
{

    public Image playerSprite;
    public Image enemySprite;

    private static hitHurtManager instance;
    private Animator anim;
    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            anim = GetComponent<Animator>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

   public void playerHit(UnitStats player, UnitStats enemy)
    {
        playerSprite.sprite = player.attack;
        enemySprite.sprite = enemy.hurt;
        anim.SetBool("playerHit", true);
        Destroy(gameObject, 1f);
        return;
    }

    public void playerHurt(UnitStats player, UnitStats enemy)
    {
        playerSprite.sprite = player.hurt;
        enemySprite.sprite = enemy.attack;
        anim.SetBool("playerHurt", true);
        Destroy(gameObject, 1f);
        return;
    }
}
