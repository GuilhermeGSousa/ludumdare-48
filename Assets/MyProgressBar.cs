using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyProgressBar : MonoBehaviour
{
    public Slider slider;
    public bool critical = false;
    public GameObject fill;
    public Color fillColor;
    public Color criticalColor;

    // Start is called before the first frame update
    public void SetOxygen(float percentage)
    {
        slider.value = percentage;
        if(critical)
        {
            if(slider.value<0.3f)
            {
                fill.GetComponent<Image>().color = criticalColor;
            }
            else{
                fill.GetComponent<Image>().color = fillColor;
            }
        }
    }
}
