using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trackScript : MonoBehaviour
{
    public Vector3 startPos;
    public Quaternion startRot;

    void Start()
    {
        GameObject[] goals = GameObject.FindGameObjectsWithTag("goal");
        GameObject startgoal,secondgoal = new GameObject();
        foreach(GameObject goal in goals){
            if(goal.GetComponent<GoalScript>().Ident == 0){
                startgoal = goal;
                startPos = startgoal.transform.position + new Vector3(0,0.5f,0);
            }
            if(goal.GetComponent<GoalScript>().Ident == 1){
                secondgoal = goal;
            }
        } // Rotating Y aswell
        
        startRot = new Quaternion(0,secondgoal.transform.rotation.y,0,1);

    }

}
