using UnityEngine;
using System;
using System.Collections;

//Just for demonstration, you can replace it with your own code logic.
public class BossControl: MonoBehaviour {

    public float speedHorizontal = 25f;
    public float speedVertical = 0;
    public GameObject fireball;
    public GameObject explosion;
    public float attackSPD;
    public float attackTimer;
    public Transform shotSpawn;
    public AudioClip fireballAudio;
    public float bossRHP = Global.Boss_Health;

    public SimpleHealthBar healthBar;

    private Animator animator;
	private float walkStartTime = 0;
	private bool isEvade = false;
    
    private bool moveStateRight = false;

    void Start () {
		animator = this.GetComponent<Animator> ();
	}

	void Update(){

		int horizontal = 0;  
		int vertical = 0;		

		horizontal = (int)(Input.GetAxisRaw ("Horizontal"));
		vertical = (int)(Input.GetAxisRaw ("Vertical"));

		if (horizontal != 0) {
			vertical = 0;
		}

		Vector3 localScale = this.transform.localScale;
		Vector3 velocity = Vector3.zero;
		Vector3 newPosition = Vector3.zero;

		
		animator.SetTrigger("run");
        if (moveStateRight)
        {
            
            this.transform.Translate(0.8f * (Time.deltaTime * speedHorizontal), 0.1f * (Time.deltaTime * speedVertical), 0);
        }
        else
        {
            this.transform.Translate(-0.8f * (Time.deltaTime * speedHorizontal), 0.1f * (Time.deltaTime * speedVertical), 0);
        }


        if (this.transform.position.x >= 17.9)
        {
            this.transform.localScale = new Vector3(-localScale.x, localScale.y, localScale.z);
            moveStateRight = false;
        }
        else if (this.transform.position.x <= -18.7)
        {
            this.transform.localScale = new Vector3(-localScale.x, localScale.y, localScale.z);
            moveStateRight = true;
        }

        if (Global.GameState == Global.state.Play && Time.time > attackTimer)
        {
            GetComponent<AudioSource>().PlayOneShot(fireballAudio);
            Instantiate(fireball, shotSpawn.position, shotSpawn.rotation);
            attackTimer = Time.time + attackSPD;
        }

		if (bossRHP < 20){
				animator.SetTrigger("skill_1");
            GameObject.Find("GameController").GetComponent<GameController>().spawnWaves(8);
		}

        healthBar.UpdateBar(bossRHP, Global.Boss_Health);

    }

	public IEnumerator Evade(){
		yield return new WaitForSeconds (0.2f);
		isEvade = true;
		yield return new WaitForSeconds (0.2f);
		isEvade = false;
	}

    public void TakeDamage(float attack)
    {
            bossRHP = bossRHP - attack;
            if (bossRHP <= 0)
            {
                Global.isBoss = false;
                Global.lengthRun++;
                GameObject boss = GameObject.Find("boss");
                boss.SetActive(false);
                GameObject vbx = Instantiate(explosion, transform.position, transform.rotation);
                Destroy(vbx, 2);
                Global.coin += 500;
                bossRHP = Global.Boss_Health;
            }
    }
}
