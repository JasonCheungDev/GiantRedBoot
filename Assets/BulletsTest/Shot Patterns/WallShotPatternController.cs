using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// spawns a wall of projectiles in the direction this object is facing.
public class WallShotPatternController : MonoBehaviour {

    public float speed = 1f;
    public float shootDelay = 0.5f;
    public float shotsPerWall = 2; 
    public GameObject shotPrefab;
    public UnityEvent shotFired; 
    private float counter = 0.0f;

	
	void Update ()
    {
        counter -= Time.deltaTime;
        if (counter <= 0)
        {
            counter += shootDelay;
            Fire();
        }
	}

    void Fire()
    {
        // just a normal shot
        if (shotsPerWall == 1)
        {
            Instantiate(shotPrefab, transform.position, transform.rotation);
        }
        else // an actual wall shot 
        {
            var leftExtent = transform.position - transform.right * transform.localScale.x / 2;
            var rightExtent = transform.position + transform.right * transform.localScale.x / 2;
            var step = transform.right * (transform.localScale.x / (shotsPerWall - 1));

            for (int i = 0; i < shotsPerWall; i++)
            {
                var shot = Instantiate(shotPrefab, leftExtent + step * i, transform.rotation);
                shot.GetComponent<ForwardMover>().speed = speed;
            }
        }

        shotFired.Invoke();
    }
}
