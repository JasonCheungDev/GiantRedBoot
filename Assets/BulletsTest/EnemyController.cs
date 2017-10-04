using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public GameObject shot;
    public GameObject shotSpawn;

    public float fireRate = 0.5f;
    private float nextFire = 0.0f;

    public float rotSpeed = 0.5f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if(transform.rotation.z <= -80.0f)
        {
            //transform.Rotate(-Vector3.forward * 200.0f * Time.deltaTime);
            //transform.rotation = Quaternion.Lerp(Quaternion.Euler(0, 0, -90.0f), Quaternion.Euler(0, 0, 90.0f), Time.time * rotSpeed);
        }
        else if(transform.rotation.z >= 80.0f)
        {
            //transform.rotation = Quaternion.Lerp(Quaternion.Euler(0, 0, 90.0f), Quaternion.Euler(0, 0, -90.0f), Time.time * rotSpeed);
        }
            


        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            //GameObject clone = 
            Instantiate(shot, shotSpawn.transform.position, shotSpawn.transform.rotation); // as GameObject;
        }
	}
}
