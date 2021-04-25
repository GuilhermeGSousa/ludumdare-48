using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBehaviour : MonoBehaviour
{
    public enum FishType // your custom enumeration
    {
        ElectricEel, 
        Tamboril, 
        Nemo,
        Mesusa,
        Kraken
    };

    [SerializeField] FishType type;
    [SerializeField] DepthLayerNames[] predilectionLayers;
    [SerializeField] float speed = 2.0f;
    [SerializeField] float direction = 0.0f;
    [SerializeField] float rotationSpeed = 1.0f;
    [SerializeField] bool doFlips = false;
    [SerializeField] bool isFlipped = false;
    float currentDirection = 0.0f;
    float inclinationT = 0.0f;

    float previousDirection = 0.0f;
    float previousSpeed = 0.0f;


    Rigidbody2D rb2d;


    // Start is called before the first frame update
    void Start()
    {
        int rand = Random.Range(0, predilectionLayers.Length-1);
        DepthLayerNames layerName = predilectionLayers[rand];
        DepthLayer spawningLayer = DepthBehaviour.instance.getLayerNamed(layerName);

        Vector2 minMaxDepth = DepthBehaviour.instance.getMinMaxDepth(spawningLayer);
        int y = Random.Range((int)minMaxDepth[0], (int)minMaxDepth[1]);

        Collider2D bounds = GameObject.FindGameObjectWithTag("Bounds").GetComponent<Collider2D>();
        float x = Random.Range(bounds.bounds.min.x, bounds.bounds.max.x);

        gameObject.transform.position = new Vector2(x, -y);

        rb2d = GetComponent<Rigidbody2D> ();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Approximately(currentDirection, direction))
                inclinationT = 0.0f;
            else
                inclinationT += Time.deltaTime;

            currentDirection = Mathf.Lerp(currentDirection, direction, inclinationT * rotationSpeed);

        if (doFlips) {
            // Mirror left right when turning
            float sin = Mathf.Cos(currentDirection * Mathf.Deg2Rad);
            if((sin >= 0.0f && isFlipped) || (sin < 0.0f && !isFlipped))
            {
                Flip();
            }

            if (!Mathf.Approximately(previousSpeed, speed))
                rb2d.AddRelativeForce(Quaternion.Euler(0, 0, currentDirection) * new Vector2(speed, 0.0f) );
        }
        else 
        { 
            //Smooth rotation
            transform.rotation = Quaternion.Euler(0, 0, currentDirection);

            if (!Mathf.Approximately(previousSpeed, speed))
                rb2d.AddRelativeForce(new Vector2(speed, 0.0f) );
        }

        previousDirection = currentDirection;
        previousSpeed = speed;
    }
    public void Flip()
    {
        isFlipped = !isFlipped;
        transform.rotation = Quaternion.Euler(0f, isFlipped ? 180f : 0f, 0f);
    }
}
