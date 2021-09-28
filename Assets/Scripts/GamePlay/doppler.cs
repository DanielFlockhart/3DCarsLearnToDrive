using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doppler : MonoBehaviour
{
    public GameObject camera;

    public float distance;

    public float calcDistance(){
        return (gameobject.transform.position - camera.transform.position).magnitude;
    }
}
