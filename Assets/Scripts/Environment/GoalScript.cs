using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    public int Ident;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Ai" && collision.gameObject.GetComponent<FitCheck>().currentGoal < Ident)
        {
            collision.gameObject.GetComponent<FitCheck>().fitness += 1;
            collision.gameObject.GetComponent<FitCheck>().currentGoal = Ident;
        }
    }

    public void hideGoal()
    {
        gameObject.SetActive(false);
    }
}
