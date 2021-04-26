using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProbeGun : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject probe;
    [SerializeField] float totalProbeCooldownTime = 20;
    [SerializeField] MyProgressBar probeBar;
    float timeSinceLastProbe = 0;
    public GameObject probeNet;

    public bool canshoot = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timeSinceLastProbe <= totalProbeCooldownTime)
            timeSinceLastProbe += Time.deltaTime;

        probeBar.SetOxygen(timeSinceLastProbe / totalProbeCooldownTime);

        if(timeSinceLastProbe > totalProbeCooldownTime)
        {
            canshoot = true;
        }

    }

    public void Shoot()
    {
        if (canshoot)
        {
            GameObject probeInstance = Instantiate(probe, transform.position, transform.rotation);
            probeInstance.transform.SetParent(probeNet.transform);
            timeSinceLastProbe = 0;
            canshoot = false;
        }
    }
}
