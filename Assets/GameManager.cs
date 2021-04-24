using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager instance = null;

    [SerializeField] float noLightDepth;
    [SerializeField] Light2D globalLight;
    Submarine submarine;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);    

        DontDestroyOnLoad(gameObject);

        Init();
    }

    private void Init()
    {
        submarine = GameObject.FindGameObjectWithTag("Player").GetComponent<Submarine>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 playerPosition = submarine.gameObject.transform.position;

        if(playerPosition.y > -noLightDepth)
        {
            globalLight.intensity = 1.0f - Mathf.Abs(playerPosition.y) / noLightDepth;
        }
        else
        {
            globalLight.intensity = 0;
        }
    }
}
