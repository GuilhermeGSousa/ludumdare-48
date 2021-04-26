using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FishesInfos {
    public int number;
    public GameObject prefab;
} 


public class FIshSpawner : MonoBehaviour
{

    public GameObject net;
    public List<FishesInfos> Fishes = new List<FishesInfos>();

    public List<FishBehaviour.FishType> typesPresent = new List<FishBehaviour.FishType>();

    public static FIshSpawner instance = null;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);    
    }
    // Start is called before the first frame update
    void Start()
    {
        foreach (FishesInfos fishinfos in Fishes)
        {
            if(fishinfos.number > 0) typesPresent.Add(fishinfos.prefab.GetComponent<FishBehaviour>().type);
            for(int i =0; i<fishinfos.number;i++)
            {
                GameObject newfish = Instantiate(fishinfos.prefab, new Vector3(0, 0, 0), Quaternion.identity);
                newfish.SetActive(true);
                newfish.transform.SetParent(net.transform);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
