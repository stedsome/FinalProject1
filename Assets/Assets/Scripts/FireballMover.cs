using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballMover : MonoBehaviour {

    private Rigidbody fireball;
    public GameObject fbobj;
    public float speed;



    private void Start()
    {
        fireball = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        fireball.velocity = transform.up * speed;
        if (transform.position.y > 22.5)
            Destroy(fbobj);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            Destroy(fbobj);
            collision.gameObject.GetComponent<MonsterAnimateController>().TakeDamage(Global.Boss_Att * Global.level);
        }
    }
}
