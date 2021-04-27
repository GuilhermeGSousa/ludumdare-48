using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBehaviour : MonoBehaviour
{
    public enum FishType // your custom enumeration
    {
        ElectricEel, 
        Tamboril, 
        Medusa,
        Kraken,
        RedFish,
        YellowFish,
        Poulpitos
    };

    [SerializeField] public FishType type;
    [SerializeField] DepthLayerNames[] predilectionLayers;
    [SerializeField] float speed = 1.0f;

    [SerializeField] float speedAnimationFactor = 1.0f;
    [SerializeField] float scaredSpeed = 5.0f;
    [SerializeField] float direction = 0.0f;
    [SerializeField] float rotationSpeed = 1.0f;
    [SerializeField] bool doFlips = false;
    [SerializeField] bool isFlipped = false;
    [SerializeField] float maxSpeed = 5.0f;
    [SerializeField] float submarineMinSpeed = 1f;

    public float minTimeChangeDirection = 2.0f;
    public float maxTimeChangeDirection =5.0f;
    float currentDirection = 0.0f;
    float inclinationT = 0.0f;
    public Vector2 forwardVector = Vector2.right;
    float previousDirection = 0.0f;
    float boostedSpeed = 0.0f;
    bool canChooseDirection = true;
    float newDirectionTime;



    Rigidbody2D rb2d;
    Collider2D bounds;
    CircleCollider2D closeArea;
    public Vector2 minMaxDepth;

    // Start is called before the first frame update
    void Start()
    {
        int rand = Random.Range(0, predilectionLayers.Length-1);
        DepthLayerNames layerName = predilectionLayers[rand];
        DepthLayer spawningLayer = DepthBehaviour.instance.getLayerNamed(layerName);

        minMaxDepth = DepthBehaviour.instance.getMinMaxDepth(spawningLayer);
        int y = Random.Range((int)minMaxDepth.x, (int)minMaxDepth.y);

        bounds = GameObject.FindGameObjectWithTag("Bounds").GetComponent<Collider2D>();
        float x = Random.Range(bounds.bounds.min.x, bounds.bounds.max.x);

        gameObject.transform.position = new Vector2(x, -y);

        rb2d = GetComponent<Rigidbody2D> ();
        closeArea = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        ModifyPhysics();
        float currentSpeed = (speed+boostedSpeed) * speedAnimationFactor;
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

            rb2d.AddRelativeForce(Quaternion.Euler(0, 0, currentDirection) * forwardVector*currentSpeed );
        }
        else 
        { 
            //Smooth rotation
            transform.rotation = Quaternion.Euler(0, 0, currentDirection);

            rb2d.AddRelativeForce(forwardVector*currentSpeed );
        }

        previousDirection = currentDirection;
        boostedSpeed = 0.0f;
    }

    private void CorrectDirectionToBounds()
    {
        Vector2 correctedDirection = Vector2.zero;

        if(transform.position.x < bounds.bounds.min.x)
        {
            correctedDirection += Vector2.right;
        }
        else if(transform.position.x > bounds.bounds.max.x)
        {
            correctedDirection += Vector2.left;
        }

        if(transform.position.y < -minMaxDepth.y)
        {
            correctedDirection += Vector2.up;
        }
        else if(transform.position.y > -minMaxDepth.x)
        {
            correctedDirection += Vector2.down;
        }
        if(correctedDirection != Vector2.zero)
        {
            direction = Vector2.SignedAngle(forwardVector, correctedDirection);
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
        newDirectionTime = Random.Range(minTimeChangeDirection, maxTimeChangeDirection);
        yield return new WaitForSeconds(newDirectionTime);
        canChooseDirection = true;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(scaredSpeed != 0  && other.gameObject.CompareTag("Player"))
        {
            Rigidbody2D submarine = other.gameObject.GetComponent<Rigidbody2D>();
            if(submarine.velocity.magnitude > submarineMinSpeed)
            {
                // multiply by scared speed to make negative scared speed make follow / attack
                Vector2 escapeDirection =(transform.position - other.gameObject.transform.position)*scaredSpeed;

                direction = Vector2.SignedAngle(forwardVector, escapeDirection);
                boostedSpeed = scaredSpeed;
                // Debug.Log("Fish : "+type+" was scared at : "+escapeDirection * scaredSpeed*speedAnimationFactor);
            }
        }
    }
}
