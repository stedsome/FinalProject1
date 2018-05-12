using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt_Mover : MonoBehaviour {
    private Rigidbody bolt;
    public GameObject boltobj;
    public float speed;

    private void Start()
    {
        bolt = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        bolt.velocity = transform.up * speed;
        if (transform.position.y < -11)
            Destroy(boltobj);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "baddie1" || collision.tag == "bigbaddie1" || collision.tag == "boss")
        {
            Destroy(boltobj);
            if (collision.tag == "boss")
            {
                collision.gameObject.GetComponent<BossControl>().TakeDamage(Global.attack);
            }
            else
            {
                collision.gameObject.GetComponent<Enemy>().TakeDamage(Global.attack);
            }
        }
    }
}
