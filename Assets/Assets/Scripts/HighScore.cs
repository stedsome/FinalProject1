using UnityEngine;
using System.Collections;
/*****************************************************************************************************/
// This class is responsible for sorting and displaying the highscores
/*****************************************************************************************************/	
public class HighScore : MonoBehaviour {

    public GUIText score1;
    public GUIText score2;
    public GUIText score3;
    public GUIText score4;
    public GUIText score5;

    public GUIText distance1;
    public GUIText distance2;
    public GUIText distance3;
    public GUIText distance4;
    public GUIText distance5;

 /***************************************************************************************************/
 // Use this for initialization
 /***************************************************************************************************/
    void Start()
    {
        LoadHighScores();
    }
 /***************************************************************************************************/
 // Capture mouse up button press - mouse LHS button has been released 
 /***************************************************************************************************/	
    void OnMouseDown()
    {
        // Change the state of the game so it remains in the main menu
        Global.GameState = Global.state.Menu;
        Application.LoadLevel("Gamescene");
    }

/***************************************************************************************************/
// Load and display the scores + distances from local persistant storage
/***************************************************************************************************/	
    void LoadHighScores()
    {
        score1.text = PlayerPrefs.GetInt("Score1").ToString();
        distance1.text = PlayerPrefs.GetInt("Distance1").ToString();

        score2.text = PlayerPrefs.GetInt("Score2").ToString();
        distance2.text = PlayerPrefs.GetInt("Distance2").ToString();

        score3.text = PlayerPrefs.GetInt("Score3").ToString();
        distance3.text = PlayerPrefs.GetInt("Distance3").ToString();

        score4.text = PlayerPrefs.GetInt("Score4").ToString();
        distance4.text = PlayerPrefs.GetInt("Distance4").ToString();

        score5.text = PlayerPrefs.GetInt("Score5").ToString();
        distance5.text = PlayerPrefs.GetInt("Distance5").ToString();
    }
}
/***************************************************************************************************/
//
// 	End of Class
//	
/***************************************************************************************************/