using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
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
    public PlayableDirector cutscene2;
    public Quest ra2Beat;
    public randomEncounters trueRaFight;
    public PlayableDirector cutscene3;


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
                tracker.continueConvo();
            } else if (tracker.convoAt == 2 && !ra2Beat.isCompleted)
            {
                StartCoroutine(cutSceneLoader());
            } else if (tracker.convoAt == 2 && ra2Beat.isCompleted)
            {
                dialogueStarter.startConvo();
                tracker.continueConvo();
            }
            else if (tracker.convoAt == 3)
            {
                cutscene3.Play();
            }

        }
    }


    IEnumerator cutSceneLoader()
    {
        cutscene2.Play();
        yield return new WaitForSeconds(3.5f);
        GameObject.Find("BattleStarter").GetComponent<battleStarter>().setEnemy(trueRaFight);
        GameObject.Find("BattleStarter").GetComponent<battleStarter>().background = background;
        SceneManager.LoadScene("battleScene");
    }

}
