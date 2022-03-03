using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closeScript : MonoBehaviour
{
    public GameObject parentObj;

    // Function is called to close the parent object
    public void close(){
        parentObj.SetActive(false);
    }
}
