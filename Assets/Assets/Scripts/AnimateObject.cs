using UnityEngine;
using System.Collections;
/*****************************************************************************************************/
// This class displays and animates an attached gameobject vertically on the screen, with a random x position 
/*****************************************************************************************************/	
public class AnimateObject : MonoBehaviour {

/***************************************************************************************************/
// Declare public and private variables
/***************************************************************************************************/
    public float speedVertical = 140;
    public float timeDelay = 50;
    public float randomInterval = 100;

    private float randSeedCounter, randSeed;
    private bool flagUp;
    private float bottomOfScreenPos;
/***************************************************************************************************/
// 	Use this for initialization
/***************************************************************************************************/	
	void Start () {

        bottomOfScreenPos = this.transform.position.y;	// save the original Y position for the gameobject
	}
/***************************************************************************************************/
// Update is called once per frame
/***************************************************************************************************/
    void Update() {
        SetUpObjectPosition();
        BoundsCheck();
	}
/***************************************************************************************************/
// Randomise when placing the object on the x axis and move the object vertically
/***************************************************************************************************/
    void SetUpObjectPosition() {

        randSeedCounter = Random.Range(1f, randomInterval);

        if ((randSeedCounter >= timeDelay) && (randSeedCounter <= timeDelay + 1) && !flagUp)
        {
            randSeed = Random.Range(-16f, 15f);				        // random x position
            this.transform.position = new Vector3(randSeed, this.transform.position.y, this.transform.position.z);
            flagUp = true;
        }

        if (flagUp && !(Global.isBoss))
        {
            this.transform.Translate(Vector3.up * speedVertical * Time.deltaTime);            
        }
    }
/***************************************************************************************************/
// 	Check if the grass clump is out of bounds
/***************************************************************************************************/
    void BoundsCheck()
    {
        // check the x bounds for the baddies 
        if (this.transform.position.y >= 34)
        {
            this.transform.position = new Vector3(this.transform.position.x, bottomOfScreenPos,  this.transform.position.z);
            TestForSpecialObject();
            flagUp = false;
        }
    }
/***************************************************************************************************/
// 	If the gameobject is a biscuit or powerup and has been consumed (eaten), we enable the renderer 
//  again so the object can be seen again
/***************************************************************************************************/
    void TestForSpecialObject()
    {
        if (this.gameObject.GetComponent<Renderer>().enabled == false)
            this.gameObject.GetComponent<Renderer>().enabled = true;        // show the gameobject again (only required for biscuits and powerups)}

    }
}
/***************************************************************************************************/
//
// 	End of Class
//	
/***************************************************************************************************/
