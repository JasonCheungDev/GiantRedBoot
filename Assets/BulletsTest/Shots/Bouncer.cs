using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour {
    Rigidbody2D rb;
    private int currentBouce;
    public int maxBounces = 3;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentBouce = 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            if (currentBouce <= maxBounces)
            {
                
                //rb.velocity = rb.velocity * -1.0f;

                Vector2 opposite = -rb.velocity;
                rb.AddForce(opposite);

                currentBouce++;
            }
        }
    }
}
