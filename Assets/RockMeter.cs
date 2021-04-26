using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockMeter : MonoBehaviour
{
    int rm, rm_max;
    GameObject needle;

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
        float new_x = (float) rm / rm_max;
        needle.transform.localPosition = new Vector3(new_x, 0, 0);
    }
}
