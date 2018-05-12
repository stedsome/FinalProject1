using UnityEngine;
using System.Collections;
/*****************************************************************************************************/
// This class is responsible for animating/tweening the buttons into the main gamescene
/*****************************************************************************************************/	
public class MainMenu : MonoBehaviour {
	
	// declare all public variables and attach gameobject references
	public GameObject menuButtonPlay;					// display the play GUI Texture	
	public GameObject menuButtonThemeSettings;			// display the themes button
	public GameObject titleBanner;						// display the game title banner GUI Texture 
	public GameObject pauseOverlay;						// display the pause overlay if the game is paused
	public GameObject scoreGUI;							// get the reference to the top level GUI 		

    private float xCoordHighScores;
    private float xCoordPlay;
    private float xCoordTheme;

/***************************************************************************************************/	
// 	Use this for initialization
/***************************************************************************************************/
	void Start () {
		
		AnimateMenusIntoScreen();
	}
/***************************************************************************************************/	
// Update is called once per frame
/***************************************************************************************************/	
	void Update () {
	
		// we find the current game state and perform actions when in that state
		switch (Global.GameState) {
			case Global.state.Menu:
				AnimateMenusIntoScreen();
				break;	
			case Global.state.Play:
				AnimateMenusOutOfScreen();
				break;	
			case Global.state.Paused:
				DisplayPauseScreen(true);
				break;	
			case Global.state.Continue:
				DisplayPauseScreen(false);
				break;
			case Global.state.GameOver:
				AnimateMenusIntoScreen();
				break;			         
		}
	}
/***************************************************************************************************/	
// Animate the menus into the main screen
/***************************************************************************************************/					
	void AnimateMenusIntoScreen() {
		
		scoreGUI.SetActive(false);							// hide the scores once the title banner has been removed
		
		iTween.MoveTo(menuButtonThemeSettings, iTween.Hash("x", 0.725, "easeType", "easeOutExpo", "none", "pingPong", "delay", .2));
		iTween.MoveTo(menuButtonPlay, iTween.Hash("x", 0.51, "easeType", "easeOutExpo", "none", "pingPong", "delay", .1));
		iTween.MoveTo(titleBanner, iTween.Hash("x",0.58,"easeType", "easeOutExpo"));
	}
/***************************************************************************************************/	
// Animate the menus out of the main screen
/***************************************************************************************************/		
	void AnimateMenusOutOfScreen() {
		iTween.MoveTo(menuButtonThemeSettings, iTween.Hash("x", -0.85, "easeType", "easeOutExpo", "none", "pingPong", "delay", .4));
		iTween.MoveTo(menuButtonPlay, iTween.Hash("x", -0.68, "easeType", "easeOutExpo", "none", "pingPong", "delay", .4));
		iTween.MoveTo(titleBanner, iTween.Hash("x",-0.5,"easeType", "easeOutExpo","delay", 0.3, "onComplete" , "ShowTopLevelGUI", "onCompleteTarget", gameObject));		
	}	
/***************************************************************************************************/	
// Display the pause image overlay and or remove it from the screen
/***************************************************************************************************/		
	void DisplayPauseScreen(bool isPaused) {

		if (isPaused)
			pauseOverlay.SetActive(true);
		else
			pauseOverlay.SetActive(false);
	}	
/***************************************************************************************************/	
// Show the top level GUI - called from the callback function iTween.MoveTo(titleBanner,..... 
/***************************************************************************************************/
	void ShowTopLevelGUI() {
        // show the scores once the title banner has been removed and game state = play
        if (Global.GameState == Global.state.Play)
		    scoreGUI.SetActive(true);			
	}
/***************************************************************************************************/	
// Show the themes options GUI -  
/***************************************************************************************************/
	void ShowThemeGUI() {
		scoreGUI.SetActive(true);			
	}
}
/***************************************************************************************************/
//
// 	End of Class
//	
/***************************************************************************************************/