using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetUI : MonoBehaviour
{
    public Color polarity;
    private Utils utilities;

    public GameObject Node;

    public float[] rgb;
    public float weight;





    void Start()
    {
        
        utilities = FindObjectOfType<GameManager>().GetComponent<Utils>();
        build(new int[4] {2,4,4,8});
    }



    void Update()
    {

    }
    void build(int[] layers) {
        for (int x = 0; x < layers.Length; x++) {
            for (int y = 0; y < layers[x]; y++) {
                addNode(x,y);
            }
        
        }

    }

    void setColour(GameObject img)
    {
        rgb = utilities.mColour(weight);
        img.GetComponent<RawImage>().color = new Color(255* rgb[0], 255 * rgb[1], 255 * rgb[2], 255);
    }
    void addNode(int xPos,int yPos) {
        GameObject newNode = Instantiate(Node);
        weight = utilities.RandomFloat();
        setColour(newNode);
        newNode.transform.position = new Vector3(20*xPos,10*yPos,0);
    }
}
