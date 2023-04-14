using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Special : MonoBehaviour
{
    public Slider slider;
    public Animator meterAnimator;

    void Start() {
        meterAnimator = GameObject.Find("Special Fill").GetComponent<Animator>();
        slider.value= 0;
    }
    
    public void setMeter(float h)
    {
        slider.value = h;
    }
    public void increaseMeter(float h)
    {
        if(slider.value < slider.maxValue) {
            slider.value += h;
        }
       
    }

    public void setMaxMeter(float h)
    {
        slider.maxValue = h;
    }

    public float getMeter() 
    {
        return slider.value;
    }

    public float getMaxMeter() 
    {
        return slider.maxValue;
    }

    void Update() {
        if (slider.value == slider.maxValue) 
        {
            meterAnimator.SetBool("MeterIsFull", true);
        }
        if (slider.value != slider.maxValue)
        {
            meterAnimator.SetBool("MeterIsFull", false);
        }
    }
}
