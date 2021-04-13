using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lr_controller : MonoBehaviour
{
    private LineRenderer lr;
    private Transform[] points;
    // Start is called before the first frame update
    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        
    }
    public void SetUpLine(Transform[] points) {
        
        lr.positionCount = points.Length;
        this.points = points;
        build();

    }
    // Update is called once per frame
    void build()
    {
        for (int i = 0; i < points.Length; i++) {
            lr.SetPosition(i, points[i].position);
        }
    }
}
