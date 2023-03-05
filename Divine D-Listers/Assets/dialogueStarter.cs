using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class dialogueStarter : MonoBehaviour
{

    public Conversation convo;

    private GameObject player;

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject;
            GameObject.Find("Dialogue").SetActive(true);
            DialogueManager.startConversation(convo);
        }
    }

}
