using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FitCheck : MonoBehaviour
{
    public float fitness;

    // Update is called once per frame
    void Update()
    {
        fitness = transform.position.z;
        if (fitness > FindObjectOfType<GameManager>().bestScore) {
            FindObjectOfType<GameManager>().bestScore = fitness;
        }
    }
}
