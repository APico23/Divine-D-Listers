using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using Unity.VisualScripting;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI speakerName, dialogue, navButtonText;
    public Image speakerSprite;
    public Image speaker2Sprite;
    private bool speaker1Speaking;
    private Sprite empty;
    public GameObject backdrop;

    private int currentIndex;
    private Conversation currentConvo;
    private static DialogueManager instance;
    private Animator anim;
    private Coroutine typing;
    private GameObject player;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            anim = GetComponent<Animator>();
        }
        else
        {
            Destroy(transform.parent.gameObject);
        }
    }

    public static void startConversation(Conversation convo)
    {
        instance.anim.SetBool("isOpen", true);
        instance.currentIndex = 0;
        instance.currentConvo = convo;
        instance.speakerName.text = "";
        instance.dialogue.text = "";
        instance.navButtonText.text = ">";
        instance.speaker1Speaking = true;
        instance.empty = instance.speakerSprite.sprite;
        instance.player = GameObject.Find("Player");
        instance.backdrop.SetActive(true);

        instance.ReadNext();
    }

    public void ReadNext()
    {
        player.GetComponent<playerMove>().canMove = false;

        if(currentIndex > currentConvo.GetLength())
        {
            instance.anim.SetBool("isOpen", false);
            speakerSprite.sprite = empty;
            speaker2Sprite.sprite = empty;
            instance.backdrop.SetActive(false);
            player.GetComponent<playerMove>().canMove = true;
            Destroy(transform.parent.gameObject,0.2f);
            return;
        }
        if(currentIndex == currentConvo.GetLength())
        {
            navButtonText.text = "X";
        }
        speakerName.text = currentConvo.GetLineByIndex(currentIndex).speaker.getName();
        
        if (typing == null)
        {
            typing = instance.StartCoroutine(TypeText(currentConvo.GetLineByIndex(currentIndex).dialogue));
        }
        else
        {
            instance.StopCoroutine(typing);
            typing = null;
            typing = instance.StartCoroutine(TypeText(currentConvo.GetLineByIndex(currentIndex).dialogue));
        }
        
        if (speakerSprite.sprite == currentConvo.GetLineByIndex(currentIndex).speaker.getSprite())
        {
            speaker1Speaking = true;
        }
        else
        {
            if (speakerSprite.sprite.name == "empty")
            {
                speakerSprite.sprite = currentConvo.GetLineByIndex(currentIndex).speaker.getSprite();
            }
            else
            {
                speaker2Sprite.sprite = currentConvo.GetLineByIndex(currentIndex).speaker.getSprite();
                speaker1Speaking = false;
            }
        }
        if (speaker1Speaking)
        {
            speakerSprite.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            speaker2Sprite.GetComponent<Image>().color = new Color32(115, 115, 115, 255);
        }
        else
        {
            speaker2Sprite.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            speakerSprite.GetComponent<Image>().color = new Color32(115, 115, 115, 255);
        }
        currentIndex++; 
    }

    private IEnumerator TypeText(string text)
    {
        dialogue.text = "";
        bool complete = false;

        int index = 0;

        while (!complete)
        {
            dialogue.text += text[index];
            yield return new WaitForSeconds(0.02f);
            index++;

            if (index == text.Length)
            {
                complete = true;
            }
        }

        typing = null;

    }

}
