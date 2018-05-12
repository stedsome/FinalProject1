using UnityEngine;
using System.Collections;
using System;
/***************************************************************************************************/	
//	The Global Class is responsible for keeping game state including scoring, lives etc
//	The class also performs methods to save game state such as save high scores.
//  All global data is stored in this class 
/***************************************************************************************************/	
public class Global : MonoBehaviour
{		
/***************************************************************************************************/
// Declare public and private variables for the class
/***************************************************************************************************/									// Set the default number of lives for the monster
	public static int coin = 5000;								// Init the score to zero
	public static int level = 1;									// Set the current level to 0							// Set lives remaining for the monster		
	public static int maxLevels = 10;								// Set the variable to the max levels
	public static int lengthRun = 0;
    public static float base_health = 5;
    public static float equipment_health = 0;
    public static float health;
    public static float base_attack = 1;
    public static float equipment_attack = 0;
    public static float attack;
    public static float remainingHealth;
    public static float attackSPD;
    public static float base_attackSPD = 0.5f;
    public static float attackSPD_modifier;
    public static bool isBoss = false;

    public static float Boss_Att = 5;
    public static float Boss_Health = 20;

    public static byte playerCharacterType = 0;                     // Set the default player index
    public static byte playerThemeType;                             // Contains the theme background index i.e. original, dinosaur
    public static byte playerAcceleratorSensitivity;                // Contains the accelerometer sensitivity index
	public static byte soundEffects;							    // play sound effects as default
	public static byte backgroundMusic;						        // play the background music as default	
	
	public GUIText scoreObject;										// GUI text to display the current score
	public GUIText distanceRunObject;								// GUI text to display the current distance run

    public static float screeneAspectRatio;
    public static Vector2 scaleRatio;
    public static int[,] highScoresTable = new int[10,10];          // Simple 2D array to hold the highscores and distance ran 

/***************************************************************************************************/
// 	Declare the game enumerations
/***************************************************************************************************/	
	public enum state {												// create enemuration for the game states					
	    Play, 
		Paused, 
		GameOver, 
		Continue, 
		Menu,
		ThemeOptions, 
		HighScores						
	};
/***************************************************************************************************/
    public static state GameState = state.Menu;
/***************************************************************************************************/	
// 	Use this for initialization
/***************************************************************************************************/
	void Start () {
        //retrieve the screen width and hight in pixels and calculate the aspect ratio
        health = base_health + equipment_health;
        remainingHealth = health;
        attack = base_attack + equipment_attack;
        attackSPD = base_attackSPD * (1 - attackSPD_modifier);
        Debug.Log(attack);
        screeneAspectRatio = (float)Screen.width / (float)Screen.height;
        RestoreState();
    }								
/***************************************************************************************************/	
// 	Update every frame
/***************************************************************************************************/
	void Update ()
	{
        //
        // Update the current score and the distance run in each GUI Texture
        // See TopGUI parent and PlayerMetricsGUI child gameobject
        //
        health = base_health + equipment_health;
        attack = base_attack + equipment_attack;
        Debug.Log(attack); 
        if ((Global.GameState == Global.state.Play) || (Global.GameState == Global.state.Continue))
        {
            scoreObject.text = coin.ToString();
            distanceRunObject.text = lengthRun.ToString() + "m";
        }

        if (lengthRun % 1000 == 0)
        {
            isBoss = true;
        }
	}
/***************************************************************************************************/
// Save game state to disk (local storage) and perform casting
/***************************************************************************************************/
    public static void SaveState()
    {
        PlayerPrefs.SetInt("ThemeType", (int)playerThemeType);
        PlayerPrefs.SetInt("PlayerType", (int)playerCharacterType);
        PlayerPrefs.SetInt("Accelerometer", (int)playerAcceleratorSensitivity);
        PlayerPrefs.SetInt("SFX", (int)(soundEffects));
        PlayerPrefs.SetInt("BgMusic", (int)(backgroundMusic));
        PlayerPrefs.Save();
    }
/***************************************************************************************************/
// Load the game state i.e. for character et al (if game run first time all values will be null)
/***************************************************************************************************/			
	public static void RestoreState () {
        // Cast from Integer to Byte
        playerThemeType = (byte)PlayerPrefs.GetInt("ThemeType");
        playerCharacterType = (byte)PlayerPrefs.GetInt("PlayerType", 2);
        playerAcceleratorSensitivity = (byte)PlayerPrefs.GetInt("Accelerometer", 2);
        soundEffects =  (byte)(PlayerPrefs.GetInt("SFX", 2));         // if null set default 2 i.e. true for SGX 
        backgroundMusic = (byte)(PlayerPrefs.GetInt("BgMusic", 2));   // if null set default 2 i.e. true for Bg Music 
	}
/***************************************************************************************************/
// Retrieve sorted highscores and distance ran from the table
/***************************************************************************************************/			
	static void GetHighScores() {
        highScoresTable[0, 0] = PlayerPrefs.GetInt("Score1");
        highScoresTable[0, 1] = PlayerPrefs.GetInt("Distance1");
        
        highScoresTable[1, 0] = PlayerPrefs.GetInt("Score2");
        highScoresTable[1, 1] = PlayerPrefs.GetInt("Distance2");
        
        highScoresTable[2, 0] = PlayerPrefs.GetInt("Score3");
        highScoresTable[2, 1] = PlayerPrefs.GetInt("Distance3");

        highScoresTable[3, 0] = PlayerPrefs.GetInt("Score4");
        highScoresTable[3, 1] = PlayerPrefs.GetInt("Distance4");

        highScoresTable[4, 0] = PlayerPrefs.GetInt("Score5");
        highScoresTable[4, 1] = PlayerPrefs.GetInt("Distance5");
	}
/***************************************************************************************************/
// Save the score table and perform shifting i.e. in descending order
/***************************************************************************************************/
    public static void SaveHighScore(int myScore, int myDistance)
    {

        GetHighScores();
        SortScores();

        int[] tempBuffer = new int[5];

        for (int n = 0; n < 5; n++)
            tempBuffer[n] = highScoresTable[n, 0];

            // Loop through the 2D array table 
            for (int indexValue = 0; indexValue < 5; indexValue++)
            {
                // Test if the player score is a new high score
                if (myScore > highScoresTable[indexValue, 0])
                {
                    // Shift the scores down the table
                    for (int counter = indexValue; counter < 5 - indexValue; counter++)
                    {
                        highScoresTable[counter + 1, 0] = tempBuffer[counter];
                    }
                    // Insert new highscore and distance ran
                    highScoresTable[indexValue, 0] = myScore;
                    highScoresTable[indexValue, 1] = myDistance;
                    break;
                }
            }

        PlayerPrefs.SetInt("Score1", highScoresTable[0, 0]);
        PlayerPrefs.SetInt("Score2", highScoresTable[1, 0]);
        PlayerPrefs.SetInt("Score3", highScoresTable[2, 0]);
        PlayerPrefs.SetInt("Score4", highScoresTable[3, 0]);
        PlayerPrefs.SetInt("Score5", highScoresTable[4, 0]);

        PlayerPrefs.SetInt("Distance1", highScoresTable[0, 1]);
        PlayerPrefs.SetInt("Distance2", highScoresTable[1, 1]);
        PlayerPrefs.SetInt("Distance3", highScoresTable[2, 1]);
        PlayerPrefs.SetInt("Distance4", highScoresTable[3, 1]);
        PlayerPrefs.SetInt("Distance5", highScoresTable[4, 1]);
        PlayerPrefs.Save();
	}
/***************************************************************************************************/
// Sort the score in descending order 
/***************************************************************************************************/
    static void SortScores()
    {
        // Loop through the 2D array table 
        for (int indexValue = 0; indexValue < 5; indexValue++)
        {
            for (int sortIndex = 0; sortIndex < 5; sortIndex++)
            {
                // Sort the score table in descending order
                if (highScoresTable[sortIndex, 0] < highScoresTable[sortIndex + 1, 0])
                {
                    // swap positions of scores
                    int buffer = highScoresTable[sortIndex + 1, 0];
                    highScoresTable[sortIndex + 1, 0] = highScoresTable[sortIndex, 0];
                    highScoresTable[sortIndex, 0] = buffer;
                }
            }
        }
    }
/***************************************************************************************************/
// Reset the score table
/***************************************************************************************************/		
    void ResetScore()
    {
        PlayerPrefs.SetInt("Score1", 000000);
        PlayerPrefs.SetInt("Score2", 000000);
        PlayerPrefs.SetInt("Score3", 000000);
        PlayerPrefs.SetInt("Score4", 000000);
        PlayerPrefs.SetInt("Score5", 000000);

        PlayerPrefs.SetInt("Distance1", 000000);
        PlayerPrefs.SetInt("Distance2", 000000);
        PlayerPrefs.SetInt("Distance3", 000000);
        PlayerPrefs.SetInt("Distance4", 000000);
        PlayerPrefs.SetInt("Distance5", 000000);
    }
/***************************************************************************************************/	
// Reset the game by resetting the camera, character and game state variables
/***************************************************************************************************/		
	public static void ResetGame() {  			    
		level = 1;									// set the current level to 0
		maxLevels = 10;								// set the variable to the max levels
        remainingHealth = health;
		lengthRun = 0;
    }			
}
/***************************************************************************************************/	
//
// 	End of Class
//	
/***************************************************************************************************/

