using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Brain : MonoBehaviour
{
    // Setup of weights, biases topology etc
    // This will be used more if I decide to implement NEAT

    // Prefabs
    private NeuralNetwork net;
    [SerializeField] Utils utilities;

    // Network Variables
    public int[] layers;

    public float[][] weights;
    public float[][] biases;
    

    // Assign Neural network before cars start to move to avoid errors
    void Awake()
    {
        utilities = FindObjectOfType<GameManager>().GetComponent<Utils>();
        net = GetComponent<NeuralNetwork>();
    }
    // Initialise weights and biases
    public void build() {
        weights = initWeights(layers);
        biases = initBiases(layers);
        
    }
    

    // Get outputs from FFNN after inputs
    public float[] getOutputs(float[] inputs)
    {
        return net.forwardPass(layers, inputs, weights, biases);
    }

    // Initialising Biases - Glorot Weight initialising?
    private float[][] initBiases(int[] layers)
    {
        float[][] biasList = new float[layers.Length-1][];
        for (int layer = 0; layer < biasList.Length; layer++)
        {
            int layerLength = layers[layer + 1];
            biasList[layer] = new float[layerLength];
            for (int node = 0; node < layerLength; node++)
            {
                // Should access random from utils but didnt like it
                biasList[layer][node] = Random.Range(-1.0f, 1.0f);
            }
        }
        return biasList;
    }

    // Initialising Weight - Glorot Weight initialising?
    private float[][] initWeights(int[] layers)
    {

        float[][] weightList = new float[layers.Length-1][];
        for (int layer = 0; layer < weightList.Length; layer++)
        {
            int layerLength = layers[layer] * layers[layer + 1];
            weightList[layer] = new float[layerLength];
            for (int connection = 0; connection < layerLength; connection++)
            {
                weightList[layer][connection] = Random.Range(-1.0f, 1.0f);
            }
        }
        return weightList;

    }
}
