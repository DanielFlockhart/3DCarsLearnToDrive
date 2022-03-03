using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : MonoBehaviour
{
    // Script to control obstacle collisions I.E walls

    // On a collision this function is called
    private void OnCollisionEnter(Collision collision)
    {
        // If the object colliding with is the ai car
        if (collision.collider.tag == "Ai") {
            // Make it stop moving instead of destroying
            collision.gameObject.GetComponent<AiController>().isAlive = false;
        }
    }
}
