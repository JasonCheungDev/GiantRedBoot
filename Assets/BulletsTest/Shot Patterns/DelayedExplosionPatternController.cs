using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedExplosionPatternController : MonoBehaviour {

    Rigidbody2D shotrb;
    public GameObject shotType;
    public float speed;
    public float fireDelay = 1.0f;
    [Tooltip("Should be a number 360 is divisble by.")]
    public float angleBetweenShots = 15f;
    private float nextFire = 0.0f;

    public float explosionDelay = 0.5f;
    private float nextExplosion = 1.0f;

    private float counter = 0f;

    public GameObject singleShot;

    // Use this for initialization
    void Start () {
        shotrb = shotType.GetComponent<Rigidbody2D>();
        shotrb.GetComponent<ForwardMover>().speed = speed;
        //shotrb.velocity = -transform.up * speed;
    }

    void OnEnable()
    {
        nextFire = Time.time;
    }

    // Update is called once per frame
    void Update () {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireDelay;            // fire again after fireDelay 
            nextExplosion = Time.time + explosionDelay;  // explode bullet after explosionDelay
            singleShot = Instantiate(shotType, transform.position, transform.rotation) as GameObject;
            singleShot.GetComponent<ForwardMover>().speed = speed;
        }

        if (Time.time > nextExplosion)
        {
            nextExplosion = float.PositiveInfinity;     // ensure bullet doesn't explode multiples

            // Create a circular explosion of bullets
            float angleCounter = 0f;
            while (angleCounter <= 360f)
            {
                Instantiate(shotType, singleShot.transform.position, Quaternion.Euler(new Vector3(singleShot.transform.rotation.x, singleShot.transform.rotation.y, singleShot.transform.rotation.z + angleCounter)));
                angleCounter += angleBetweenShots;
            }

            if (singleShot)
                Destroy(singleShot);
        }
    }
}
