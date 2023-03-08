using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioChooser : MonoBehaviour
{

    private AudioSource speaker;
    public AudioClip mainTheme;
    public AudioClip battleTheme;


    void Start()
    {
        //speaker = GetComponent<AudioSource>();
        //speaker.clip = battleTheme;
        //Debug.Log(speaker.clip.name);
        //speaker.Play();
        //Debug.Log(speaker.isPlaying);
    }

    // Update is called once per frame
    void Update()
    {
        //speaker.Stop();

    }
}
