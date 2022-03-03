using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class graph_script : MonoBehaviour
{
    // Prefab for a graph point
    public GameObject point;
    // Dimensions of the graph
    public float g_width = 100f;
    public float g_height = 100f;
    List<float> values = new List<float>();
    // Maximum value recorded in the graph
    public float peak = 0;
    
    // Plot the graph
    public void plot(int min,int max){
        clearPlot();
        //float split = g_width/values.Count;
        int maxApprox = Mathf.Min(max,values.Count);
        // Finds what the distance between each point should be
        float split = g_width/(maxApprox-min);
        
        // Finds the maximum fitness of the graph
        peak = Mathf.Max(peak,values[values.Count-1]);

        // Iterates through all the points that need to be drawn
        for(int x = min; x < maxApprox;x++){
            // Draws a point relative to the other points.
            addPoint(values[x],split,x-min,peak);
        }
    }
    // Clears the graph of all the points.
    void clearPlot(){
        GameObject[] points = GameObject.FindGameObjectsWithTag("GraphPoint");
        foreach(GameObject point in points){
            Destroy(point);
        }
    }
    // Adds a new average fitness to the fitness list over time.
    public void addValue(float averageFitness){
        values.Add(averageFitness);
    }
    // Adds a specific point.
    void addPoint(float value,float split, float pos,float peak)
    {
        // Instantiate node
        GameObject newPoint = Instantiate(point);
        // Place node and place in correct position in heirachy
        newPoint.transform.SetParent(transform.Find("Points"), false);
        newPoint.transform.position = transform.position;
        newPoint.transform.position += new Vector3(split*pos-(g_width/2), (value/peak) * g_height - (g_height/2), 0);
    }
    // Resets the graph and fitness values entirely
    public void reset_values(){
        clearPlot();
        values = new List<float>();
        peak = 0;
    }
}
