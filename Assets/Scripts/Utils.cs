using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    // Basic Utils script for containing reoccuring functions without using too many libraries

    //Return random float between -1 and 1 to use as weights/biases
    public float RandomFloat()
    {
        return Random.Range(-1.0f, 1.0f);
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
}
