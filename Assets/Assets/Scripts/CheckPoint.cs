using UnityEngine;
using System.Collections;
/*****************************************************************************************************/
// This class displays the checkpoint every 1000meters or units
/*****************************************************************************************************/	
public class CheckPoint : MonoBehaviour {
/***************************************************************************************************/
// Declare public and private variables
/***************************************************************************************************/
    public float speedVertical = 25;                    // set the velocity of the checkpoint
    public int checkPoint = 1000;                       // we show the checkpoint every 1000 meters
    private float bottomOfScreenPos;
    private int showCheckPoint;
    private bool isCheckPoint = false;
/***************************************************************************************************/
// 	Use this for initialization
/***************************************************************************************************/	
	void Start () {
        showCheckPoint = checkPoint;
        bottomOfScreenPos = this.transform.position.y;	// save the original Y position for the gameobject
	}
/***************************************************************************************************/
// Update is called once per frame
/***************************************************************************************************/
    void Update() {
    
        if (Global.lengthRun >= showCheckPoint)
        {
            Global.level++;
            showCheckPoint += 1000;
            isCheckPoint = true;
        }

        // if the flag is true we continue to execute the statements until set false when the checkpoint object has reached the top of the viewport
        if (isCheckPoint)
        {
            SetUpObjectPosition();
            BoundsCheck();
        }
	}
/***************************************************************************************************/
// Move the checkpoint gameobject vertically
/***************************************************************************************************/
    void SetUpObjectPosition() {
            this.transform.Translate(Vector3.up * speedVertical * Time.deltaTime);                  
    }
/***************************************************************************************************/
// 	Check if the checkpoint gameobject is out of bounds
/***************************************************************************************************/
    void BoundsCheck()
    {
        // check if the gameobject has passed the top of the viewport and if so reset it to the bottom of the viewport 
        if (this.transform.position.y >= 40)
        {
            this.transform.position = new Vector3(this.transform.position.x, bottomOfScreenPos,  this.transform.position.z);
            isCheckPoint = false;
            Global.coin += 50;        // increase the score by 1000
        }
    }
}
/***************************************************************************************************/
//
// 	End of Class
//	
/***************************************************************************************************/