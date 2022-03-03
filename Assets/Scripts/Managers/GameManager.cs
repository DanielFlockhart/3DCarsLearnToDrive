using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    /* Main Game Manager,
     * Often is the spaghetti'est place */

    // Hyperparameters

    // The interval at which the models will be saved
    public int checkpointInterval = 200;
    //Prefabs
    public GameObject car;
    public GameObject graph;

    // Population sizes, generation lengths mutation rates and other parameters
    public int populationSize = 64;
    public int generation;

    public float training_time;
    public float timer;
    public float genTime = 0;
    private float newGenTime = 0;
    public float startTime = 15;
    public float mutRate = 0.02f;

    // Time increment per generation per new goal of progress
    public float increment = 0.5f;

    // Arrays
    public List<float[][]> weights;
    public List<float[][]> biases;
    public List<float> fitnesses;

    public static Vector3 startPos;

    // Fitness related variables
    public float bestScore = -100;
    public float averageFitness = 0;
    public int bestGoal = 0;
    
    public float[] state;
    void Awake(){

        populationSize = setup.population;
        Time.timeScale = setup.timeScale;
        checkpointInterval = setup.checkInterval;
    }

    void Start()
    {
        // Spawn first ais and assign goals their identities
        
        spawn("init");
        
    }

    // Controls generation timer
    void Update()
    {
        // Stores the current state of the game
        state = new float[]{populationSize,generation,training_time,genTime,startTime,mutRate,increment,bestScore};
        timer += Time.deltaTime;
        training_time += Time.deltaTime;
        // If the game is over then start the next generation
        if (isOver()) {
            timer = 0;
            storeData();
            clear();
            spawn("respawn");
        }
        
    }


    // Spawning
    public void spawn(string state) {
        // Get the current generation length
        genTime = startTime + (bestGoal * increment);
        generation++;
        // If the generation is a checkpoint then save models
        if(generation % checkpointInterval == 0 && generation > 0){
            FindObjectOfType<rw_script>().Save();
        }
        // If first spawn stage
        if (state == "init"){
            // Create a population of ais
            for (int x = 0; x < populationSize; x++) {
                GameObject ai = Instantiate(car);
                ai.transform.position = startPos;
                ai.GetComponent<AiController>().state = "init";
                ai.name = "car" + x;
            }
        }
        // If already spawned before respawn them
        else if (state == "respawn"){
            List<List<float[][]>> sorted = gameObject.GetComponent<Genetics>().sortfits(weights,biases,fitnesses);
            weights = sorted[0];
            biases = sorted[1];
            List<List<float[][]>> newGen = gameObject.GetComponent<Genetics>().newGeneration(weights,biases,populationSize,mutRate);
            weights = sorted[0];
            biases = sorted[1];
            place();
        // Reload models from saved model.
        } else if (state == "reload"){
            
            genTime = newGenTime;
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
    public void clear() {
        GameObject[] ais = GameObject.FindGameObjectsWithTag("Ai");
        foreach (GameObject ai in ais) {
            Destroy(ai);
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
        // Add new data to graph
        graph.GetComponent<graph_script>().addValue(averageFitness);
        graph.GetComponent<graph_script>().plot(0,generation);
    }
    // Reload weights from previously saved models
    public void reload_weights(List<float[][]> weights_load, List<float[][]> biases_load, int[] layers){
        timer = 0;
        clear();
        averageFitness = 0;
        weights = weights_load;
        biases = biases_load;

        // Set the neural network layers
        setup.hiddenNodes = layers[1];
        setup.hiddenLayers = layers.Length - 2;
        setup.population = populationSize;
        
        spawn("reload");

    }
    // Reload the state from the peference file values that were passed in
    public void setState(float[] values){
        populationSize = (int) values[0];
        generation = (int) values[1];
        training_time = values[2];
        newGenTime = values[3];
        startTime = values[4];
        mutRate = values[5];
        increment = values[6];
        bestScore = values[7];
    }
    // Place the ais in the correct positions
    public void place(){
        for (int x = 0; x < populationSize; x++) {

            GameObject ai = Instantiate(car);
            ai.GetComponent<Brain>().build();
            ai.GetComponent<Brain>().setWeights(weights[x]);
            ai.GetComponent<Brain>().setBiases(biases[x]);
            ai.transform.position = startPos;
            ai.GetComponent<AiController>().state = "new";
            ai.name = "car" + x;
        }
        
    }
}
// populationSize,generation,training_time,genTime,startTime,mutRate,increment,bestScore





