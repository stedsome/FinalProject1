using UnityEngine;
using System.Collections;
/*****************************************************************************************************/
// This class scrolls the background scenery (tree/rocks) gameobjects up the screen (Y+). 
/*****************************************************************************************************/	
public class ScrollBackground : MonoBehaviour {
	
	public Transform backgroundSceneryLHS;					// Attached transform component for LHS of the screen 
	public Transform backgroundSceneryRHS;					// Attached transform component for RHS of the screen
	
	public float speed = 55f;								// set the vertical scroll speed for the gameobject 
	
	private Vector3 storeOriginalPosition;
	private float yOriginalPosTreeLHS;

/***************************************************************************************************/	
// 	Use this for initialization
/***************************************************************************************************/
	void Start () {
		
		// enable or disable the conponnent based on the Global setting		
		storeOriginalPosition =  this.transform.position;			// save the initial camera x,y,z axis as a Vector3
		
		// store the original y positions for the tree's
		yOriginalPosTreeLHS = backgroundSceneryLHS.position.y;
	}
/***************************************************************************************************/	
// Update is called once per frame
/***************************************************************************************************/	
	void FixedUpdate () {
        Debug.Log(Global.isBoss);
		if ( (Global.GameState == Global.state.Paused))
        {	// not sure this is the best way to do things ???
				// reset the camera to its initial coordinates
				this.transform.position = storeOriginalPosition;
		}
        else if (!Global.isBoss) 
		{
            
            // 
            if ((Global.GameState == Global.state.Play) || (Global.GameState == Global.state.Continue)) 
			    Global.lengthRun ++;
							
			backgroundSceneryLHS.Translate(Vector3.up *  speed * Time.deltaTime);  		
			backgroundSceneryRHS.Translate(Vector3.up *  speed * Time.deltaTime);  		
			
			if (backgroundSceneryLHS.position.y >= 34) {
                backgroundSceneryLHS.position = new Vector3(backgroundSceneryLHS.position.x, yOriginalPosTreeLHS, backgroundSceneryLHS.position.z);	
			}

			if (backgroundSceneryRHS.position.y >= 34) {
                backgroundSceneryRHS.position = new Vector3(backgroundSceneryRHS.position.x, yOriginalPosTreeLHS, backgroundSceneryRHS.position.z);					
			}
		}
	}	
}
/***************************************************************************************************/	
//
// 	End of Class
//	
/***************************************************************************************************/