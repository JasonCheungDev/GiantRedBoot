using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpreadShotPatternController : MonoBehaviour {

    public UnityEvent shotFired;

    Rigidbody2D shotrb;
    public GameObject shotType;
    public float speed;
    public float fireRate = 0.5f;
    [Tooltip("Degrees to rotate left and right")]
    public float spreadAngle = 30f;
    public float angleBetweenShots = 5f;
    private float nextFire = 0.0f;


    // Use this for initialization
    void Start () {
        shotrb = shotType.GetComponent<Rigidbody2D>();
        shotrb.GetComponent<ForwardMover>().speed = speed;
        //shotrb.velocity = -transform.up * speed;
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            // fire one shot down the middle
            Instantiate(shotType, transform.position, Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z)));

            // fire until spread angle is reached
            var angleCounter = 0 + angleBetweenShots;
            while (angleCounter < spreadAngle)
            {
                // fire one right
                Instantiate(shotType, transform.position, Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z + angleCounter)));
                // fire one left
                Instantiate(shotType, transform.position, Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z - angleCounter)));

                angleCounter += angleBetweenShots;
            }

            // raise event
            shotFired.Invoke();
        }
    }
}
