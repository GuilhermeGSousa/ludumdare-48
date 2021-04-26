using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGamePropelleGameManager : MonoBehaviour
{
    public int multiplier;
    public int streak;
    int points = 100;

    // Start is called before the first frame update
    void Start()
    {
        multiplier = 1;
        streak = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddStreak()
    {
        streak++;
        if (streak >= 20)
        {
            multiplier = 4;
        }
        else if (streak >= 12)
        {
            multiplier = 3;
        }
        else if (streak >= 4)
        {
            multiplier = 2;
        }
        else
        {
            multiplier = 1;
        }
    }

    public void ResetStreak()
    {
        streak = 0;
        multiplier = 1;
    }

    public int GetScore()
    {
        return points*multiplier;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Note_up"
           || col.gameObject.tag == "Note_left"
           || col.gameObject.tag == "Note_down"
           || col.gameObject.tag == "Note_right")
        {
            Destroy(col.gameObject);
            ResetStreak();
        }
    }
}
