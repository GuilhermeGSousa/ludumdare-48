using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapBehavior : MonoBehaviour
{
    public Transform Rooms;
    public void ToggleMiniMap()
    {
        Animator anim = GetComponent<Animator>();
        if(!anim) return;
        bool current = anim.GetBool("Open");
        anim.SetBool("Open",!current);
        foreach(Transform child in Rooms)
        {
            Button bt  = child.gameObject.GetComponent<Button>();
            bt.enabled = !current;
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
