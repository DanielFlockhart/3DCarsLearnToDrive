using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Ai") {
            Destroy(collision.gameObject);
            FindObjectOfType<GameManager>().GetComponent<GameManager>().storeData(collision.gameObject);
            //print(collision.gameObject.transform.position.z);
        }
    }
}
