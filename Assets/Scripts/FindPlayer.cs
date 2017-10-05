using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPlayer : MonoBehaviour {

    public Transform playerTransform;

	void Start () {
        //playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(playerTransform.position.x, playerTransform.position.y, transform.position.z);
	}
}
