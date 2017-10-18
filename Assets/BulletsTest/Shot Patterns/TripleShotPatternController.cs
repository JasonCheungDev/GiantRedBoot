using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShotPatternController : MonoBehaviour {

    Rigidbody2D shotrb;
    public GameObject shotType;
    public float speed;
    public float fireRate = 0.5f;
    private float nextFire = 0.0f;

    // Use this for initialization
    void Start () {
        shotrb = shotType.GetComponent<Rigidbody2D>();
        shotrb.GetComponent<Mover>().speed = speed;
        //shotrb.velocity = -transform.up * speed;
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shotType, transform.position, transform.rotation);
        }
    }
}
