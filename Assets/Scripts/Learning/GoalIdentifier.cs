using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalIdentifier : MonoBehaviour
{
    // Script for labelling goals for fitness
    void Start()
    {
        initialiseCourse();
    }
    // Find all goal objects and iterate through them adding an identity
    void initialiseCourse() {
        int x = 0;
        
        foreach (GameObject goal in GameObject.FindGameObjectsWithTag("goal"))
        {
            goal.GetComponent<GoalScript>().Ident = x;
            x++;
        }
    }
}
