using UnityEngine;
using System.Collections;
/*****************************************************************************************************/
// This class is responsible for playing the background music 
/*****************************************************************************************************/	
public class MusicManager : MonoBehaviour {

    public AudioClip bgMusic1;
	public float timeSound, timeDelay = 100f;
	
/***************************************************************************************************/	
// Update is called once per frame
/***************************************************************************************************/		
	void Update () {	
		PlayBgMusic(bgMusic1);									
	}
/***************************************************************************************************/
// Play the sound effects for the game. 
/***************************************************************************************************/
    void PlayBgMusic(AudioClip bgMusic)
    {
        if (!GetComponent<AudioSource>().isPlaying)
			if (Global.backgroundMusic == 2 && Time.time > timeSound)
			{						// play the audio clip if enabled
				timeSound = Time.time + timeDelay;
				GetComponent<AudioSource>().clip = bgMusic1;
				GetComponent<AudioSource>().Play();
			}
		
		// turn of the background music if disabled in theme settings screen 
		if (Global.backgroundMusic == 1){
		   timeSound = Time.time - timeDelay;
		   GetComponent<AudioSource>().Stop();
		}
    }
}
/***************************************************************************************************/
//
// 	End of Class
//	
/***************************************************************************************************/