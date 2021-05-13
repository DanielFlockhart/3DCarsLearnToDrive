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
    public float[][] Bweights;
    public float[][] Bbiases;

    public float[][][] w_list;
    public float[][][] b_list;




    // Start is called before the first frame update
    void Start()
    {
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

    void storeData(GameObject ai) {

        float[][] toStoreWeights = ai.GetComponent<Brain>().weights;
        float[][] toStoreBiases = ai.GetComponent<Brain>().biases;
        float fitness = ai.GetComponent<FitCheck>().fitness;
    }
}

