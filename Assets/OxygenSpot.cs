using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenSpot : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Submarine>().ResetOxygen();
        }   
    }

}
