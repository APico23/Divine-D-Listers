using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class flickerLight : MonoBehaviour
{
    
    Light2D lightF;
    bool notFlickering;

    private void Start()
    {
        lightF = gameObject.GetComponent<Light2D>();
        notFlickering = true;
    }

    void Update()
    {
        if (notFlickering == true)
        {
            notFlickering= false;
            StartCoroutine(lightFlicker());
        }
        
    }

    private IEnumerator lightFlicker()
    {

        float randTime = 0;

        for (int i = 0; i < 5; i++)
        {
            randTime = Random.Range(0f, 2f);
            lightF.intensity -= 0.1f;
            yield return new WaitForSeconds(randTime);
        }

        for (int i = 0; i < 5; i++)
        {
            randTime = Random.Range(0f, 2f);
            lightF.intensity += 0.1f;
            yield return new WaitForSeconds(randTime);
        }
        notFlickering = true;

        notFlickering = true;
    }
}
