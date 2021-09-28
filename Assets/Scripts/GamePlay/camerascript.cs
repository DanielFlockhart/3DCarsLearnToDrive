using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerascript : MonoBehaviour
{
    public GameObject car;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(car.transform.position.x,150,car.transform.position.z);
    }
}
