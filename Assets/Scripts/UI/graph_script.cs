using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class graph_script : MonoBehaviour
{
    public GameObject point;
    [SerializeField] private Transform[] points;
    [SerializeField] private LineController line;
    // Start is called before the first frame update
    void Start()
    {
        
        float initY = 0;
        float prevX = 0;
        float prevY = 0;
        for(int i = 0; i < 25;i++){
            initY += Random.Range(-1.0f,1.0f) * 10;
            float[] res = addPoint(i*30,initY);
            prevX = res[0];
            prevY = res[1];
        }   
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GraphPoint");
        Transform[] t_objs = new Transform[objs.Length];
    
        for ( int i = 0; i < t_objs.Length; ++i ){
            t_objs[i] = objs[i].transform;
        }
        line.SetUpLine(t_objs);
    }
    float[] addPoint(float xPos, float yPos)
    {
        // Instantiate node
        GameObject newPoint = Instantiate(point);

        // Place node and place in correct position in heirachy
        newPoint.transform.SetParent(transform.Find("Points"), false);
        newPoint.transform.position = transform.position;
        newPoint.transform.position += new Vector3(xPos, yPos, 0);
        return new float[2]{newPoint.transform.position.x,newPoint.transform.position.y};
    }
}
