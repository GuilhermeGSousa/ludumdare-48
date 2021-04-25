using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Probe : MonoBehaviour
{
    [SerializeField] float probeSpeed = 10f;
    Rigidbody2D rb2d;
    bool hasCollided = false;

    // Start is called before the first frame update
    void Start()
    {
        hasCollided = false;
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!hasCollided)
        {
            rb2d.AddForce(transform.up * probeSpeed);
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if(!hasCollided)
        {
            hasCollided = true;
            rb2d.isKinematic = true;
            rb2d.velocity = Vector2.zero;
            
        }

    }
}
