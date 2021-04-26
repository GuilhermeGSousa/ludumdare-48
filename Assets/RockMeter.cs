using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockMeter : MonoBehaviour
{
    int rm, rm_max;
    GameObject needle;
    double max_distance = 1.5;

    // Start is called before the first frame update
    void Start()
    {
        needle = transform.Find("Needle").gameObject;
        rm_max = PlayerPrefs.GetInt("RockMeterMax");
    }

    // Update is called once per frame
    void Update()
    {
        rm = PlayerPrefs.GetInt("RockMeter");
        float new_x_norm = (float) rm / rm_max;
        needle.transform.localPosition = new Vector3((new_x_norm * (float)max_distance), 0, 0);
    }
}
