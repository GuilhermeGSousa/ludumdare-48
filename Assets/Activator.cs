using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{
    SpriteRenderer sr;
    public KeyCode key_w;
    public KeyCode key_a;
    public KeyCode key_s;
    public KeyCode key_d;
    public KeyCode key_up;
    public KeyCode key_lft;
    public KeyCode key_bot;
    public KeyCode key_rgt;
    bool active = false;
    GameObject note, gm;
    Color old;
    public bool checkSyncMode;

    public int score;
    private AudioSource tickSound;

    private float pos_x_activator;

    // Start is called before the first frame update
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        gm = GameObject.Find("MiniGamePropelleGameManager");
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
        if(Input.GetKeyDown(key_w)
          || Input.GetKeyDown(key_a)
          || Input.GetKeyDown(key_s)
          || Input.GetKeyDown(key_d)
          || Input.GetKeyDown(key_up)
          || Input.GetKeyDown(key_lft)
          || Input.GetKeyDown(key_bot)
          || Input.GetKeyDown(key_rgt))
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
        if(col.gameObject.tag == "Note_up"
           || col.gameObject.tag == "Note_left"
           || col.gameObject.tag == "Note_down"
           || col.gameObject.tag == "Note_right")
            note = col.gameObject;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        active = false;
    }

    void AddScore()
    {
        score += gm.GetComponent<MiniGamePropelleGameManager>().GetScore();
    }

    IEnumerator Pressed()
    {
        sr.color = new Color(0,0,0);
        yield return new WaitForSeconds(0.05f);
        sr.color = old;
    }
}
