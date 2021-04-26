using UnityEngine;
using System;
using UnityEngine.Experimental.Rendering.Universal;

public enum DepthLayerNames // your custom enumeration
 {
    Surface, 
    CoralArea, 
    DeepSea,

    DeepWaterMaze,
    Abysses,
    Ground
 };
[System.Serializable]
public class DepthLayer {
    public int span;
    public DepthLayerNames name;
} 



public class DepthBehaviour : MonoBehaviour {

    public static DepthBehaviour instance = null;
    public DepthLayer[] layers;

    public float maxPointLightIntensity = 2.04f;
    public float maxFlahsLightIntensity = 2.95f;

    public float minGlobalLightIntensity = 0.06f;

    [SerializeField] float noLightDepth;
    [SerializeField] Light2D globalLight;
    Submarine submarine;

    GameObject [] pointLights;
    GameObject [] flashLights;

    float startY;

    
    private void Init()
    {
        submarine = GameObject.FindGameObjectWithTag("Player").GetComponent<Submarine>();
        pointLights = GameObject.FindGameObjectsWithTag("PointsLight");
        flashLights = GameObject.FindGameObjectsWithTag("FlashLights");
        startY = submarine.gameObject.transform.position.y;
    }
    private void Awake() {
        if(instance == null) instance = this;
        else
            Destroy (gameObject);

        Init();
    }

    
    // Update is called once per frame
    void Update()
    {
        Vector2 playerPosition = submarine.gameObject.transform.position;

        float diffFactor = Mathf.Abs(playerPosition.y - startY) / Mathf.Abs(noLightDepth - startY);

        globalLight.intensity = Mathf.Max(minGlobalLightIntensity, 1.0f - diffFactor);
        foreach(GameObject obj in pointLights)
        {
            Light2D light = obj.GetComponent<Light2D>();
            light.intensity = maxPointLightIntensity*diffFactor;
        }
        foreach(GameObject obj in flashLights)
        {
            Light2D light = obj.GetComponent<Light2D>();
            light.intensity = maxFlahsLightIntensity*diffFactor;
        }
    }
    public DepthLayer getLayerAt(int depth) {
        foreach(DepthLayer layer in layers) {
            depth -= layer.span;
            if(depth < 0)
                return layer;
        }

        return null;
    }

    public DepthLayer getLayerNamed(DepthLayerNames name) {
        foreach(DepthLayer layer in layers) {
            if(layer.name == name)
                return layer;
        }
        return null;
    }

    public Vector2 getMinMaxDepth(DepthLayer layer) {
        int minDepth = 0;
        if(layer != null) {
            foreach(DepthLayer l in layers) {
                if(l != layer)
                    minDepth += l.span;
                else
                    break;
            }
            return new Vector2(minDepth, minDepth + layer.span);
        }
        return new Vector2(minDepth, minDepth);
    }
    
}