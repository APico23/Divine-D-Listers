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
        speaker = GetComponent<AudioSource>();
        speaker.clip = battleTheme;
        speaker.Play();
    }

    // Update is called once per frame
    void Update()
    {
        speaker.Stop();

    }
}
