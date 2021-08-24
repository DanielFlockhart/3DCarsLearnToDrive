using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameButtons : MonoBehaviour
{
    public int fp = -1;
    public Text viewText;

    
    public GameObject FPCamera;
    public GameObject TPCamera;
    public void Update(){
        if(fp == 1){
            FPCamera.SetActive(true);
            TPCamera.SetActive(false);
            viewText.text = "Enter 3rd Person";
        } else {
            FPCamera.SetActive(false);
            TPCamera.SetActive(true);
            viewText.text = "Enter 1st Person";
        }
        
    }
    public void changeView(){
        fp *= -1;
    }
}
