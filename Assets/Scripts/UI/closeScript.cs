using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closeScript : MonoBehaviour
{
    public GameObject parentObj;
    public void close(){
        parentObj.SetActive(false);
    }
}
