using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBehaviour : MonoBehaviour
{
    public float speed;

    public string[] predilactionLayers;

    // Start is called before the first frame update
    void Start()
    {
        int rand = Random.Range(0, predilactionLayers.Length);
        DepthLayer spawningLayer = DepthBehaviour.instance.layers[rand];

        Vector2 minMaxDepth = DepthBehaviour.instance.getMinMaxDepth(spawningLayer);
        float y = Random.Range(minMaxDepth[0], minMaxDepth[1]);

        Collider2D bounds = GameObject.FindGameObjectWithTag("Bounds").GetComponent<Collider2D>();
        float x = Random.Range(bounds.bounds.min.x, bounds.bounds.max.y);

        gameObject.transform.position = new Vector2(x, y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
