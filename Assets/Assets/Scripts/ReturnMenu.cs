using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnMenu : MonoBehaviour {

    public int ifReturned = 0;
    // Use this for initialization
    private void Update()
    {
        if (Input.GetKey(KeyCode.R) && (ifReturned == 0))
        {
            GameObject.Find("background_music").GetComponent<AudioSource>().Stop();
            SceneManager.LoadScene("GameScene",LoadSceneMode.Additive);
            ifReturned = 1;
        }
    }
   
}
