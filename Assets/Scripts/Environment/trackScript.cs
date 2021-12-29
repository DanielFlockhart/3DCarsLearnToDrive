using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trackScript : MonoBehaviour
{
    public Vector3 newstartPos;

    void Awake()
    {
        GameManager.startPos = newstartPos;

    }

}
