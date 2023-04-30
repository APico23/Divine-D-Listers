using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Gameplay/New Inventory")]

public class inventory : ScriptableObject, ISerializationCallbackReceiver
{
    public int playerGold;

    public Sprite[] itemSprites;

    public item[] playerItems;

    public int openPosition;

    public void OnAfterDeserialize()
    {
        playerGold = 0;
        playerItems = new item[16];
        openPosition= 0;
    }

    public void OnBeforeSerialize()
    {

    }

    public bool addItem(itemType type)
    {
        if (openPosition <= 15)
        {
            playerItems[openPosition] = new item(type, openPosition);
            openPosition++;
            return true;
        }
        return false;
    }

    public item getItem(int index) { 
        return playerItems[index];
    }

    public void removeItem(int index)
    {
        for (int i = index; i < openPosition - 1; i++) {
            playerItems[i] = playerItems[i+1];   
        }
        openPosition--;
        playerItems[openPosition] = null;

    }

}
