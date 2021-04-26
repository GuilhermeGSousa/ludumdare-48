using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGamePropelleGameManager : MonoBehaviour
{
    int multiplier;
    int streak;
    int points = 100;

    private int needle_current;
    public int max_needle_shift;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetInt("RockMeter", 0);
        PlayerPrefs.SetInt("RockMeterMax", max_needle_shift);
        multiplier = 1;
        streak = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Win()
    {
    }

    void Lose()
    {
    }

    public void AddStreak()
    {
        needle_current = PlayerPrefs.GetInt("RockMeter");
        if (needle_current < max_needle_shift)
        {
            PlayerPrefs.SetInt("RockMeter", needle_current + 1);
        }
        else
        {
            Win();
        }

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
        needle_current = PlayerPrefs.GetInt("RockMeter");
        if (needle_current > -max_needle_shift)
        {
            PlayerPrefs.SetInt("RockMeter", needle_current - 1);
        }
        else
        {
            Lose();
        }

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
