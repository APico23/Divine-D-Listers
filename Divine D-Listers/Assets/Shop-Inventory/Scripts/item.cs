using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum itemType {AMBROSIA=0 }

public class item
{
    public Sprite sprite;
    public string itemName;
    public int index;

    public item(itemType type, int i, Sprite s)
    {
        switch (type) {
            case itemType.AMBROSIA:
                itemName = "Ambrosia";
                sprite = s;
                index = i;
                break;
            default:
                index = -1;
                break;
        
        }
    }
}
