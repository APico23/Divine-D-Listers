using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class cutSceneManager : MonoBehaviour
{

    public PlayableDirector cutscene1;
    public Quest pyramidFirst;
    
    


    void Start()
    {
        if (!pyramidFirst.isCompleted)
        {
            cutscene1.Play();
            pyramidFirst.isCompleted= true;
        }
        
    }

}
