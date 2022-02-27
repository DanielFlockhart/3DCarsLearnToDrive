using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelController : MonoBehaviour
{
    // Controller for loading driving game models 
    private int opponents = 160;
    public GameObject car;
    public List<float[][]> weights;
    public List<float[][]> biases;

    /* Place Opponents and assign weights
        Currently Models have some dodgy ones and when saving there is no sorting.
        Possibly add end procedures where ai reproduction changes and there is no mutation and it gets top 16 performing
    */
    public void place(){
        for (int x = 0; x < opponents; x++) {
            // Instantiate new car from prefab
            GameObject ai = Instantiate(car);
            ai.GetComponent<AiController>().state = "new";
            ai.name = "car" + x;
            // Build Neural Network and assign weights
            ai.GetComponent<Brain>().build();

            
            ai.GetComponent<Brain>().setWeights(weights[x]);
            ai.GetComponent<Brain>().setBiases(biases[x]);
        }
    }
    // Start placement process from model loading
    public void load_weights(List<float[][]> weights_load, List<float[][]> biases_load, int[] layers){
        setup.hiddenNodes = layers[1];
        setup.hiddenLayers = layers.Length - 2;
        weights = weights_load;
        biases = biases_load;
        
        place();
    }
}
