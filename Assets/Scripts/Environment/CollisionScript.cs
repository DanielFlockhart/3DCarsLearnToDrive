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

            //FindObjectOfType<GameManager>().GetComponent<GameManager>().storeData(collision.transform.gameObject);
            //Destroy(collision.transform.gameObject);

            // Make it stop moving instead of destroying
            collision.gameObject.GetComponent<AiController>().isAlive = false;
        }
    }
}
