using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum itemType {AMBROSIA=0, CHEESE=1, FEATHER=2}

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
            case itemType.CHEESE:
                itemName = "Brick Of Cheese";
                sprite = s;
                index = i;
                break;
            case itemType.FEATHER:
                itemName = "Phoenix Feather";
                sprite = s;
                index = i;
                break;
            default:
                index = -1;
                break;
        
        }
    }
}
