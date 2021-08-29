using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalIdentifier : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        initialiseCourse();
    }
    void initialiseCourse() {
        int x = 0;
        
        foreach (GameObject goal in GameObject.FindGameObjectsWithTag("goal"))
        {
            goal.GetComponent<GoalScript>().Ident = x;
            x++;
        }
    }
}
