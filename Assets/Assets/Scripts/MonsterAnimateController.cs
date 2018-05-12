 using UnityEngine;
using System.Collections;
/*****************************************************************************************************/
// The MonsterAnimateController class is responsible for animating, display, collision testing...
/*****************************************************************************************************/	
public class MonsterAnimateController : MonoBehaviour {
/***************************************************************************************************/
// Declare public and private variables
/***************************************************************************************************/	
	public Texture2D RHSAnimationWalk;					// set references to the images
	public Texture2D LHSAnimationWalk;
	
	public float speed = 20;
	public AudioClip hurtNoise;
	public AudioClip eatCookie;
	public AudioClip playerFootsteps;
    public AudioClip shootSound;

    public SimpleHealthBar healthBar;
	public GameObject particleEffectDead;
	public GameObject scoreGUI;
	public GameObject mainCamera;
	public float monsterAnimationSpeed = 1f;    
    public float accelerometerSpeed = 10;               // sensitivity of the acceleromter
    public GameObject shot;
    public Transform shotSpawn;

    private float fireTime;
	private float animationInterval = 1f;				// initial monster animation speed for walking	 
	private enum FadeType { Fade, Flash, Injury };
	private bool animationState;
	private bool arrowKeysDirectionLeft = true;
	private bool arrowKeysDirectionRight = true;
	private byte buttonPressCounter = 0;
	private Vector3 tempVector3 = Vector3.zero;				// temporary var to hold a vector 3 from device accelerometer 

    private Vector3 tmpPos;

    private void Start()
    {
        healthBar.UpdateBar(Global.remainingHealth, Global.health);
    }
    /***************************************************************************************************/
    // Update is called once per frame
    /***************************************************************************************************/
    void Update () {
		
		if ((Global.GameState == Global.state.GameOver)) {						
			Global.ResetGame(); 					// the game is over so we reset the game variables to their initial state						
		}
		
		else if ((Global.GameState == Global.state.Play) || (Global.GameState == Global.state.Paused) || (Global.GameState == Global.state.Continue))
		{
			this.GetComponent<Renderer>().enabled = true;			        // show the player character 
            CheckIfGamePaused();		
			GetPlayerInput();	
			CheckMonsterBounds();
			DisplayTexture();
			IsPlayerDead();            
		}
	}
/***************************************************************************************************/	
// 	Pause or unpause the game
/***************************************************************************************************/
	void CheckIfGamePaused () {	
		
		// has the player touched the screen or clicked the screen, if so show the pause GUI Texture
		if (Input.GetMouseButtonDown(0)) {
			buttonPressCounter ++;
			if (buttonPressCounter == 1) {
				scoreGUI.SetActive(false);			// hide the scores
				PauseGame(true);
			}
			else if (buttonPressCounter == 2) {
				scoreGUI.SetActive(true);			// show the scores
				PauseGame(false);	
				buttonPressCounter = 0;				// reset the counter
			}		
		}
	}
/***************************************************************************************************/	
// 	Get player input from a keyboard or touch screen 
/***************************************************************************************************/
	void GetPlayerInput () {
		//
		// Get player input if the game is not paused
		//
        if (Global.GameState != Global.state.Paused) {
			//
			// translate movement from the keyboard direction keys for the player monster
			//
            if (Input.GetKey(KeyCode.Space) && Time.time > fireTime)
            {
                fireTime = Time.time + Global.attackSPD;
                PlaySoundEffect(shootSound);
                Instantiate(shot, shotSpawn.position, shotSpawn.rotation);

            }
			if (Input.GetKey(KeyCode.LeftArrow) && arrowKeysDirectionLeft) 
                // move player left
				this.transform.Translate(Vector3.left * speed * Time.deltaTime);   
			
			if (Input.GetKey(KeyCode.RightArrow) && arrowKeysDirectionRight) 
                // move player right
                this.transform.Translate(Vector3.right * speed * Time.deltaTime);
						
			if (Input.GetKey(KeyCode.UpArrow)) 
				// move player up
				this.transform.Translate(Vector3.up * speed * Time.deltaTime);
						
			if (Input.GetKey(KeyCode.DownArrow)) 
				// move player down
				this.transform.Translate(Vector3.down * speed * Time.deltaTime);
			
			//
			// get input from the device accelerometer for both x and y axis
			//
			tempVector3.x = Input.acceleration.x;						// clamp acceleration vector to the unit sphere
			tempVector3.y = Input.acceleration.y;						// clamp acceleration vector to the unit sphere

			if (tempVector3.sqrMagnitude > 1)
				tempVector3.Normalize();
            //
			// ensure the player character remains with screen bounds
            //
			tmpPos = transform.position;
            tmpPos.x = Mathf.Clamp(tmpPos.x, -21.07f, 20.58f);
			tmpPos.y = Mathf.Clamp(tmpPos.y, -8.07f, 20.58f);
			transform.position = tmpPos;

			//
			// Transform the player monster on the x and y axis (move the player monster)
			//
            this.transform.Translate(tempVector3 * (accelerometerSpeed * (Global.playerAcceleratorSensitivity * 2.5f)) * Time.deltaTime);
		}
	}
/***************************************************************************************************/	
// 	Display the animation texture for walking right/left depending on the animations state
/***************************************************************************************************/
	 void DisplayTexture() {
			
			// Animate the monster if the game is not paused
			if (Global.GameState != Global.state.Paused) {			
				// decrease the animation interval by the deltatime i.e. the time it took for the last frame			
				animationInterval -= Time.deltaTime;						
			
				if ((animationInterval > 0.95) ) {								// show the texture for 50 milliseconds
					this.GetComponent<Renderer>().material.mainTexture = LHSAnimationWalk;
				}

				if (animationInterval <= 0.95) {								// show the texture for 50 milliseconds
					this.GetComponent<Renderer>().material.mainTexture = RHSAnimationWalk;	
				} 			
			
				if ((animationInterval < 0.90) ) {								// reset the animation interval after 50milliseconds
					animationInterval = monsterAnimationSpeed;					// so we can continuously loop the animation effect
				}
			}
	}
/***************************************************************************************************/	
// 	Check the monster is not out of bounds in the x direction
/***************************************************************************************************/
	void CheckMonsterBounds() {

        // check if the player monster is at the RHS of the view and if so, stop moving right
        if (this.transform.position.x >= 20.58)
        {
			arrowKeysDirectionLeft = true;
			arrowKeysDirectionRight = false;       
        // check if the player monster is at the LHS of the view and if so, stop moving left
		} 
        else if (this.transform.position.x <= -21.07) 
        {		
            arrowKeysDirectionLeft = false;
			arrowKeysDirectionRight = true;		    
        // the player monster can move both left and right if within the camera view
		}
        else if (this.transform.position.x > -21.07 && this.transform.position.x < 20.58) 
        {			
            arrowKeysDirectionLeft = true;
			arrowKeysDirectionRight = true;			
		}
	}
/***************************************************************************************************/	
// Detect collisions with the main character
/***************************************************************************************************/		 	
	void OnTriggerEnter(Collider otherObj) {
		if (Global.GameState != Global.state.GameOver)
        {
            //
            // what has the player monster collided with? Cookies = points! BadMonster = decrement lives by one
            //
            if ((otherObj.gameObject.tag == "bigBaddie1") || (otherObj.gameObject.tag == "baddie1"))
            {
                TakeDamage(1);
            }
            else if (otherObj.gameObject.tag == "cookies")
            {
                // total cookies collected and add up points			
                Global.coin += 5;
                PlaySoundEffect(eatCookie);
                otherObj.gameObject.GetComponent<Renderer>().enabled = false;                           // hide the object from the viewport
            }
        }
		
	}

    public void TakeDamage(float attack)
    {
        PlaySoundEffect(hurtNoise);
        // Flash the screen red for a moment to indicate the player has been hurt
        ScreenFade.Fade(Color.red, .6f, 0, .35f, 0, true);
        Global.remainingHealth = Global.remainingHealth - attack;
        healthBar.UpdateBar(Global.remainingHealth, Global.health);
    }
	
/***************************************************************************************************/	
// Check if the player has died
/***************************************************************************************************/		
    void IsPlayerDead() {
		
		if ((Global.remainingHealth <= 0)) {		// game over player is dead	
			// animate the player particle die effect 
			this.GetComponent<Renderer>().enabled = false;			// set the property of the renderer component and set it to false to hide the player character
			Instantiate( particleEffectDead, new Vector3( this.transform.position.x, this.transform.position.y, this.transform.position.z) , transform.rotation );						
			Global.GameState = Global.state.GameOver;
            Global.isBoss = false;
            Global.lengthRun--;
            Global.SaveHighScore(Global.coin, Global.lengthRun);        // Save the score and distance ran 
		}		
	}
/***************************************************************************************************/	
// Pause/Unpause game by setting the current game state
/***************************************************************************************************/		
	void PauseGame(bool isPaused) {
		if (isPaused) 
			Global.GameState = Global.state.Paused;
		else
			Global.GameState = Global.state.Continue;				
	}	
/***************************************************************************************************/	
// Play the sound effects for the game. 
/***************************************************************************************************/		
	void PlaySoundEffect(AudioClip soundEffect) {
	
		if (Global.soundEffects == 2)						// play the audio clip if enabled
			GetComponent<AudioSource>().PlayOneShot(soundEffect);		
	}
}
/***************************************************************************************************/	
//
// 	End of Class
//	
/***************************************************************************************************/