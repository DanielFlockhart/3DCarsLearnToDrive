using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FitCheck : MonoBehaviour
{
    public float fitness = -1;
    public int currentGoal = -1;
    // Update is called once per frame
    void Update()
    {
        fitness = currentGoal;
        if (fitness > FindObjectOfType<GameManager>().bestScore) {
            FindObjectOfType<GameManager>().bestScore = fitness;
            FindObjectOfType<GameManager>().Bweights = GetComponent<Brain>().weights;
            FindObjectOfType<GameManager>().Bbiases = GetComponent<Brain>().biases;
            print(fitness);
        }
    }
}
