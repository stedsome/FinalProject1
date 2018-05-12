using UnityEngine;
using System.Collections;
/*****************************************************************************************************/
// The SplashScreen class fades in and out the scene  
/*****************************************************************************************************/	
public class SplashScreen : MonoBehaviour {
		
	public GameObject particleEffect;
	
	private Vector3 startingPosition;	
	private Transform cachedTransform;
	
	private float delayTime = 5f;
	private float currentTime;
	private bool isAudioPlaying;
	
/***************************************************************************************************/	
// Process code before Start function  
/***************************************************************************************************/			
	void Awake() {	
		// reposition the text and image in the centre of the screen		
		cachedTransform = transform;
		startingPosition = cachedTransform.localPosition;
	}	
/***************************************************************************************************/	
// Initialization code
/***************************************************************************************************/			
	void Start () {
		// display the particle effect behind the logo and character		
		Instantiate(particleEffect, new Vector3(transform.position.x, transform.position.y, -0.09f), transform.rotation);				
		// PixelPlacement.com --> screen fader script
		ScreenFade.Fade( Color.black, ScreenFade.CurrentAlpha, 1, 2f, 1, false );		
	}
/***************************************************************************************************/	
// Update is called once per frame
/***************************************************************************************************/			
	void Update () {
		
		// test if the current if it is current time + 3 seconds
		currentTime = Time.fixedTime;
		
		// three seconds have elapsed so start the fade routine
		if (currentTime >= delayTime) {			
			ScreenFade.Fade(Color.black, ScreenFade.CurrentAlpha, 0, 0.4f, 0, false );	
			delayTime = 10000f;			
		}		
	}
/***************************************************************************************************/	
//	Load the actual game scene after x seconds  
/***************************************************************************************************/			
	IEnumerator LoadMainScene(){
		
		yield return new WaitForSeconds(3);
		Application.LoadLevel("GameScene");
	}
/***************************************************************************************************/	
//  
/***************************************************************************************************/			
	void OnDisable(){
		ScreenFade.OnFadeBegin -= HandleFadeBegin;		 
		ScreenFade.OnFadeEnd -= HandleFadeEnd;
		
	}
/***************************************************************************************************/	
//  
/***************************************************************************************************/			
	void OnEnable(){
		ScreenFade.OnFadeBegin += HandleFadeBegin;	 
		ScreenFade.OnFadeEnd += HandleFadeEnd;
	}
/***************************************************************************************************/	
//  
/***************************************************************************************************/			
	void HandleFadeBegin(){
		Debug.Log( "Fade Begin" );
		cachedTransform.localPosition = startingPosition;
	}

/***************************************************************************************************/	
// Once the fade has ended we start the coroutine
/***************************************************************************************************/		
	void HandleFadeEnd(){
		Debug.Log( "Fade End" );
		cachedTransform.localPosition = startingPosition;
		StartCoroutine(LoadMainScene());		
	}
}
/***************************************************************************************************/	
//
// 	End of Class
//	
/***************************************************************************************************/
