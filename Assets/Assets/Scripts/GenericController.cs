using UnityEngine;
using System.Collections;
/***********************************************************************************************************/
//  A generic class for deciding which objects to display for a theme 
//  Extendable by declaring new gameobjects. 
/***********************************************************************************************************/
public class GenericController : MonoBehaviour {

    public GameObject objectOne;

/***************************************************************************************************/
// Update is called once per frame
/***************************************************************************************************/
    void Update()
    {
        // If the game is any of the states as described below, the selected or default background scenery is displayed
        // The monster is set in the ThemeOptionSelection script
        		
            // The player monster type is given a value from 0 to 2
                    objectOne.SetActive(true);
    }
}
/***************************************************************************************************/
//
// 	End of Class
//	
/***************************************************************************************************/
