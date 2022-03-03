using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSounds : MonoBehaviour
{
    public AudioClip clip;
    public AudioSource source;    

    // Whenever a user hovers over button, this function is called and a sound is played
    public void OnMouseOver()
    {
        source.PlayOneShot(clip);
    }
}
