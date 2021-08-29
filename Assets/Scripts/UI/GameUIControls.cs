using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameUIControls : MonoBehaviour
{
    public Text position;
    public Text results;

    // Update is called once per frame
    void Update()
    {
        setPosition(position);
        setPosition(results);
    }
    void setPosition(Text res){
        res.text = "Place : " + FindObjectOfType<PlaceScript>().GetComponent<PlaceScript>().playerPos;
    }
}
