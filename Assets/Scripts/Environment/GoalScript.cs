using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    // Class script for each goal, stores the goal identity
    // Fitness will eventaully be dictated by time take to get to goal and overall fitness
    public int Ident;

    private void OnTriggerEnter(Collider collision)
    {
        // If object collided is ai and its not a previous goal
        if (collision.gameObject.tag == "Ai" && collision.gameObject.GetComponent<FitCheck>().currentGoal == Ident-1)
        {
            //Increase ai fitness by one
            collision.gameObject.GetComponent<FitCheck>().fitness += 1;
            // Set the current goal of the ai to this one so it cant get it again
            collision.gameObject.GetComponent<FitCheck>().currentGoal = Ident;
        }
    }
}
