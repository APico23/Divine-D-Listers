using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class outOfBattleUIManager : MonoBehaviour
{
    public GameObject optionsPanel;
    public GameObject pauseMenu;
    public GameObject statsMenu;
    public GameObject bottomPanel;
    public GameObject player;
    playerMove playerMoveScript;

    // Start is called before the first frame update
    void Start()
    {
        playerMoveScript = player.GetComponent<playerMove>();
        optionsPanel.SetActive(false);
        pauseMenu.SetActive(false);
        bottomPanel.SetActive(false);
        statsMenu.SetActive(false);
    }

    public void pauseButton() 
    {
        playerMoveScript.canMove = false;
        pauseMenu.SetActive(true);
        bottomPanel.SetActive(true);
    }

    public void optionsButton() 
    {
        bottomPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void homeButton() 
    {
        SceneManager.LoadSceneAsync("Main Menu");
    }

    public void statsButton()
    {
        statsMenu.SetActive(true);
    }

    public void statsButtonResume()
    {
        statsMenu.SetActive(false);
    }

    public void resumeButton() 
    {
        pauseMenu.SetActive(false);
        bottomPanel.SetActive(false);
        playerMoveScript.canMove = true;
    }

    public void optionsBack() 
    {
        optionsPanel.SetActive(false);
        bottomPanel.SetActive(true);
    }
}
