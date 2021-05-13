using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject car;
    public int populationSize = 80;
    public float genTime = 10;
    public float timer;
    public float bestScore = -100;
    public float mutRate = 0.05f;

    public float[][][][] data;





    // Start is called before the first frame update
    void Start()
    {
        data = new float[populationSize][][][];
        initialiseCourse();
        spawn("init");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (isOver()) {
            timer = 0;
            clear();
            spawn("respawn");
        
        }
    }

    void spawn(string state) {
        for (int x = 0; x < populationSize; x++) {
            if (state == "init")
            {
                GameObject ai = Instantiate(car);
                ai.GetComponent<AiController>().state = "init"; 
            }
            else if (state == "respawn") {
                GameObject ai = Instantiate(car);
                ai.GetComponent<AiController>().state = "new";
                if (x > populationSize/2)
                {
                    ai.GetComponent<Brain>().weights = Bweights;
                    ai.GetComponent<Brain>().biases = Bbiases;
                }
                else { 
                    ai.GetComponent<Brain>().weights = ai.GetComponent<Genetics>().mutate(mutRate, Bweights);
                    ai.GetComponent<Brain>().biases = ai.GetComponent<Genetics>().mutate(mutRate, Bbiases);
                }
                
            }
        }
    }
    bool isOver() {
        if (GameObject.FindGameObjectsWithTag("Ai").Length == 0 || timer > genTime)
        {
            return true;
        }
        else {
            return false;
        }
    }

    void clear() {
        GameObject[] ais = GameObject.FindGameObjectsWithTag("Ai");
        foreach (GameObject ai in ais) {
            storeData(ai);
            Destroy(ai);
        }
    }

    void initialiseCourse() {
        int x = 0;
        foreach (GameObject goal in GameObject.FindGameObjectsWithTag("goal"))
        {
            goal.GetComponent<GoalScript>().Ident = x;
            x++;
        }
    }

    public void storeData(GameObject ai) {

        float[][] toStoreWeights = ai.GetComponent<Brain>().weights;
        float[][] toStoreBiases = ai.GetComponent<Brain>().biases;
        float fitness = ai.GetComponent<FitCheck>().fitness;
        for (int x = 0; x < populationSize; x++) {
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

