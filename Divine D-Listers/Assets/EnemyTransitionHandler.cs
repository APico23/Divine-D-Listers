using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyTransitionHandler : MonoBehaviour
{
    public GameObject transitionPopup;
    public GameObject tensionLines;
    public Image flash;
    private Color flashColor;
    private float multiplier;
    public GameObject actualTransition;
    public Animator atAnimator;

   
    void Start() {
        actualTransition.SetActive(false);
        atAnimator.SetBool("Transition1Done", false);
        EnterBattle();
    }

    public void EnterBattle()
    {
        transitionPopup.SetActive(true);
        StartCoroutine(DoTheFlashThingy());
        StartCoroutine(sceneTransition());
    }

    public IEnumerator DoTheFlashThingy() 
    {
        for (int i = 0; i < 2; i++)
        {
            //start image at 0 opacity
            flashColor = new Color(1f, 1f, 1f, 0f);
            //increase opacity until it reaches max opacity
            flash.color = flashColor;
            multiplier = 0.05f;
            while (flashColor.a <= 0.75f)
            {
                flashColor.a += multiplier;
                flash.color = flashColor;
                yield return new WaitForSeconds(0.1f);
                multiplier *= 2f;
            }
            //then decrease it
            multiplier = 0.05f;
            while (flashColor.a >= 0f)
            {
                flashColor.a -= multiplier;
                flash.color = flashColor;
                yield return new WaitForSeconds(0.1f);
                multiplier *= 2f;
            }
        }
    }
    public IEnumerator sceneTransition() 
    {
        yield return new WaitForSeconds(3);
        actualTransition.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        transitionPopup.SetActive(false);
        //load battle scene here
        atAnimator.SetBool("Transition1Done", true);
        yield return new WaitForSeconds(0.5f);
        actualTransition.SetActive(false);
    }

    public void ExitBattle()
    { 

    }
}
