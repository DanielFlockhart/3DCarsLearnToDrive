using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelController : MonoBehaviour
{
    public int opponents = 64;
    public GameObject car;
    public List<float[][]> weights;
    public List<float[][]> biases;
    public void place(){
        for (int x = 0; x < opponents; x++) {
            GameObject ai = Instantiate(car);
            ai.GetComponent<AiController>().state = "new";
            ai.name = "car" + x;
            ai.GetComponent<Brain>().build();
            ai.GetComponent<Brain>().setWeights(weights[x]);
            ai.GetComponent<Brain>().setBiases(biases[x]);
        }
    }
    public void load_weights(List<float[][]> weights_load, List<float[][]> biases_load){
        weights = weights_load;
        biases = biases_load;
        place();
    }
}
