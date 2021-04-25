using UnityEngine;
using System.Collections.Generic;

public class PhotoManager : MonoBehaviour {
    public static PhotoManager instance = null;

    [SerializeField] GameObject photoBook = null;

    private void Awake() {
        if(instance == null) instance = this;
        else
            Destroy (gameObject);

    }

    List<FishBehaviour.FishType> types = new List<FishBehaviour.FishType>();

    public bool TrytoAdd(FishBehaviour.FishType type) {
        if (!types.Contains(type)) {
            types.Add(type);
            
            var player = GameObject.FindGameObjectWithTag("Player");
            GameObject photoCamera = player.transform.Find("PhotoCamera").gameObject;
            Camera camera = photoCamera.GetComponent<Camera>();
            var currentRT = RenderTexture.active;
            RenderTexture.active = camera.targetTexture;

            // Render the camera's view.
            camera.Render();

            // Make a new texture and read the active Render Texture into it.
            Texture2D image = new Texture2D(camera.targetTexture.width, camera.targetTexture.height, TextureFormat.RGB24, false);
            image.ReadPixels(new Rect(0, 0, camera.targetTexture.width, camera.targetTexture.height), 0, 0, false);
            image.Apply();

            var photoBookComp = photoBook.GetComponent<PhotoBook>();

            photoBookComp.AddPicture(image);

            // Replace the original active Render Texture.
            RenderTexture.active = currentRT;

            return true;
        }
        return false;
    }
}