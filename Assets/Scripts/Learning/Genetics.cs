using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Genetics : MonoBehaviour
{
    float elite = 3/16f;
    float mut_crossover = 10/16f;
    float mut_elite = 3/16f;

    float[] cull_weights;
    
    

    /*Spawn Function:
     Spawn some elites
     Some with Crossover
     Some with Random
     Some with Random and Crossover
     Some New*/

    // Genetic Algorithm + Natural Selection Control
    // Includes crossover, mutation and sorting of ais
    void Start(){
        cull_weights = new float[3]{elite,mut_crossover,mut_elite};
    }
    
    public List<List<float[][]>> newGeneration(List<float[][]> weights,List<float[][]> biases,int population,float mut_rate){
        int pointer = 0;
        for(int x = 0; x < cull_weights.Length;x++){
            
            for(int z = 0; z < cull_weights[x]*population;z++){
                if(x==0){
                    // NO POINT HAVING THIS LINE BUT WHATEVER
                    weights[pointer] = weights[pointer];
                    pointer +=1;
                }
                if(x == 1){
                    //weights[pointer] = crossover(List<float[][]> weights,List<float[][]> biases,int population,float mut_rate);
                    pointer +=1;
                }
                if(x == 2){
                    pointer +=1;
                }
            }
        }
        return new List<List<float[][]>>{weights, biases};
    }
    public void crossover()
    {
        //Will most likely use uniform random crossover not cutoff
        print(NextGaussian());
    }
    public List<List<float[][]>> sortfits(List<float[][]> weights,List<float[][]> biases,List<float> fitness) {
        // Use LINQ to sort by the fitness value (Or use lambda)
        // Data in form (F,W,B)
        //ata = ;

        // MIGHT BE AN ISSUE HERE WITH PASSING BY REFERENCE
        int swaps = 1;
        while (swaps != 0) // Bubble sort
        {
            swaps = 0;
            for (int i = 0; i < fitness.Count - 1; i++)
            {
                if (fitness[i + 1] > fitness[i])
                {
                    float[][] storedWeights = weights[i];
                    float[][] storedBiases = biases[i];
                    float storedFit = fitness[i];
                    fitness[i] = fitness[i + 1];
                    weights[i] = weights[i + 1];
                    biases[i] = biases[i + 1];
                    fitness[i + 1] = storedFit;
                    weights[i + 1] = storedWeights;
                    biases[i + 1] = storedBiases;
                    swaps++;
                }
            }
        }
        return new List<List<float[][]>>{weights, biases};

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
    public float normalSample(float mean,float sd){

        
    }

    

}
