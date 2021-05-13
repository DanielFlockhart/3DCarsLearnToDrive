using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGoals : MonoBehaviour
{
    // After one full loop of course has been completed, current goal of ai is reset to -1
    private void OnTriggerEnter(Collider collision)
    {
        // If object collided is the ai and it hasnt just gone back on itself from the start
        if (collision.gameObject.tag == "car" && collision.gameObject.GetComponent<FitCheck>().currentGoal > 10)
        {
            Debug.Log("Car reached end - resetting goals");
            collision.gameObject.GetComponent<FitCheck>().currentGoal = -1;
        }
    }

}
