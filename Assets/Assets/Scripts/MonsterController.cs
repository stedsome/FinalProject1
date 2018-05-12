using UnityEngine;
using System.Collections;
/***********************************************************************************************************/
//  A generic class for deciding which monster to display 
//  Extendable by adding more monsters 
/***********************************************************************************************************/
public class MonsterController : MonoBehaviour {

	//
	// To add more player monsters create another public gameobject e.g.,
	//
	// public GameObject playerMonster4;
	//
	// and add another 'case' statement as shown in the Update method
	//
    public GameObject playerMonster1;

/***************************************************************************************************/
// Update is called once per frame
/***************************************************************************************************/
	void Update () {

        //
		// If the game is any of the states as described below, the selected or default player monster is displayed
        // The player monster is set in the ThemeOptionSelection script 
		//
        if ((Global.GameState == Global.state.Play) || (Global.GameState == Global.state.Paused) || (Global.GameState == Global.state.Continue))
        {
            //
			// Here we disable or enable the player monster gameobjects depending on the value of Global.playerCharacterType
            //
                    playerMonster1.SetActive(true);
        }
	}
}
/***************************************************************************************************/
//
// 	End of Class
//	
/***************************************************************************************************/

