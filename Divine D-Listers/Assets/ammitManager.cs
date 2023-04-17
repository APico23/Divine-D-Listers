using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ammitManager : MonoBehaviour
{
    public convoTracker tracker;
    public Quest riddle2;
    public Sprite hurt;
    public VectorValue playerStorage;
    public Vector2 playerPosition;
    public randomEncounters ammitEnemy;
    public Sprite background;
    public Quest ammitBeat;

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (tracker.convoAt == 0)
            {
                dialogueStarter.startConvo();
                tracker.continueConvo();
                return;
            }
            if (tracker.convoAt == 1 && !ammitBeat.isCompleted)
            {
                playerStorage.initialValue = playerPosition;
                GameObject.Find("BattleStarter").GetComponent<battleStarter>().setEnemy(ammitEnemy);
                GameObject.Find("BattleStarter").GetComponent<battleStarter>().background = background;
                SceneManager.LoadScene("battleScene");
                
            }
            else if (tracker.convoAt == 1)
            {
                dialogueStarter.startConvo();
                tracker.continueConvo();
                riddle2.isCompleted = true;
            }
        }
    }

    public void Start()
    {
        if (ammitBeat.isCompleted)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = hurt;
        }
    }


}
