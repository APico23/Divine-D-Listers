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
    public ParticleSystem enemySlash;
    public ParticleSystem playerSlash;
    public ParticleSystem partyBurn;
    public ParticleSystem sleep;
    public ParticleSystem rock;
    public ParticleSystem rock2;
    public ParticleSystem rock3;

    private static hitHurtManager instance;
    private Animator anim;
    private GameObject fullCanvas;
    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            anim = GetComponent<Animator>();
            fullCanvas = GameObject.Find("Canvas");
        }
        else
        {
            Destroy(gameObject);
        }
    }

   public void playerHit(UnitStats player, UnitStats enemy)
    {
        fullCanvas.SetActive(false);
        playerSprite.sprite = player.attack;
        enemySprite.sprite = enemy.hurt;
        enemySlash.Play();
        Destroy(gameObject, 2f);
        fullCanvas.SetActive(true);
        return;
    }
    public void playerspecial(UnitStats player, UnitStats enemy)
    {
        fullCanvas.SetActive(false);
        playerSprite.sprite = player.special;
        enemySprite.sprite = enemy.hurt;
        sleep.Play();
        Destroy(gameObject, 2f);
        fullCanvas.SetActive(true);
        return;
    }

    public void playerHurt(UnitStats player, UnitStats enemy)
    {
        fullCanvas.SetActive(false);
        playerSprite.sprite = player.hurt;
        enemySprite.sprite = enemy.attack; 
        playerSlash.Play();
        Destroy(gameObject, 2f);
        fullCanvas.SetActive(true);
        return;
    }
    public void partyHurt(UnitStats player1, UnitStats player2, UnitStats player3, UnitStats enemy, string type)
    {
        fullCanvas.SetActive(false);
        player1Sprite.sprite = player1.hurt;
        player2Sprite.sprite = player2.hurt;
        player3Sprite.sprite = player3.hurt;
        enemySprite.sprite = enemy.special;
        if (type == "burn")
        {
            partyBurn.Play();
        }
        else
        {
            rock.Play();
            rock2.Play();
            rock3.Play();
        }
        Destroy(gameObject, 2f);
        fullCanvas.SetActive(true);
        return;
    }
}
