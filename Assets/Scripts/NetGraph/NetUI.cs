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

    public float[] rgb;

    public int[] layers;
    





    void Start()
    {
        layers = new int[2] { 1, 1 };
        utilities = FindObjectOfType<GameManager>().GetComponent<Utils>();
        build(layers);
        GameObject[] nodes = GameObject.FindGameObjectsWithTag("netNode");
        connectNet(layers,nodes);
    }


    void Update()
    {

    }
    void build(int[] layers) {
        for (int x = 0; x < layers.Length; x++) {
            for (int y = 0; y < layers[x]; y++) {
                float xWidth = x-(layers.Length/2);
                float yHeight = y-(layers[x]/2);
                addNode(xWidth, yHeight);
            }
        
        }

    }

    void setColour(GameObject img,float weight)
    {
        rgb = utilities.mColour(weight);
        img.GetComponent<RawImage>().color = new Color(255* rgb[0], 255 * rgb[1], 255 * rgb[2], 255);
    }
    void addNode(float xPos,float yPos) {
        GameObject newNode = Instantiate(Node);
        newNode.transform.SetParent(NetGraph.transform, false);
        newNode.transform.position = NetGraph.transform.position;

        float weight = utilities.RandomFloat();
        newNode.transform.position += new Vector3(20*xPos,10*yPos,0);
        setColour(newNode,weight);
    }
    void connectNet(int[] layers,GameObject[] nodes) {
        foreach (GameObject node in nodes)
        {
            connectNode(node, nodes);
        }

        
    }
    void connectNode(GameObject root, GameObject[] nodes) {
        foreach (GameObject node in nodes) {
            if (node.GetComponent<NodeScript>().layerID == root.GetComponent<NodeScript>().layerID + 1) { 
                
            }
        }
    
    }
}
