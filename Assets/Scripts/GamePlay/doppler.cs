using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doppler : MonoBehaviour
{
    // Stores the player object
    public GameObject player;

    // Stores the audio source for the car engine.
    public AudioSource motorsound;
    
    // Holds whether the car is making a sound already or not
    public bool isPlaying = false;
    public void Update(){
        // If the user is pressing accelerate, the sound is played.
        if(player.GetComponent<CarController>().verticalInput != 0 && !isPlaying){
            motorsound.Play();
            isPlaying = true;
        }
        // If the user is not pressing accelerate, the sound is stopped.
        if(player.GetComponent<CarController>().verticalInput == 0){
            isPlaying = false;
            motorsound.Stop();
        }
    }
}
