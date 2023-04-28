using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class shopManager : MonoBehaviour
{

    public TextMeshProUGUI goldCount;
    private inventory playerInventory;
    private playerMove move;

    void Start()
    {
        playerInventory = GameObject.Find("PlayerInfo").GetComponent<PlayerInfo>().inventory;
        goldCount.SetText("" + playerInventory.playerGold);
        move = GameObject.Find("Player").GetComponent<playerMove>();
        move.canMove = false;
    }

    
    public void ambrosiaBought()
    {
        if (playerInventory.playerGold >= 25)
        {
            playerInventory.playerGold -= 25;
            goldCount.SetText("" + playerInventory.playerGold);
            playerInventory.addItem(itemType.AMBROSIA);
        }
    }

    public void cheeseBought()
    {
        if (playerInventory.playerGold >= 30)
        {
            playerInventory.playerGold -= 30;
            goldCount.SetText("" + playerInventory.playerGold);
            playerInventory.addItem(itemType.AMBROSIA);
        }

    }
   
    public void featherBought()
    {
        if (playerInventory.playerGold >= 50)
        {
            playerInventory.playerGold -= 50;
            goldCount.SetText("" + playerInventory.playerGold);
            playerInventory.addItem(itemType.AMBROSIA);
        }

    }

    public void back()
    {
        move.canMove = true;
        Destroy(transform.parent.gameObject);
    }
}
