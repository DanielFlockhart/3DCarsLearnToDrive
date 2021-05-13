using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : MonoBehaviour
{
    // Script to control obstacle collisions I.E walls
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Ai") {
            // Currently fully destroys but this does not make sense in the real world.
            Destroy(collision.gameObject);
            FindObjectOfType<GameManager>().GetComponent<GameManager>().storeData(collision.gameObject);
        }
    }
}
