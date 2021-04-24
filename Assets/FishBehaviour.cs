using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBehaviour : MonoBehaviour
{
    public enum FishType // your custom enumeration
    {
        ElectricEel, 
        Tamboril, 
        Nemo
    };

    public FishType type;
    public float speed;

    public DepthLayerNames[] predilectionLayers;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
