using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Submarine : MonoBehaviour
{
    public static Submarine instance = null;
    [SerializeField] LayerMask fishLayerMask;
    [Header("Movement")]
    [SerializeField] float thrust = 1f;
    [SerializeField] float maxSpeed = 5f;
    [SerializeField] float linearDrag = 5f;

    [SerializeField] float inclinationSpeed = 1.0f;
    [SerializeField] Vector2 minMaxAngles = new Vector2(-7.0f, 7.0f);

    [Header("Lights")]
    [SerializeField] Transform frontLight;

    [Header("Tools")]
    [SerializeField] float totalOxigenTime = 120;

    [Header("UI")]
    [SerializeField] MyProgressBar oxygenBar;
    float currentOxigenTime;
    Vector2 controlDirection;
    Vector3 mousePosition;
    Camera mainCamera;
    Rigidbody2D rb2d;
    GameObject photoZone = null;

    private Animator animator;

    bool isTurnedLeft = false;

    float currentAngle = 0.0f;

    float inclinationT = 0.0f;
    bool pictureAxis = false;

    public GameObject canTakePictureOf = null;

    private void Awake() {
        if(instance == null) instance = this;
        else
            Destroy (gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D> ();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        currentOxigenTime = totalOxigenTime;

        animator = GetComponent<Animator>();

        photoZone = transform.Find("PhotoZone").gameObject;
    }

    // Update is called once per frame
    void Update()
    {

        GetInput();
        Move();
        UpdateOxygen();
        UpdateLights();
        ModifyPhysics();

        if (pictureAxis) {
            pictureAxis = false;
            TryToTakePicture();
        }
    }

    public void ResetOxygen()
    {
        currentOxigenTime = totalOxigenTime;
    }

    private void UpdateOxygen()
    {
        currentOxigenTime -= Time.deltaTime;

        oxygenBar.SetOxygen(currentOxigenTime / totalOxigenTime);

        if(currentOxigenTime <= 0)
        {
            OnDeath();
        }
    }

    private void OnDeath()
    {
        SceneManager.LoadScene("LostGame");
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
        pictureAxis = Input.GetKeyDown(KeyCode.Return);
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

        float target = 0.0f;

        if (controlDirection.y < 0 ) {
            inclinationT += Time.deltaTime;
            if (isTurnedLeft)
                target = minMaxAngles.y;
            else 
                target = minMaxAngles.x;
        }
        else if  (controlDirection.y > 0 ) {
            inclinationT += Time.deltaTime;
            if (isTurnedLeft)
                target = minMaxAngles.x;
            else
                target = minMaxAngles.y;
        }
        else
            inclinationT = 0.0f;

        currentAngle = Mathf.Lerp(currentAngle, target, inclinationT * inclinationSpeed);

        gameObject.transform.rotation = Quaternion.Euler(0, 0, currentAngle);

        if(controlDirection.x > 0 && isTurnedLeft)
        {
            isTurnedLeft = false;
            animator.SetTrigger("TurnRight");
        }
        else if  (controlDirection.x < 0 && !isTurnedLeft) {
            isTurnedLeft = true;
            animator.SetTrigger("TurnLeft");
        }
    }

    public void TryToTakePicture() {

        
        RaycastHit2D hit = Physics2D.BoxCast(photoZone.transform.position, new Vector3(2f, 2f), 0f, transform.right, 0, fishLayerMask);

        if(hit)
        {
            FishBehaviour fish = hit.collider.gameObject.GetComponent<FishBehaviour>();
            PhotoManager.instance.TrytoAdd(fish.type);
        }
    }
}
