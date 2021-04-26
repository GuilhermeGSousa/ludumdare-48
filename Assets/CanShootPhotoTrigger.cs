using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanShootPhotoTrigger : MonoBehaviour
{
    [SerializeField] GameObject submarine = null;

    Submarine sub = null;

    Collider2D col = null;
    // Start is called before the first frame update
    void Start()
    {
        sub = submarine.GetComponent<Submarine>();
        col = gameObject.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] colliders = new Collider2D[50];
        ContactFilter2D filter2D = new ContactFilter2D();
        filter2D.NoFilter();
        col.OverlapCollider(filter2D, colliders);

        foreach(Collider2D c in colliders) {
            if(c != null && c.gameObject != null && c.gameObject.tag == "Fish") {
                sub.canTakePictureOf = c.gameObject;
            }
        }
    }
}
