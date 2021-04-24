using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Submarine : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float thrust = 1f;
    [SerializeField] float maxSpeed = 5f;
    [SerializeField] float linearDrag = 5f;

    [Header("Lights")]
    [SerializeField] Transform frontLight;

    Vector2 controlDirection;
    Vector3 mousePosition;
    Camera mainCamera;
    bool isFlipped = false;
    Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D> ();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Move();
        UpdateLights();
        ModifyPhysics();
    }

    private void UpdateLights()
    {
        Vector3 distancePivotVector = mousePosition - frontLight.position;
        
        float angle = Mathf.Atan2(distancePivotVector.y, distancePivotVector.x) * Mathf.Rad2Deg - 90;

        if(Mathf.Abs(angle) > 90)
        {
            frontLight.rotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            frontLight.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    private void GetInput()
    {
        controlDirection = new Vector2(Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    private void ModifyPhysics()
    {
        rb2d.velocity = Vector2.ClampMagnitude(rb2d.velocity, maxSpeed);
    }

    private void Move()
    {
        rb2d.AddForce(Vector2.right * thrust * controlDirection.x);
        rb2d.AddForce(Vector2.up * thrust * controlDirection.y);

        if(controlDirection.magnitude > 0.2f)
        {
            rb2d.drag = 0.0f;
        }
        else
        {
            rb2d.drag = linearDrag;
        }

        if((controlDirection.x > 0 && isFlipped) || (controlDirection.x < 0 && !isFlipped))
        {
            Flip();
        }
    }

    public void Flip()
    {
        isFlipped = !isFlipped;
        transform.rotation = Quaternion.Euler(0f, isFlipped ? 180f : 0f, 0f);
    }
}
