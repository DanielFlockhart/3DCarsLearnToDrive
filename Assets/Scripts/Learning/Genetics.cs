using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Genetics : MonoBehaviour
{
    // Distribution of generational statistics
    float elite = 7/16f;
    float elite_mutation = 0/16f;
    float mut_crossover = 9/16f;
    float mut_mutation = 0/16f;

    float[] cull_weights;

    /*Spawn Function:
     Spawn some elites
     Some with Mutation and Crossover */

    // Genetic Algorithm + Natural Selection Control
    // Includes crossover, mutation and sorting of ais
    void Start(){
        cull_weights = new float[2]{elite,mut_crossover};
    }
    
    public List<List<float[][]>> newGeneration(List<float[][]> weights,List<float[][]> biases,int population,float mut_rate){
        int pointer = 0;
        //List<float[][]> new_weights = new List<float[][]>(population);

        //List<float[][]> new_biases = new List<float[][]>(population);

        // Iterate through culled weights
        // List<>'s in unity pass by reference and I hate it
        for(int x = 0; x < cull_weights.Length;x++){
            
            for(int z = 0; z < cull_weights[x]*population;z++){
                if(x==0){
                    // Temporary
                    // ELITE
                    //weights[pointer] = weights[pointer];
                    //biases[pointer] = biases[pointer];
                    pointer +=1;
                }

                if(x == 100){
                    weights[pointer] = mutate(mut_rate,weights[sampleParent(population)]);
                    biases[pointer] = mutate(mut_rate, biases[sampleParent(population)]);;
                    pointer +=1;
                }

                if(x == 1){
                    //CROSSOVER + Mutation
                    // PASSING BY REFERENCE THATS WHY ITS BROKEN
                    weights[pointer] = crossover(population,weights,mut_rate);
                    biases[pointer] = crossover(population,biases,mut_rate);
                    pointer +=1;
                }
                
                if(x == 30000){
                    weights[pointer] = mutate(mut_rate,weights[0]);
                    biases[pointer] = mutate(mut_rate,biases[0]);;
                    pointer +=1;
                }
            }
        }
        return new List<List<float[][]>>{weights, biases};
    }

    public int sampleParent(int population){
        return  Mathf.Min(population - (int) Mathf.Round(getParent(population)),population-1);
    }

    
    public float[][] crossover(int populationSize,List<float[][]> weights,float mut_rate)
    {
        // ONLY SELECTING THE FIRST AND LAST PARENT
        // BETTER AIS ARE CLOSER TO 0
        int parent1 = sampleParent(populationSize);
        int parent2 = sampleParent(populationSize);
        float[][] p1_weights = new float[weights[parent1].Length][];
        float[][] p2_weights = new float[weights[parent2].Length][];
        Array.Copy(weights[parent1],p1_weights,weights[parent1].Length);
        Array.Copy(weights[parent2],p2_weights,weights[parent2].Length);
        
        float[][] child_weights = new float[p1_weights.Length][];
        for(int x = 0; x < p1_weights.Length;x++){
            for(int i = 0; i < p1_weights[x].Length;i++){
                child_weights[x] = new float[p1_weights[x].Length];
            }
        }
        /* iterate through weights and randomly choose which genes from which parents to keep at 50% split
        For a while i had it as only choosing from one parent and it was working fine. Then I fixed this and It broke performance, causing non determinism
        Debugging will persist until end of project.
        */
        for (int layer = 0; layer < weights[0].Length; layer++) {
            for (int weight = 0; weight < weights[0][layer].Length; weight++){
                child_weights[layer][weight] = UnityEngine.Random.Range(0.0f,1.0f) > 0.5 ? p1_weights[layer][weight]: p1_weights[layer][weight];
            } // HMMM
        }
        // Mutate resulting weights
        child_weights = mutate(mut_rate,child_weights);
        return child_weights;
    }

    /* Sort weights
    In code refactoring possibly use different sorting algorithm
    Have sorting algorithm in utils so I can just pass into there

    Bubble sort is a good solution as the length of inputs are fixed/constant and are small in volume
    */

    // Temporary Bubble sort
    public List<List<float[][]>> sortfits(List<float[][]> weights,List<float[][]> biases,List<float> fitness) {

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
                if (UnityEngine.Random.Range(0, 1.0f) < rate)
                {
                    newVals[x][z] = UnityEngine.Random.Range(-1.0f, 1.0f);
                }
                else {
                    newVals[x][z] = vals[x][z];
                }
            }
        }
        return newVals;
    }
    public float samplePopulation(float mean = 0.9f,float sd=0.2f){
        float x1 = 1 - UnityEngine.Random.Range(0.0f,1.0f);
        float x2 = 1 - UnityEngine.Random.Range(0.0f,1.0f);
        float y1 = Mathf.Sqrt((float)-2.0 * Mathf.Log(x1)) * Mathf.Cos((float)2.0 * Mathf.PI * x2);
        float value =  y1 * sd + mean;
        return  value < 0 ? 0 : Mathf.Min(1,value);
        
    }
    public float getParent(int populationSize){
        // FIXED BUG HERE WAS CONVERTING TO INT
        return (float) samplePopulation() * populationSize;
    }
}
