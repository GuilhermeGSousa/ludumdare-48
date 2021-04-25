using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{
    SpriteRenderer sr;
    public KeyCode key;
    bool active = false;
    GameObject note;
    Color old;

    public int score;

    // Start is called before the first frame update
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        old = sr.color;
        score = 0;
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(key))
        {
            StartCoroutine(Pressed());
            if(active)
            {
                Destroy(note);
                AddScore();
                active = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        active = true;
        if(col.gameObject.tag == "Note")
            note = col.gameObject;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        active = false;
    }

    void AddScore()
    {
        score += 100;
    }

    IEnumerator Pressed()
    {
        sr.color = new Color(0,0,0);
        yield return new WaitForSeconds(0.05f);
        sr.color = old;
    }
}
