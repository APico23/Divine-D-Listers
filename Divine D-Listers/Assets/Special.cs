using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Special : MonoBehaviour
{
    public Slider slider;
    
    public void setMeter(float h)
    {
        slider.value = h;
    }

    public void setMaxMeter(float h)
    {
        slider.maxValue = h;
        slider.value = h;
    }

    public float getMeter() 
    {
        return slider.value;
    }

    public float getMaxMeter() 
    {
        return slider.maxValue;
    }
}
