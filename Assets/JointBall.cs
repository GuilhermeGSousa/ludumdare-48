using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointBall : MonoBehaviour
{
    // Start is called before the first frame update

    LineRenderer lineRenderer;
    [SerializeField] Transform mouse;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, mouse.position);
    }
}
