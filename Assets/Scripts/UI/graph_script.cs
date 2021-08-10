using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class graph_script : MonoBehaviour
{
    public GameObject point;
    public float g_width = 100f;
    public float g_height = 100f;
    List<float> values = new List<float>();

    public float peak = 0;
    // Start is called before the first frame update
    public void plot(){
        clearPlot();
        float split = g_width/values.Count;
        peak = Mathf.Max(peak,values[values.Count-1]);
        for(int x = 0; x < values.Count;x++){
            addPoint(values[x],split,x,peak);
        }
    }
    void clearPlot(){
        GameObject[] points = GameObject.FindGameObjectsWithTag("GraphPoint");
        foreach(GameObject point in points){
            Destroy(point);
        }
    }
    public void addValue(float averageFitness){
        values.Add(averageFitness);
    }
    void addPoint(float value,float split, float pos,float peak)
    {
        // Instantiate node
        GameObject newPoint = Instantiate(point);
        // Place node and place in correct position in heirachy
        newPoint.transform.SetParent(transform.Find("Points"), false);
        //newPoint.transform.position = transform.position;
        newPoint.transform.position += new Vector3(split*pos-500, (value/peak) * g_height - 250, 0);
    }
}
