using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnArm : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Dragon Arm"))
        {
            Destroy(gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
        }
    }
}
