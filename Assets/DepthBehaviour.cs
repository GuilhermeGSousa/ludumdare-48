using UnityEngine;
using System;


public enum DepthLayerNames // your custom enumeration
 {
    Surface, 
    CoralArea, 
    DeepSea,
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

    private void Awake() {
        if(instance == null) instance = this;
        else
            Destroy (gameObject);

        DontDestroyOnLoad(gameObject);
    }
    public DepthLayer[] layers;

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
        foreach(DepthLayer l in layers) {
            if(l != layer)
                minDepth += l.span;
            else
                break;
        }
        return new Vector2(minDepth, minDepth + layer.span);
    }
    
}