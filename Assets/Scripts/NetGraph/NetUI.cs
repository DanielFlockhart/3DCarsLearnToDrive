using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetUI : MonoBehaviour
{

    // Main Class for Building neural network representation of best performing ais

    // Variable declaring
    public Color polarity;
    private Utils utilities;

    public GameObject NetGraph;
    public GameObject Node;
    public GameObject lineRenderer;

    public float[] rgb;

    public int[] layers;

    public int xScale = 40;
    public int yScale = 20;


    void Start()
    {
        // Will eventually be dictated by gamemanager script most likely
        layers = new int[5] { 4, 8,8,8,16 };

        // Assign Utils Script
        utilities = FindObjectOfType<GameManager>().GetComponent<Utils>();

        // Build Nodes in Net
        build(layers);

        //Conneect Nodes in network
        GameObject[] nodes = GameObject.FindGameObjectsWithTag("netNode");
        connectNet(layers, nodes);
    }

    // Build network
    void build(int[] layers)
    {
        // Iterates through layers in network
        for (int x = 0; x < layers.Length; x++)
        {
            // Iterates through nodes in layer
            for (int y = 0; y < layers[x]; y++)
            {
                // Gets x and y pos of node placement 
                // Should make this non hard coded
                float xWidth = x - (layers.Length / 2);
                float yHeight = y - (layers[x] / 2);

                // Add the node
                addNode(xWidth, yHeight,x,y);
            }
        }
    }

    // Convert Weight into rgb colour using utils
    Color convertColor(float weight) {
        rgb = utilities.mColour(weight);
        return new Color(255 * rgb[0], 255 * rgb[1], 255 * rgb[2], 255);
    }

    // Simple setter for node colour
    void setColour(GameObject img, float bias)
    {
        img.GetComponent<RawImage>().color = convertColor(bias);
    }

    // Simple setter for axon colour
    void setStrandColour(GameObject line, float weight) {
        Color col = convertColor(weight);
        line.GetComponent<LineRenderer>().endColor = col;
        line.GetComponent<LineRenderer>().startColor = col;

    }

    // Add node to netUI
    void addNode(float xPos, float yPos,int x,int y)
    {
        // Instantiate node
        GameObject newNode = Instantiate(Node);
        float bias = utilities.RandomFloat();


        // Place node and place in correct position in heirachy
        newNode.transform.SetParent(NetGraph.transform.Find("Nodes"), false);
        newNode.transform.position = NetGraph.transform.position;
        newNode.transform.position += new Vector3(xScale * xPos, yScale * yPos, 0);

        // Assign node object positional inforamtion
        newNode.GetComponent<NodeScript>().layerID = x;
        newNode.GetComponent<NodeScript>().posID = y;

        //Set colour of new node
        setColour(newNode, bias);
    }

    // Connect every node in netUI with axon to next layer nodes
    void connectNet(int[] layers, GameObject[] nodes)
    {
        foreach (GameObject node in nodes)
        {
            connectNode(node, nodes);
        }


    }
    // Connect individual node to all adjacent ones
    void connectNode(GameObject root, GameObject[] nodes)
    {
        foreach (GameObject node in nodes)
        {
            // If checked node is in the next layer connect to it
            if (node.GetComponent<NodeScript>().layerID == root.GetComponent<NodeScript>().layerID + 1)
            {
                // Instantiate LineRenderer
                GameObject lr_obj = Instantiate(lineRenderer);
                float weight = utilities.RandomFloat();

                // Assign axon object positional inforamtion
                lr_obj.transform.SetParent(NetGraph.transform.Find("Axons"), false);
                lr_obj.GetComponent<Lr_controller>().SetUpLine(new Transform[2] { root.transform, node.transform });

                // Set colour of new axon
                setStrandColour(lr_obj, weight);
            }
        }

    }
}

    
