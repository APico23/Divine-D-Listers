using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class dialogueStarter : MonoBehaviour
{

    public Conversation[] convos;
    public convoTracker tracker;
    public GameObject dialogue;

    private GameObject player;
    private static dialogueStarter instance;
    private void Awake()
    {
        instance = this;
    }

    public static void startConvo()
    {
        
        Instantiate(instance.dialogue, Vector3.zero, Quaternion.identity);
        DialogueManager.startConversation(instance.convos[instance.tracker.convoAt]);
    }

}
