using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class hitHurtManager : MonoBehaviour
{

    public Image playerSprite;
    public Image enemySprite;
    public Image player1Sprite;
    public Image player2Sprite;
    public Image player3Sprite;

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
    public void partyHurt(UnitStats player1, UnitStats player2, UnitStats player3, UnitStats enemy)
    {
        player1Sprite.sprite = player1.hurt;
        player2Sprite.sprite = player2.hurt;
        player3Sprite.sprite = player3.hurt;
        enemySprite.sprite = enemy.attack;
        Destroy(gameObject, 1f);
        return;
    }
}
