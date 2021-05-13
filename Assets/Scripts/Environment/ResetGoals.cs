using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGoals : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "car" && collision.gameObject.GetComponent<FitCheck>().currentGoal > 10)
        {
            Debug.Log("Car reached end - resetting goals");
            collision.gameObject.GetComponent<FitCheck>().currentGoal = -1;
        }
    }

    public void resetGoals()
    {
        foreach (GameObject goal in GameObject.FindGameObjectsWithTag("Goals"))
        {
            goal.SetActive(true);
        }
    }
}
