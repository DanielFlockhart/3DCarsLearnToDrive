using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetUI : MonoBehaviour
{
    public Color polarity;
    private Utils utilities;

    private GameObject forwardNode;

    public float[] rgb;
    public float weight;





    void Start()
    {
        utilities = FindObjectOfType<GameManager>().GetComponent<Utils>();
        weight = utilities.RandomFloat();
        setColour(gameObject);
    }



    void Update()
    {

    }

    void setColour(GameObject img)
    {
        
        rgb = utilities.mColour(weight);
        img.GetComponent<RawImage>().color = new Color(255* rgb[0], 255 * rgb[1], 255 * rgb[2], 255);//rgb[0] * 255, rgb[1] * 255, rgb[2] * 255, 100
    }
}
