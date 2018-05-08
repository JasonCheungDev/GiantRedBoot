using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollider : MonoBehaviour {

    private ParticleSystem PSystem;
    private ParticleCollisionEvent[] CollisionEvents;

    void Awake()
    {
        PSystem = GetComponent<ParticleSystem>();
    }

    public void OnParticleCollision(GameObject other)
    {
        int collCount = PSystem.GetSafeCollisionEventSize();

        if (collCount > CollisionEvents.Length)
            CollisionEvents = new ParticleCollisionEvent[collCount];

        int eventCount = PSystem.GetCollisionEvents(other, CollisionEvents);

        for (int i = 0; i < eventCount; i++)
        {
            //TODO: Do your collision stuff here. 
            Destroy(other);
            // You can access the CollisionEvent[i] to obtaion point of intersection, normals that kind of thing
            // You can simply use "other" GameObject to access it's rigidbody to apply force, or check if it implements a class that takes damage or whatever
        }
    }

}
