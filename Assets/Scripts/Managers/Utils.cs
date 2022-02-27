using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Utils : MonoBehaviour
{
    // Basic Utils script for containing reoccuring functions without using too many libraries

    //Return random float between -1 and 1 to use as weights/biases
    public float rFloat()
    {
        return UnityEngine.Random.Range(-1.0f, 1.0f);
    }

    // Used for converting polarity(weight) into a red or blue colour based on whether it is positive or negative
    // Used predominantly in the NetUI script
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


    // Returns value between 0 - 1 -> to get between -1,1 times by 2 minus 1
    public float sigmoid(float x)
    {
        return (1 / (1 + Mathf.Exp(-x)));
    }
    public float tanh(float x) {
        return (float) Math.Tanh(x);
    }
}
