using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameButtons : MonoBehaviour
{
    // Holds an indicator of the current perspective of the user.
    public int fp = -1;
    // The label for the user that shows what perspective they are in.
    public Text viewText;
    
    // Holds the first person camera.
    public GameObject FPCamera;

    // Holds the third person camera.
    public GameObject TPCamera;
    public void Update(){
        // If the perspective is in first person, the camera is set to the first person camera.
        if(fp == 1){
            FPCamera.SetActive(true);
            TPCamera.SetActive(false);
            viewText.text = "Enter 3rd Person";
        // If the perspective is in first person, the camera is set to the first person camera.
        } else {
            FPCamera.SetActive(false);
            TPCamera.SetActive(true);
            viewText.text = "Enter 1st Person";
        }
        
    }
    // Changes the perspective of the user.
    public void changeView(){
        fp *= -1;
    }
    // Loads the main menu scene.
    public void Menu(){
        SceneManager.LoadScene("Menu");
    }
    
}
