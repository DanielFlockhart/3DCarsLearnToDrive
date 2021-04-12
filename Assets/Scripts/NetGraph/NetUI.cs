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
    public float weight;





    void Start()
    {
        
        utilities = FindObjectOfType<GameManager>().GetComponent<Utils>();
        build(new int[4] {2,4,3,6});
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

    void setColour(GameObject img)
    {
        rgb = utilities.mColour(weight);
        img.GetComponent<RawImage>().color = new Color(255* rgb[0], 255 * rgb[1], 255 * rgb[2], 255);
    }
    void addNode(float xPos,float yPos) {
        GameObject newNode = Instantiate(Node);
        newNode.transform.SetParent(NetGraph.transform, false);
        newNode.transform.position = NetGraph.transform.position;


        weight = utilities.RandomFloat();
        setColour(newNode);
        newNode.transform.position += new Vector3(20*xPos,10*yPos,0);
    }
}
