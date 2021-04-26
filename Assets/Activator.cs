using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{
    SpriteRenderer sr;
    public KeyCode key;
    public KeyCode key_2;
    bool active = false;
    GameObject note;
    Color old;
    public bool checkSyncMode;

    private int score;
    private AudioSource tickSound;

    private float pos_x_activator;

    // Start is called before the first frame update
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        old = sr.color;
        score = 0;
        tickSound = GetComponent<AudioSource>(); // For testing synchronization
        pos_x_activator = GetComponent<Transform>().position.x;
    }


    // Update is called once per frame
    void Update()
    {
        if (checkSyncMode && active)
        {
            if (Mathf.Abs((float)(note.transform.position.x - (pos_x_activator))) < 0.1)
            {
                tickSound.Play();
            }
        }
        if(Input.GetKeyDown(key) || Input.GetKeyDown(key_2))
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
