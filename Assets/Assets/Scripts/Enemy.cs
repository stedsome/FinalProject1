using UnityEngine;
using System.Collections;
/***************************************************************************************************/	
//	The Enemy class is responsible for animating and randomly placing the enemy gameobjects 
//  on the x axis.
//	The class also performs bounds checking.
/***************************************************************************************************/	
public class Enemy : MonoBehaviour {

	public Texture  animationState1;
	public Texture  animationState2;
    public GameObject explosion;
	public float    monsterAnimationSpeed = 1000f;
	public float    speedHorizontal = 25f;
	public float    speedVertical = 3f;					// this value determines how fast the gameobject scrolls up the screen

    public float health = 2;
    public float remainingHealth = 2;
    public float    timeDelay = 50;
    public float    randomInterval = 100;

    private int     RorL;
    
	private bool    moveStateRight = true;
	
	private float   bottomOfScreenPos;
    private bool    isActive = false;                   // if true the enemy is active
    private float   randSeedCounter;
/***************************************************************************************************/	
// 	Use this for initialization
/***************************************************************************************************/
	void Start () {
        RorL = Random.Range(0, 2);
        if (RorL < 1)
            moveStateRight = false;
        bottomOfScreenPos = this.transform.position.y;	// save the original Y position for the bad biscuits 			
	}
/***************************************************************************************************/	
// 	Update on each frame
/***************************************************************************************************/	
	void Update () {
	
        //
        // We use the random function to generate a random number between 1 and the user defined value
        // The enemy is delayed for a period of time before being activated
        //
        randSeedCounter = Random.Range(1f, randomInterval);
        if ((randSeedCounter >= timeDelay) && (randSeedCounter <= timeDelay + 1))
            isActive = true;
        //
        // we test the flag, if true animate the enemy else delay from animating
        //
        if (isActive) 
            RandomDisplayBadBiscuit();
	}
/***************************************************************************************************/
// 	Randomise when we display the bad biscuit
/***************************************************************************************************/
    void RandomDisplayBadBiscuit()
    {
        
        DisplayTexture();
        BoundChecking();
        AnimateMonster();
    }
/***************************************************************************************************/	
// 	Check if the baddie is within screen bounds and move left to right
/***************************************************************************************************/	
	void BoundChecking() {
        //
        // check the x bounds for the enemy within the viewport
        //
        if (this.transform.position.x >= 17.9) {
           
            moveStateRight = false;
		} else if (this.transform.position.x <= -18.7) {
            moveStateRight = true;			 			
		}		
	}	
/***************************************************************************************************/	
//
// 	Display the animation texture for walking right/left depending on the animations state
//
/***************************************************************************************************/
	void DisplayTexture() {

        //
        // simple animation that changes the image texture if moveStateRight is true or false
        //
        if (moveStateRight)
			this.GetComponent<Renderer>().material.mainTexture = animationState1;			
		else
			this.GetComponent<Renderer>().material.mainTexture = animationState2;
		
		// if the bad biscuit reaches the top of the viewport/camera we reset to the bottom of the viewport
		if (this.transform.position.y >= 22.5) {
            Destroy(gameObject);
		}					
	}
/***************************************************************************************************/	
//
// 	Move the monster either left or right
//
/***************************************************************************************************/
	void AnimateMonster() {
        //
        // we test moveStateRight if true or false. If true we move the enemy right or left if false
        //
        if (moveStateRight)
            this.transform.Translate(0.8f * (Time.deltaTime * speedHorizontal), 0.1f * (Time.deltaTime * speedVertical), 0);
		else
            this.transform.Translate(-0.8f * (Time.deltaTime * speedHorizontal), 0.1f * (Time.deltaTime * speedVertical), 0);														
				
	}
    public void TakeDamage(float attack)
    {
        if (remainingHealth <= 0)
        {
            Destroy(gameObject);
            Instantiate(explosion, transform.position, transform.rotation);
        }
        else
        {
            remainingHealth = remainingHealth - attack;
            if (remainingHealth <= 0)
            {
                Destroy(gameObject);
                GameObject vbx = Instantiate(explosion, transform.position, transform.rotation);
                Destroy(vbx, 2);
            }
        }
           
    }
}
    
   
/***************************************************************************************************/	
//
// 	End of Class
//	
/***************************************************************************************************/
