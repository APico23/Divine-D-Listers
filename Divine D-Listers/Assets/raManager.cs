using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class raManager : MonoBehaviour
{
    public convoTracker tracker;
    public Quest raBeat;
    public randomEncounters raFight;
    public VectorValue playerStorage;
    public Vector2 playerPosition;
    public Sprite background;

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            if (tracker.convoAt == 0)
            {
                dialogueStarter.startConvo();
                tracker.continueConvo();
            }
            else if (tracker.convoAt == 1 && !raBeat.isCompleted) {
                playerStorage.initialValue = playerPosition;
                GameObject.Find("BattleStarter").GetComponent<battleStarter>().setEnemy(raFight);
                GameObject.Find("BattleStarter").GetComponent<battleStarter>().background = background;
                SceneManager.LoadScene("battleScene");
            } else if (tracker.convoAt == 1 && raBeat.isCompleted)
            {
                dialogueStarter.startConvo();
            }

        }
    }
}
