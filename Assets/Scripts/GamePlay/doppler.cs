using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doppler : MonoBehaviour
{
    public GameObject player;
    public AudioSource motorsound;
    public float distance;

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
    public float calcDistance(){
        return (gameObject.transform.position -  player.transform.position).magnitude;
    }
}
