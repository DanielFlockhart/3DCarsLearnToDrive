using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSounds : MonoBehaviour
{
    public AudioClip clip;
    public AudioSource source;    
    public void OnMouseOver()
    {
        print("sdfadasd");
        source.PlayOneShot(clip);
    }
}
