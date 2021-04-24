using UnityEngine;
using System;

[System.Serializable]
public class DepthLayer {
    public int span;
    public string name;
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

    DepthLayer getLayerAt(int depth) {
        foreach(DepthLayer layer in layers) {
            depth -= layer.span;
            if(depth < 0)
                return layer;
        }

        return null;
    }
    
}