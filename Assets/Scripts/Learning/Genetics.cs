using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Genetics : MonoBehaviour
{
    // Genetic Algorithm + Natural Selection Control
    // Includes crossover, mutation and sorting of ais
    
    void crossover()
    {
        //Will most likely use uniform random crossover not cutoff
    }
    void sortfits(float[][][][] data) {
        // Use LINQ to sort by the fitness value (Or use lambda)
        // Data in form (F,W,B)
        //ata = ;

    }
    // Possible issues with pointers here
    // Copy the individual variables in the 3D array to new 3D array and give a chance of random mutation
    public float[][] mutate(float rate,float[][] vals) {
        float[][] newVals = new float[vals.Length][];
        for (int x = 0; x < vals.Length; x++) {
            newVals[x] = new float[vals[x].Length];
            for (int z = 0; z < vals[x].Length; z++) {
                if (Random.Range(0, 1.0f) < rate)
                {
                    newVals[x][z] = Random.Range(-1.0f, 1.0f);
                }
                else {
                    newVals[x][z] = vals[x][z];
                }
            }
        }
        return newVals;
    }

}
