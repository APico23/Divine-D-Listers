using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatMenu : MonoBehaviour
{

    UnitStats playerUnit1;
    UnitStats playerUnit2;
    UnitStats playerUnit3;

    public GameObject jorm;
    public GameObject hameeda;
    public GameObject exounos;

    public GameObject jormBanner;
    public GameObject exounosBanner;
    public GameObject hameedaBanner;

    private GameObject[] banners;
    private UnitStats[] playerStats;
    private inventory playerInventory;

    public Text attackT;
    public Text luckT;
    public Text defenceT;
    public Text healthT;
    public Text speedT;
    public Text levelT;
    public TextMeshProUGUI goldCount;



    private int currentIndex; 


    // Start is called before the first frame update
    void Start()
    {
        playerInventory = GameObject.Find("PlayerInfo").GetComponent<PlayerInfo>().inventory;
        goldCount.SetText("" + playerInventory.playerGold);



        currentIndex = 0;
        exounosBanner.SetActive(false);
        hameedaBanner.SetActive(false);
        playerUnit1 = jorm.GetComponent<Unit>().unitStats;
        playerUnit2 = hameeda.GetComponent<Unit>().unitStats;
        playerUnit3 = exounos.GetComponent<Unit>().unitStats;

        playerStats = new UnitStats[] { playerUnit1, playerUnit2, playerUnit3};
        banners = new GameObject[] { jormBanner, hameedaBanner, exounosBanner}; 
    }
    void Update()
    {
        banners[currentIndex].SetActive(true);
        attackT.text = "" + playerStats[currentIndex].damage;
        luckT.text = "" + playerStats[currentIndex].luck;
        defenceT.text = "" + playerStats[currentIndex].defence;
        healthT.text = "" + playerStats[currentIndex].maxHP;
        speedT.text = "" + playerStats[currentIndex].speed;
        levelT.text = "" + playerStats[currentIndex].unitLevel;
    }
    public void Left() 
    {
        banners[currentIndex].SetActive(false);
        if (currentIndex == 0)
        {
            currentIndex = 2;
        }
        else 
        {
            currentIndex--;
        }
    }
    public void Right() 
    {
        banners[currentIndex].SetActive(false);
        if (currentIndex == 2)
        {
            currentIndex = 0;
        }
        else
        {
            currentIndex++;
        }
    }
}
