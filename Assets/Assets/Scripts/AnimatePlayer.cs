using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatePlayer : MonoBehaviour {

    public Texture LHSAnimationWalk;
    public Texture RHSAnimationWalk;
    public float monsterAnimationSpeed = 1f;
    private float animationInterval = 1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.GetComponent<Renderer>().enabled = true;
        animationInterval -= Time.deltaTime;

        if ((animationInterval > 0.95))
        {                               // show the texture for 50 milliseconds
            this.GetComponent<Renderer>().material.mainTexture = LHSAnimationWalk;
        }

        if (animationInterval <= 0.95)
        {                               // show the texture for 50 milliseconds
            this.GetComponent<Renderer>().material.mainTexture = RHSAnimationWalk;
        }

        if ((animationInterval < 0.90))
        {                               // reset the animation interval after 50milliseconds
            animationInterval = monsterAnimationSpeed;                  // so we can continuously loop the animation effect
        }
    }
}
