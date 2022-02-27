using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FitCheck : MonoBehaviour
{
    // Script to control and evaluate current fitness of ai
    public float fitness = -1;
    public int currentGoal = -1;
    void Update()
    {
        // If this ais fitness is better then the current set change it
        if(gameObject.tag == "Ai"){
            // If ai has fitness record set it in game manager
            if (fitness > FindObjectOfType<GameManager>().bestScore) {
                FindObjectOfType<GameManager>().bestScore = fitness;
            }
            // If ai has goal record set it in game manager
            if (currentGoal > FindObjectOfType<GameManager>().bestGoal) {
                FindObjectOfType<GameManager>().bestGoal = currentGoal;
            }
            // If ai has reached the end of the course reset the current goals to the start
            if (currentGoal+1 >= GameObject.FindGameObjectsWithTag("goal").Length){
                currentGoal = -1;
            }
            
        }
        // Temporary
        // If ai reaches the end, load results
         else {
            if (currentGoal+1 >= GameObject.FindGameObjectsWithTag("goal").Length){
                //FindObjectOfType<GameButtons>().results.SetActive(true);
            }
        }
        
        
    }
}
