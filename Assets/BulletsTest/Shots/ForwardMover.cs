using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardMover : MonoBehaviour {

    public float speed;

    Rigidbody2D rb;

    //private Vector3 axis;
    //private Vector3 pos;

    //public float angle;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();

       // pos = transform.position;
        //axis = new Vector3(0.0f, -1.0f, 0.0f);

        //gameObject.transform.Rotate(0.0f, 0.0f, angle);
        rb.velocity = -gameObject.transform.up * speed;
    }
	
	// Update is called once per frame
	void Update () {
        //pos += -gameObject.transform.up * Time.deltaTime * speed;
        //rb.transform.position = pos + axis;

        
    }
}
