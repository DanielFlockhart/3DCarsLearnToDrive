using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    // Class script for each goal, stores the goal identity
    // Fitness will eventaully be dictated by time take to get to goal and overall fitness
    public int Ident;
    float points;
    private void OnTriggerEnter(Collider collision)
    {
        // If gameobject is interacting with goals for training or gameplay it decides the scoring method
        if (collision.gameObject.tag == "Ai"){
            points = 20 + Ident/(FindObjectOfType<GameManager>().timer*10);
        }
        else if (collision.gameObject.tag == "Opponent" ||  collision.gameObject.tag == "Player"){
            points = 1;
        } else {
            points = -1;
        }
        // If object collided is ai and its not a previous goal
        if (points > 0 && collision.gameObject.GetComponent<FitCheck>().currentGoal == Ident-1)
        {
            //Increase ai fitness by one
            collision.gameObject.GetComponent<FitCheck>().fitness += points;
            // Set the current goal of the ai to this one so it cant get it again
            collision.gameObject.GetComponent<FitCheck>().currentGoal = Ident;
            if(Ident == GameObject.FindObjectsOfType<GoalScript>().Length){
                collision.gameObject.GetComponent<FitCheck>().currentGoal  = 0;
            }
        }
    }
}
