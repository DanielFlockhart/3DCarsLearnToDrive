using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameUIControls : MonoBehaviour
{   
    // Stores the text objects
    public Text position;
    public Text results;

    // Update is called once per frame
    void Update()
    {
        setPosition(position);
    }

    // Sets the position of the player
    void setPosition(Text res){
        int place =  FindObjectOfType<PlaceScript>().GetComponent<PlaceScript>().playerPos;
        string post = "th";
        if (place == 2) post = "nd";
        else if(place == 3) post = "rd";
        else if(place == 1) post = "st";
        res.text = place + post;
    }
}
