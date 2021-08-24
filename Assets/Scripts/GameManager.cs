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
    public GameObject graph;
    public int populationSize = 64;
    public int generation;

    public float training_time;
    public float timer;
    public float genTime = 0;

    public float startTime = 15;
    public float mutRate = 0.02f;

    public float increment = 0.5f;

    // Arrays
    public List<float[][]> weights;
    public List<float[][]> biases;
    public List<float> fitnesses;


    public float bestScore = -100;
    public float averageFitness = 0;
    public int bestGoal = 0;
    
    public float[] state;
    void Start()
    {
        // Spawn first ais and assign goals their identities
        
        initialiseCourse();
        spawn("init");
        
    }

    // Controls generation timer
    void Update()
    {
        //print(bestScore + "  "+ averageFitness);
        state = new float[]{populationSize,generation,training_time,genTime,startTime,mutRate,increment,bestScore};
        timer += Time.deltaTime;
        training_time += Time.deltaTime;
        if (isOver()) {
            timer = 0;
            storeData();
            clear();
            spawn("respawn");
        
        }
    }


    
    void spawn(string state) {
        genTime = startTime + (bestGoal * increment);
        generation++;
        // If first spawn stage
        if (state == "init"){
            for (int x = 0; x < populationSize; x++) {
                GameObject ai = Instantiate(car);
                ai.GetComponent<AiController>().state = "init";
                ai.name = "car" + x;
            }
        }

        else if (state == "respawn"){
            List<List<float[][]>> sorted = gameObject.GetComponent<Genetics>().sortfits(weights,biases,fitnesses);
            weights = sorted[0];
            biases = sorted[1];
            List<List<float[][]>> newGen = gameObject.GetComponent<Genetics>().newGeneration(weights,biases,populationSize,mutRate);
            weights = sorted[0];
            biases = sorted[1];
            place();
        } else if (state == "reload"){
            place();
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
            Destroy(ai);
        }
        // NOT CLEARING AFTER LOAD
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
    public void storeData() {
        GameObject[] ais = GameObject.FindGameObjectsWithTag("Ai");
        weights = new List<float[][]>();
        biases = new List<float[][]>();
        fitnesses = new List<float>();
        averageFitness = 0;

        for (int x = 0;x<ais.Length;x++){
            weights.Add(ais[x].GetComponent<Brain>().weights);
            biases.Add(ais[x].GetComponent<Brain>().biases);
            fitnesses.Add(ais[x].GetComponent<FitCheck>().fitness);
            averageFitness+=ais[x].GetComponent<FitCheck>().fitness;
        }
        averageFitness = averageFitness / populationSize;
        graph.GetComponent<graph_script>().addValue(averageFitness);
        graph.GetComponent<graph_script>().plot(0,generation);
    }
    public void reload_weights(List<float[][]> weights_load, List<float[][]> biases_load){
        timer = 0;
        clear();
        averageFitness = 0;
        weights = weights_load;
        biases = biases_load;
        spawn("reload");

    }
    public void setState(float[] values){
        populationSize = (int) values[0];
        generation = (int) values[1];
        training_time = values[2];
        genTime = values[3];
        startTime = values[4];
        mutRate = values[5];
        increment = values[6];
        bestScore = values[7];

    }
    public void place(){
        for (int x = 0; x < populationSize; x++) {
            GameObject ai = Instantiate(car);
            ai.GetComponent<AiController>().state = "new";
            ai.name = "car" + x;
            ai.GetComponent<Brain>().build();
            ai.GetComponent<Brain>().setWeights(weights[x]);
            ai.GetComponent<Brain>().setBiases(biases[x]);
        }
    }
}
// populationSize,generation,training_time,genTime,startTime,mutRate,increment,bestScore

