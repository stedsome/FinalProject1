using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
/***********************************************************************************************************/
//  A generic class for detecting mouse down/touches for the attached gameobject
/***********************************************************************************************************/
public class ButtonPress : MonoBehaviour {

/***************************************************************************************************/
// Declare public and private variables 
/***************************************************************************************************/
    public Texture2D textureUp;				// set the texture for the mouse up state
    public Texture2D textureDown;			// set the texture for the mouse down state
    public SimpleHealthBar healthBar;
/***************************************************************************************************/
// Capture the mouse down button press event for the gameobject
/***************************************************************************************************/
    void OnMouseDown()
    {
        this.gameObject.GetComponent<GUITexture>().texture = textureDown;
    }
/***************************************************************************************************/
// Capture the mouse up button press event for the gameobject
/***************************************************************************************************/
    void OnMouseUp()
    {
        this.gameObject.GetComponent<GUITexture>().texture = textureUp;
        ActionGameObject(this.gameObject.name);
    }
/***************************************************************************************************/
// The script is attached to the following 3 gameobjects and whichever triggers a mouse up event
// the gameState global variable is changed to the current state i.e., if the play button is pressed
// the gamestate is changed to 'play' and as a result the games can start  	
/***************************************************************************************************/
    void ActionGameObject(string gameObject)
    {
        switch (gameObject)
        {
            case "StartGameButton":
                Global.GameState = Global.state.Play;
                healthBar.UpdateBar(Global.remainingHealth, Global.health);
                break;

            case "Sales":
                SceneManager.UnloadSceneAsync("GameScene");
                Destroy(GameObject.Find("BackgroundMusic"));
                GameObject.Find("background_music").GetComponent<AudioSource>().Play();
                GameObject.Find("ShopWindow").GetComponent<ReturnMenu>().ifReturned = 0;
                break;

            case "HighScoresButton":
                Global.GameState = Global.state.HighScores;
            break;

        }

    }
}
/***************************************************************************************************/
//
// 	End of Class
//	
/***************************************************************************************************/
