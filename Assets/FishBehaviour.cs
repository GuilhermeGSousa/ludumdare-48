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
    [SerializeField] float speed = 1.0f;
    [SerializeField] float scaredSpeed = 5.0f;
    [SerializeField] float direction = 0.0f;
    [SerializeField] float rotationSpeed = 1.0f;
    [SerializeField] bool doFlips = false;
    [SerializeField] bool isFlipped = false;
    [SerializeField] float maxSpeed = 0.5f;
    [SerializeField] float submarineMinSpeed = 1f;
    float currentDirection = 0.0f;
    float inclinationT = 0.0f;

    float previousDirection = 0.0f;
    float previousSpeed = 0.0f;
    bool canChooseDirection = true;
    float newDirectionTime;

    Rigidbody2D rb2d;
    Collider2D bounds;
    CircleCollider2D closeArea;

    // Start is called before the first frame update
    void Start()
    {
        int rand = Random.Range(0, predilectionLayers.Length-1);
        DepthLayerNames layerName = predilectionLayers[rand];
        DepthLayer spawningLayer = DepthBehaviour.instance.getLayerNamed(layerName);

        Vector2 minMaxDepth = DepthBehaviour.instance.getMinMaxDepth(spawningLayer);
        int y = Random.Range((int)minMaxDepth[0], (int)minMaxDepth[1]);

        bounds = GameObject.FindGameObjectWithTag("Bounds").GetComponent<Collider2D>();
        float x = Random.Range(bounds.bounds.min.x, bounds.bounds.max.x);

        //gameObject.transform.position = new Vector2(x, -y);

        rb2d = GetComponent<Rigidbody2D> ();
        closeArea = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        ModifyPhysics();

        if(canChooseDirection)
            StartCoroutine("ChooseDirection");

        CorrectDirectionToBounds();

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

            rb2d.AddRelativeForce(Quaternion.Euler(0, 0, currentDirection) * new Vector2(speed, 0.0f) );
        }
        else 
        { 
            //Smooth rotation
            transform.rotation = Quaternion.Euler(0, 0, currentDirection);

            rb2d.AddRelativeForce(new Vector2(speed, 0.0f) );
        }

        previousDirection = currentDirection;
        previousSpeed = speed;
    }

    private void CorrectDirectionToBounds()
    {
        Vector2 correctedDirection = Vector2.zero;

        if(transform.position.x < bounds.bounds.min.x)
        {
            correctedDirection += Vector2.right;
        }
        else if(transform.position.x > bounds.bounds.max.y)
        {
            correctedDirection += Vector2.left;
        }

        if(transform.position.y < bounds.bounds.min.y)
        {
            correctedDirection += Vector2.down;
        }
        else if(transform.position.y > bounds.bounds.max.y)
        {
            correctedDirection += Vector2.up;
        }

        if(correctedDirection != Vector2.zero)
        {
            direction = Vector2.Angle(Vector2.right, correctedDirection);
        }
    }

    public void Flip()
    {
        isFlipped = !isFlipped;
        transform.rotation = Quaternion.Euler(0f, isFlipped ? 180f : 0f, 0f);
    }

    private void ModifyPhysics()
    {
        rb2d.velocity = Vector2.ClampMagnitude(rb2d.velocity, maxSpeed);
    }


    private IEnumerator ChooseDirection()
    {
        rb2d.velocity = rb2d.velocity * 0.8f;
        canChooseDirection = false;
        direction =  Random.Range(0, 360);
        newDirectionTime = Random.Range(2, 5);
        yield return new WaitForSeconds(newDirectionTime);
        canChooseDirection = true;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Rigidbody2D submarine = other.gameObject.GetComponent<Rigidbody2D>();
            
            if(submarine.velocity.magnitude > submarineMinSpeed)
            {
                Vector2 escapeDirection = transform.position - other.gameObject.transform.position;

                direction = Vector2.Angle(Vector2.right, escapeDirection);

                rb2d.AddForce(escapeDirection * scaredSpeed);
            }
        }
    }
}
