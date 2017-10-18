using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedExplosionPatternController : MonoBehaviour {

    Rigidbody2D shotrb;
    public GameObject shotType;
    public float speed;
    public float fireRate = 1.0f;
    private float nextFire = 0.0f;

    public float explosionRate = 2.5f;
    private float nextExplosion = 1.0f;

    GameObject singleShot;

    // Use this for initialization
    void Start () {
        shotrb = shotType.GetComponent<Rigidbody2D>();
        shotrb.GetComponent<ForwardMover>().speed = speed;
        //shotrb.velocity = -transform.up * speed;
    }

    private void OnEnable()
    {
        //fireRate = 1.0f;
       // nextFire = 0.0f;

        //explosionRate = 2.5f;
       // nextExplosion = 1.0f;
    }

    // Update is called once per frame
    void Update () {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            singleShot = Instantiate(shotType, transform.position, transform.rotation) as GameObject;
        }

        if (Time.time > nextExplosion)
        {
            nextExplosion = Time.time + explosionRate;

            for (int i = 0; i < 48; i++)
            {
                Instantiate(shotType, singleShot.transform.position, Quaternion.Euler(new Vector3(singleShot.transform.rotation.x, singleShot.transform.rotation.y, singleShot.transform.rotation.z + (7.5f * i))));
            }

            Destroy(singleShot);
        }
    }
}
