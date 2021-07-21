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
        //fitness = currentGoal;
        // If this ais fitness is better then the current set change it
        if (fitness > FindObjectOfType<GameManager>().bestScore) {
            FindObjectOfType<GameManager>().bestScore = fitness;
        }
        if (currentGoal > FindObjectOfType<GameManager>().bestGoal) {
            FindObjectOfType<GameManager>().bestGoal = currentGoal;
        }
        
    }
}
