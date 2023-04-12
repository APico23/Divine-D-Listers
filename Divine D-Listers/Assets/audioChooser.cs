using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class audioChooser : MonoBehaviour
{

    private AudioSource speaker;
    public AudioClip mainTheme;
    public AudioClip battleTheme;
    public AudioClip realmTheme;
    


    void Start()
    {
       speaker = GetComponent<AudioSource>();
    }

    void Update()
    {
        string curScene = SceneManager.GetActiveScene().name;
        if (curScene == "realmOfForgottenGods")
        {
            if (speaker.clip != realmTheme)
            {
                speaker.clip = realmTheme;
                speaker.loop = true;
                speaker.Play();
            }
        }
        if (curScene == "battleScene")
        {
            if (speaker.clip != battleTheme)
            {
                speaker.clip = battleTheme;
                speaker.loop = true;
                speaker.Play();
            }
        }
        if (curScene != "realmOfForgottenGods" && curScene != "battleScene")
        {
            if (speaker.clip != mainTheme)
            {
                speaker.clip = mainTheme;
                speaker.loop = true;
                speaker.Play();
            }
        }

    }
}
