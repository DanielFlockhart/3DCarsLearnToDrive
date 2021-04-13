using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetUI : MonoBehaviour
{
    public Color polarity;
    private Utils utilities;

    public GameObject NetGraph;
    public GameObject Node;
    public GameObject lineRenderer;

    public float[] rgb;

    public int[] layers;






    void Start()
    {
        layers = new int[5] { 4, 8,8,8,16 };
        utilities = FindObjectOfType<GameManager>().GetComponent<Utils>();
        build(layers);
        GameObject[] nodes = GameObject.FindGameObjectsWithTag("netNode");
        connectNet(layers, nodes);
    }


    void Update()
    {

    }
    void build(int[] layers)
    {
        for (int x = 0; x < layers.Length; x++)
        {
            for (int y = 0; y < layers[x]; y++)
            {
                float xWidth = x - (layers.Length / 2);
                float yHeight = y - (layers[x] / 2);
                addNode(xWidth, yHeight,x,y);
            }

        }

    }

    void setColour(GameObject img, float bias)
    {
        rgb = utilities.mColour(bias);
        img.GetComponent<RawImage>().color = new Color(255 * rgb[0], 255 * rgb[1], 255 * rgb[2], 255);
    }
    void setStrandColour(GameObject line, float weight) {
        rgb = utilities.mColour(weight);
        Color col = new Color(255 * rgb[0], 255 * rgb[1], 255 * rgb[2], 255);
        line.GetComponent<LineRenderer>().material.color = col;
    }
    void addNode(float xPos, float yPos,int x,int y)
    {
        GameObject newNode = Instantiate(Node);
        newNode.transform.SetParent(NetGraph.transform.Find("Nodes"), false);
        newNode.transform.position = NetGraph.transform.position;

        float bias = utilities.RandomFloat();
        newNode.transform.position += new Vector3(20 * xPos, 10 * yPos, 0);
        newNode.GetComponent<NodeScript>().layerID = x;
        newNode.GetComponent<NodeScript>().posID = y;
        setColour(newNode, bias);
    }


    void connectNet(int[] layers, GameObject[] nodes)
    {
        foreach (GameObject node in nodes)
        {
            connectNode(node, nodes);
        }


    }
    void connectNode(GameObject root, GameObject[] nodes)
    {
        foreach (GameObject node in nodes)
        {
            if (node.GetComponent<NodeScript>().layerID == root.GetComponent<NodeScript>().layerID + 1)
            {
                float weight = utilities.RandomFloat();
                GameObject lr_obj = Instantiate(lineRenderer);
                lr_obj.transform.SetParent(NetGraph.transform.Find("Axons"), false);
                lr_obj.GetComponent<Lr_controller>().SetUpLine(new Transform[2] { root.transform, node.transform });
            }
        }

    }
}

    
