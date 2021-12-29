using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trackScript : MonoBehaviour
{
    public Vector3 newstartPos;
    public Quaternion newstartRot;

    void Start()
    {
        GameManager.startPos = newstartPos;
        GameManager.startRot = newstartRot;

    }

}
