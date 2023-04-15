using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class shopManager : MonoBehaviour
{

    public TextMeshProUGUI goldCount;
    private inventory playerInventory;

    void Start()
    {
        playerInventory = GameObject.Find("PlayerInfo").GetComponent<PlayerInfo>().inventory;
        goldCount.SetText("" + playerInventory.playerGold);
    }

    
    public void ambrosiaBought()
    {

    }

    public void back()
    {
        Destroy(transform.parent.gameObject);
    }
}
