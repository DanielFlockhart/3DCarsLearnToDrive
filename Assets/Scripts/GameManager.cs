﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /* Main Game Manager,
     * Often is the spaghetti'est place */

    //Prefabs
    public GameObject car;

    // Coefficients
    public int populationSize = 80;
    public float genTime = 10;
    public float mutRate = 0.05f;

    // Arrays
    public float[][][][] data;


    public float timer;
    public float bestScore = -100;

    void Start()
    {
        // Spawn first ais and assign goals their identities
        data = new float[populationSize][][][];
        initialiseCourse();
        spawn("init");
    }

    // Controls generation timer
    void Update()
    {
        timer += Time.deltaTime;
        if (isOver()) {
            timer = 0;
            clear();
            spawn("respawn");
        
        }
    }


    /*Spawn Function:
     Spawn some elites
     Some with Crossover
     Some with Random
     Some with Random and Crossover*/
    void spawn(string state) {
        for (int x = 0; x < populationSize; x++) {
            // If first spawn stage
            if (state == "init")
            {
                GameObject ai = Instantiate(car);
                ai.GetComponent<AiController>().state = "init";
                ai.name = "car" + x;
            }
            // If spawning ais with related weights from previous generation
            else if (state == "respawn") {
                GameObject ai = Instantiate(car);
                ai.GetComponent<AiController>().state = "new";
                ai.name = "car" + x;

                if (x < populationSize/2)
                {
                    ai.GetComponent<Brain>().weights = data[0][x];
                    ai.GetComponent<Brain>().biases = data[1][x];
                }
                else { 
                    ai.GetComponent<Brain>().weights = ai.GetComponent<Genetics>().mutate(mutRate, data[0][x-populationSize/2]);
                    ai.GetComponent<Brain>().biases = ai.GetComponent<Genetics>().mutate(mutRate, data[1][x - populationSize / 2]);
                }
                
            }
        }
    }

    // Checking if generation is over, Add manual gen skipping
    bool isOver() {
        if (GameObject.FindGameObjectsWithTag("Ai").Length == 0 || timer > genTime)
        {
            return true;
        }
        else {
            return false;
        }
    }

    // Kill all ais currently still alive
    void clear() {
        GameObject[] ais = GameObject.FindGameObjectsWithTag("Ai");
        foreach (GameObject ai in ais) {
            storeData(ai);
            Destroy(ai);
        }
    }


    // Give Goals their identities
    void initialiseCourse() {
        int x = 0;
        foreach (GameObject goal in GameObject.FindGameObjectsWithTag("goal"))
        {
            goal.GetComponent<GoalScript>().Ident = x;
            x++;
        }
    }


    // Stores ai data before they get destroyed so it can be save for sorting and culling
    public void storeData(GameObject ai) {
        float[][] toStoreWeights = ai.GetComponent<Brain>().weights;
        float[][] toStoreBiases = ai.GetComponent<Brain>().biases;
        float fitness = ai.GetComponent<FitCheck>().fitness;
        for (int x = 0; x < populationSize; x++) {

            // TEMP FIX - Very inefficient
            if (data[x] == null) {
                data[x] = new float[3][][];
                data[x][0] = new float[1][];
                data[x][0][0] = new float[1]{ fitness};

                data[x] = new float[3][][];
                data[x][1] = new float[toStoreWeights.Length][];
                for (int z = 0; z < toStoreWeights.Length; z++) {
                    data[x][1][z] = new float[toStoreWeights[z].Length];
                    for (int w = 0; w < toStoreWeights[z].Length; w++) {
                        data[x][1][z][w] = toStoreWeights[z][w];
                    }
                }


                data[x] = new float[3][][];
                data[x][2] = new float[toStoreBiases.Length][];
                for (int z = 0; z < toStoreBiases.Length; z++)
                {
                    data[x][2][z] = new float[toStoreBiases[z].Length];
                    for (int w = 0; w < toStoreBiases[z].Length; w++)
                    {
                        data[x][2][z][w] = toStoreBiases[z][w];
                    }
                }
                break;
            }
        }
    }
}

