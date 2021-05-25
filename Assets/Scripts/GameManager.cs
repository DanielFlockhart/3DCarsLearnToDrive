using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
    public float[][][] weights;
    public float[][][] biases;
    public float[] fitnesses;


    public float timer;
    public float bestScore = -100;

    void Start()
    {
        // Spawn first ais and assign goals their identities
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

                if (x < 10000000)
                {
                    ai.GetComponent<Brain>().weights = weights[x];
                    ai.GetComponent<Brain>().biases = biases[x];
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
        storeData(ais);
        foreach (GameObject ai in ais) {
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
    public void storeData(GameObject[] ais) {
        weights = new float[ais.Length][][];
        biases = new float[ais.Length][][];
        fitnesses = new float[ais.Length];
        for (int x = 0;x<ais.Length;x++){
            fitnesses[x] = ais[x].GetComponent<FitCheck>().fitness;
            Array.Copy(ais[x].GetComponent<Brain>().weights, weights[x],weights[x].Length);
            Array.Copy(ais[x].GetComponent<Brain>().biases, biases[x], biases[x].Length);
        }
        
    }
}

