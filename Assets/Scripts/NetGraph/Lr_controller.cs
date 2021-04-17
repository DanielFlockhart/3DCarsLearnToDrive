using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lr_controller : MonoBehaviour
{

    // Class script of linerender
    private LineRenderer lr;
    private Transform[] points;
    
    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        
    }

    // Initialise points to connect to
    public void SetUpLine(Transform[] points) {
        
        lr.positionCount = points.Length;
        this.points = points;
        build();

    }

    // Connect between two points
    void build()
    {
        for (int i = 0; i < points.Length; i++) {
            lr.SetPosition(i, points[i].position);
        }
    }
}
