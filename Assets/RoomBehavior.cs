using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomBehavior : MonoBehaviour
{
    public bool Fixed = false;
    public Color whenFixed = new Color(0.0f,1.0f,0.0f,0.75f);
    public Color whenBroken = new Color(1.0f,0.0f,0.0f,0.75f);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Image sr = GetComponent<Image>();
        if(Fixed)
        {
            sr.color = whenFixed;
        }
        else{
            sr.color = whenBroken;
        }
    }

    public void GoToRoom() {
        // TODO start minigame
        Fixed = ! Fixed;
    }
}
