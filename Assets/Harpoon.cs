using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harpoon : MonoBehaviour
{
    // Start is called before the first frame update
    LineRenderer rope;
    [SerializeField] LayerMask hitMask;
    [SerializeField] float range = 10f;
    [SerializeField] float speed = 10f;
    void Start()
    {
        rope = GetComponent<LineRenderer>();
        rope.positionCount = 2;

        rope.SetPosition(0, this.transform.position);
        rope.SetPosition(1, this.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine("Shoot");
    }

    private IEnumerator Shoot()
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition;

        rope.SetPosition(0, startPosition);
        rope.SetPosition(1, startPosition);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, range, hitMask);

        if(hit)
        {
            endPosition = hit.point;
            
        }
        else
        {
            endPosition =  transform.position + transform.right * range;
        }

        Vector3 pos = startPosition;
        float startTimeGo = Time.time;

        while(pos != endPosition)
        {
            pos = Vector3.MoveTowards(startPosition, endPosition, (Time.time - startTimeGo) * speed);
            rope.SetPosition(1, pos);

            yield return null;
        }

        Destroy(this.gameObject);

    }
}
