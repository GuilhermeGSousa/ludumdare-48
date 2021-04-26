using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGamePropelleGameManager : MonoBehaviour
{
    int multiplier = 1;
    int streak = 0;
    int points = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetScore()
    {
        return points*multiplier;
    }
}
