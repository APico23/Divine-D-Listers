using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class newHealthBar : MonoBehaviour
{
    public Slider slider;

    public void setHealth(int h)
    {
        slider.value = h;
    }

    public void setMaxHealth(int h)
    {
        slider.maxValue = h; 
        slider.value = h;
    }
}
