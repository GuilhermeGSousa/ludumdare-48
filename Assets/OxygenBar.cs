using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Slider slider;

    // Start is called before the first frame update
    public void SetProgress(float oxygenPercentage)
    {
        slider.value = oxygenPercentage;
    }
}
