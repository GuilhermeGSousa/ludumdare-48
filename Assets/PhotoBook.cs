using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoBook : MonoBehaviour
{
    [SerializeField] GameObject framePrefab; 
    [SerializeField] GameObject page; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPicture(Texture2D image) {
        var pictureGo = Instantiate(framePrefab, new Vector3(0, 0, 0), Quaternion.identity);
        PhotoBehaviour pB = pictureGo.GetComponent<PhotoBehaviour>();
        pB.setImage(image);
        pictureGo.transform.SetParent(page.transform);
    }
}
