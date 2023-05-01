using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class inventoryHelper : MonoBehaviour
{
    private inventory playerInventory;
    public TextMeshProUGUI goldCount;

    public TextMeshProUGUI ambrosiaCount;
    public TextMeshProUGUI cheeseCount;
    public TextMeshProUGUI featherCount;

    private int ac;
    private int cc;
    private int fc;


    void Start()
    {
        playerInventory = GameObject.Find("PlayerInfo").GetComponent<PlayerInfo>().inventory;
    }

    void Update()
    {
        goldCount.SetText("" + playerInventory.playerGold);
        displayInventory();
    }

    public void displayInventory()
    {
        ac = 0;
        cc = 0;
        fc = 0;

        for (int i = 0; i < playerInventory.openPosition; i++) 
        {
            if (playerInventory.playerItems[i].itemName == "Ambrosia")
            {
                ac++;
            }
            else if (playerInventory.playerItems[i].itemName == "Brick Of Cheese")
            {
                cc++;
            }
            else
            {
                fc++;
            }
        }

        ambrosiaCount.text = "Ambrosia: " + ac;
        cheeseCount.text = "Bricks of cheese: " + cc;
        featherCount.text = "Phoenix Feathers: " + fc; 
    }
}
