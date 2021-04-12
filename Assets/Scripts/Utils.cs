using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public float RandomFloat()
    {

        return Random.Range(-1.0f, 1.0f);
    }
    public float[] mColour(float polarity)
    {
        if (polarity > 0)
        {
            return new float[] { 0, 0, polarity };
        }
        else
        {
            return new float[] { -polarity, 0, 0 };
        }
    }
}
