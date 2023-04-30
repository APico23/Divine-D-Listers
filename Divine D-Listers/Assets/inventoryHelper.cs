using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class inventoryHelper : MonoBehaviour
{
    public Transform t1;
    public Transform t2;
    public Transform t3;
    public Transform t4;
    public Transform t5;
    public Transform t6;
    public Transform t7;
    public Transform t8;
    public Transform t9;
    public Transform t10;
    public Transform t11;
    public Transform t12;
    public Transform t13;
    public Transform t14;
    public Transform t15;
    public Transform t16;

    private inventory playerInventory;
    public TextMeshProUGUI goldCount;

    public GameObject ambrosiaSprite;
    public GameObject cheeseSprite;
    public GameObject featherSprite;


    void Start()
    {
        playerInventory = GameObject.Find("PlayerInfo").GetComponent<PlayerInfo>().inventory;
    }

    void Update()
    {
        goldCount.SetText("" + playerInventory.playerGold);
        displayInventory();
    }

    //YES I KNOW, there is probably a much better way to do this
    public void displayInventory()
    {
        for (int i = 0; i < playerInventory.openPosition - 1; i++)
        {
            switch (i)
            {
                case 0:
                    if (playerInventory.playerItems[0].itemName == "Ambrosia")
                    {
                        Instantiate(ambrosiaSprite, t1);
                    }
                    else if (playerInventory.playerItems[0].itemName == "Brick Of Cheese")
                    {
                        Instantiate(cheeseSprite, t1);
                    }
                    else
                    {
                        Instantiate(featherSprite, t1);
                    }
                    break;
                case 1:
                    if (playerInventory.playerItems[1].itemName == "Ambrosia")
                    {
                        Instantiate(ambrosiaSprite, t2);
                    }
                    else if (playerInventory.playerItems[1].itemName == "Brick Of Cheese")
                    {
                        Instantiate(cheeseSprite, t2);
                    }
                    else
                    {
                        Instantiate(featherSprite, t2);
                    }
                    break;
                case 2:
                    if (playerInventory.playerItems[2].itemName == "Ambrosia")
                    {
                        Instantiate(ambrosiaSprite, t3);
                    }
                    else if (playerInventory.playerItems[2].itemName == "Brick Of Cheese")
                    {
                        Instantiate(cheeseSprite, t3);
                    }
                    else
                    {
                        Instantiate(featherSprite, t3);
                    }
                    break;
                case 3:
                    if (playerInventory.playerItems[3].itemName == "Ambrosia")
                    {
                        Instantiate(ambrosiaSprite, t4);
                    }
                    else if (playerInventory.playerItems[3].itemName == "Brick Of Cheese")
                    {
                        Instantiate(cheeseSprite, t4);
                    }
                    else
                    {
                        Instantiate(featherSprite, t4);
                    }
                    break;
                case 4:
                    if (playerInventory.playerItems[4].itemName == "Ambrosia")
                    {
                        Instantiate(ambrosiaSprite, t5);
                    }
                    else if (playerInventory.playerItems[4].itemName == "Brick Of Cheese")
                    {
                        Instantiate(cheeseSprite, t5);
                    }
                    else
                    {
                        Instantiate(featherSprite, t5);
                    }
                    break;
                case 5:
                    if (playerInventory.playerItems[5].itemName == "Ambrosia")
                    {
                        Instantiate(ambrosiaSprite, t6);
                    }
                    else if (playerInventory.playerItems[5].itemName == "Brick Of Cheese")
                    {
                        Instantiate(cheeseSprite, t6);
                    }
                    else
                    {
                        Instantiate(featherSprite, t6);
                    }
                    break;
                case 6:
                    if (playerInventory.playerItems[6].itemName == "Ambrosia")
                    {
                        Instantiate(ambrosiaSprite, t7);
                    }
                    else if (playerInventory.playerItems[6].itemName == "Brick Of Cheese")
                    {
                        Instantiate(cheeseSprite, t7);
                    }
                    else
                    {
                        Instantiate(featherSprite, t7);
                    }
                    break;
                case 7:
                    if (playerInventory.playerItems[7].itemName == "Ambrosia")
                    {
                        Instantiate(ambrosiaSprite, t8);
                    }
                    else if (playerInventory.playerItems[7].itemName == "Brick Of Cheese")
                    {
                        Instantiate(cheeseSprite, t8);
                    }
                    else
                    {
                        Instantiate(featherSprite, t8);
                    }
                    break;
                case 8:
                    if (playerInventory.playerItems[8].itemName == "Ambrosia")
                    {
                        Instantiate(ambrosiaSprite, t9);
                    }
                    else if (playerInventory.playerItems[8].itemName == "Brick Of Cheese")
                    {
                        Instantiate(cheeseSprite, t9);
                    }
                    else
                    {
                        Instantiate(featherSprite, t9);
                    }
                    break;
                case 9:
                    if (playerInventory.playerItems[9].itemName == "Ambrosia")
                    {
                        Instantiate(ambrosiaSprite, t10);
                    }
                    else if (playerInventory.playerItems[9].itemName == "Brick Of Cheese")
                    {
                        Instantiate(cheeseSprite, t10);
                    }
                    else
                    {
                        Instantiate(featherSprite, t10);
                    }
                    break;
                case 10:
                    if (playerInventory.playerItems[10].itemName == "Ambrosia")
                    {
                        Instantiate(ambrosiaSprite, t11);
                    }
                    else if (playerInventory.playerItems[10].itemName == "Brick Of Cheese")
                    {
                        Instantiate(cheeseSprite, t11);
                    }
                    else
                    {
                        Instantiate(featherSprite, t11);
                    }
                    break;
                case 11:
                    if (playerInventory.playerItems[11].itemName == "Ambrosia")
                    {
                        Instantiate(ambrosiaSprite, t12);
                    }
                    else if (playerInventory.playerItems[11].itemName == "Brick Of Cheese")
                    {
                        Instantiate(cheeseSprite, t12);
                    }
                    else
                    {
                        Instantiate(featherSprite, t12);
                    }
                    break;
                case 12:
                    if (playerInventory.playerItems[12].itemName == "Ambrosia")
                    {
                        Instantiate(ambrosiaSprite, t13);
                    }
                    else if (playerInventory.playerItems[12].itemName == "Brick Of Cheese")
                    {
                        Instantiate(cheeseSprite, t13);
                    }
                    else
                    {
                        Instantiate(featherSprite, t13);
                    }
                    break;
                case 13:
                    if (playerInventory.playerItems[13].itemName == "Ambrosia")
                    {
                        Instantiate(ambrosiaSprite, t14);
                    }
                    else if (playerInventory.playerItems[13].itemName == "Brick Of Cheese")
                    {
                        Instantiate(cheeseSprite, t14);
                    }
                    else
                    {
                        Instantiate(featherSprite, t14);
                    }
                    break;
                case 14:
                    if (playerInventory.playerItems[14].itemName == "Ambrosia")
                    {
                        Instantiate(ambrosiaSprite, t15);
                    }
                    else if (playerInventory.playerItems[14].itemName == "Brick Of Cheese")
                    {
                        Instantiate(cheeseSprite, t15);
                    }
                    else
                    {
                        Instantiate(featherSprite, t15);
                    }
                    break;
                case 15:
                    if (playerInventory.playerItems[15].itemName == "Ambrosia")
                    {
                        Instantiate(ambrosiaSprite, t16);
                    }
                    else if (playerInventory.playerItems[15].itemName == "Brick Of Cheese")
                    {
                        Instantiate(cheeseSprite, t16);
                    }
                    else
                    {
                        Instantiate(featherSprite, t16);
                    }
                    break;
                default:
                    break;
            }
        }
    }

    //buttons for removing items
    public void removeitem1()
    {
        playerInventory.removeItem(0);
    }
    public void removeitem2()
    {
        playerInventory.removeItem(1);
    }
    public void removeitem3()
    {
        playerInventory.removeItem(2);
    }
    public void removeitem4()
    {
        playerInventory.removeItem(3);
    }
    public void removeitem5()
    {
        playerInventory.removeItem(4);
    }
    public void removeitem6()
    {
        playerInventory.removeItem(5);
    }
    public void removeitem7()
    {
        playerInventory.removeItem(6);
    }
    public void removeitem8()
    {
        playerInventory.removeItem(7);
    }
    public void removeitem9()
    {
        playerInventory.removeItem(8);
    }
    public void removeitem10()
    {
        playerInventory.removeItem(9);
    }
    public void removeitem11()
    {
        playerInventory.removeItem(10);
    }
    public void removeitem12()
    {
        playerInventory.removeItem(11);
    }
    public void removeitem13()
    {
        playerInventory.removeItem(12);
    }
    public void removeitem14()
    {
        playerInventory.removeItem(13);
    }
    public void removeitem15()
    {
        playerInventory.removeItem(14);
    }
    public void removeitem16()
    {
        playerInventory.removeItem(15);
    }

}
