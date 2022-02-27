using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerascript : MonoBehaviour
{
    // The Script that controls the minimap camera
    // The player object.
    public GameObject car;
    void Update()
    {
        // Centralise the minimap about where the play is situated in the environment
        transform.position = new Vector3(car.transform.position.x,150,car.transform.position.z);
    }
}
