using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class ShowTuto : MonoBehaviour
{
    [SerializeField] int nbVideoClips;
    Animator animator = null;

    int currentVideoPlayed = 0;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    void OnAwake() {
        if(nbVideoClips > 0 && currentVideoPlayed < nbVideoClips) {
            animator.SetInteger("index", currentVideoPlayed);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void NextVideo() {
        if(nbVideoClips > 0 && currentVideoPlayed < nbVideoClips - 1 ) {
            currentVideoPlayed += 1;
            animator.SetInteger("index", currentVideoPlayed);
        }
        else {
            SceneManager.LoadScene("MainScene");
        }
    }
}
