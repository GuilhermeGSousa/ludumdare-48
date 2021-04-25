using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProbeGun : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject probe;
    [SerializeField] float totalProbeCooldownTime = 20;
    [SerializeField] ProgressBar probeBar;
    float timeSinceLastProbe = 0;
    public GameObject probeNet;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timeSinceLastProbe <= totalProbeCooldownTime)
            timeSinceLastProbe += Time.deltaTime;

        probeBar.SetProgress(timeSinceLastProbe / totalProbeCooldownTime);

        if(Input.GetMouseButtonDown(0) && timeSinceLastProbe > totalProbeCooldownTime)
        {
            GameObject probeInstance = Instantiate(probe, transform.position, transform.rotation);
            probeInstance.transform.SetParent(probeNet.transform);
            timeSinceLastProbe = 0;
        }

    }
}
