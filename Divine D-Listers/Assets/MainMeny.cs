using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMeny : MonoBehaviour
{
    public void startButton() {
        SceneManager.LoadScene("realmOfForgottenGods");
    }
}
