using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;

public class PhotoManager : MonoBehaviour {
    public static PhotoManager instance = null;

    [SerializeField] GameObject photoBook = null;
    [SerializeField] float waitTime = 4.0f;
    public GameObject photoScore;

    private float timer = 0.0f;

    private bool gameIsDone = false;

    private void Awake() {
        if(instance == null) instance = this;
        else
            Destroy (gameObject);
        
    }

    private void Start() {
    }

    private void Update() {
        if(gameIsDone) {
            timer += Time.deltaTime;
            if (timer > waitTime)
            {
                gameIsDone = false;
                SceneManager.LoadScene("EndGame");
            }
        }
    }

    public List<FishBehaviour.FishType> types = new List<FishBehaviour.FishType>();

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
            checkEndGame();
            return true;
        }
        return false;
    }

    public void checkEndGame()
    {
        TMPro.TMP_Text txt = photoScore.GetComponent<TMPro.TMP_Text>();
        txt.text = types.Count.ToString() + "/" + FIshSpawner.instance.typesPresent.Count.ToString();
        if(types.Count == FIshSpawner.instance.typesPresent.Count)
        {
            GameObject panel = photoBook.transform.Find("PhotoBookPanel").gameObject;
            panel.SetActive(true);

            gameIsDone = true;
        }
    }
}