using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

    public float speed;

    Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        //rb.transform.Rotate(transform.forward, 180.0f);
        rb.velocity = -transform.up * speed;

        
	}
	
	// Update is called once per frame
	void Update () {
    }
}
