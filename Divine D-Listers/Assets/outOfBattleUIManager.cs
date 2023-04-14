using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class outOfBattleUIManager : MonoBehaviour
{
    public GameObject optionsPanel;
    public GameObject pauseMenu;
    public GameObject statsMenu;
    public GameObject bottomPanel;

    // Start is called before the first frame update
    void Start()
    {
        optionsPanel.SetActive(false);
        pauseMenu.SetActive(false);
        bottomPanel.SetActive(false);
    }

    public void pauseButton() 
    {
        pauseMenu.SetActive(true);
        bottomPanel.SetActive(true);
    }

    public void optionsButton() 
    {
        bottomPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void statsButton()
    { 
    
    }

    public void resumeButton() 
    {
        pauseMenu.SetActive(false);
        bottomPanel.SetActive(false);
    }

    public void optionsBack() 
    {
        optionsPanel.SetActive(false);
        bottomPanel.SetActive(true);
    }
}
