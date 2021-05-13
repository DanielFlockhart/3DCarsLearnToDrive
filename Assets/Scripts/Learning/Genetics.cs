using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Genetics : MonoBehaviour
{
    void crossover()
    {

    }
    void sortfits() { 
    
    }
    // Possible issues with pointers here
    public float[][] mutate(float rate,float[][] vals) {
        for (int x = 0; x < vals.Length; x++) {
            for (int z = 0; z < vals[x].Length; z++) {
                if (Random.Range(0, 1.0f) < rate)
                {
                    vals[x][z] = Random.Range(-1.0f, 1.0f);
                }
                else {
                    vals[x][z] = vals[x][z];
                }
            }
        }
        return vals;
    }

}
