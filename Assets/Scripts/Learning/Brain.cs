﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Brain : MonoBehaviour
{
    // Setup of weights, biases topology etc

    // Prefabs
    private NeuralNetwork net;

    // Network Variables
    public int[] layers;

    public float[][] weights;
    public float[][] biases;
    

    // Assign Neural network before cars start to move to avoid errors
    void Awake()
    {
        net = GetComponent<NeuralNetwork>();
    }
    // Initialise weights and biases
    public void build() {
        gameObject.GetComponent<AiController>().layerSetup();
        layers = GetComponent<AiController>().layers;
        
        weights = initWeights(layers);
        biases = initBiases(layers);
        
    }
    // Get outputs from FFNN after inputs
    public float[] getOutputs(float[] inputs)
    {
        layers = GetComponent<AiController>().layers;
        return net.forwardPass(layers, inputs, weights, biases);
    }

    // Setters for weights and biases
    public void setWeights(float[][] w){
        Array.Copy(w,weights, w.Length);
    }

    public void setBiases(float[][] b){
        Array.Copy(b,biases, b.Length);
    }

    /*
    Initialisation process of brain
    Could implement other weight initialisation techniques like glorot
    Similar Code used for initialising weights and biases - In refactoring I will combine
    Initialise Biases
    */
    private float[][] initBiases(int[] layers)
    {
        float[][] biasList = new float[layers.Length-1][];
        for (int layer = 0; layer < biasList.Length; layer++)
        {
            int layerLength = layers[layer + 1];
            biasList[layer] = new float[layerLength];
            for (int node = 0; node < layerLength; node++)
            {
                // Set new random bias
                biasList[layer][node] = UnityEngine.Random.Range(-1.0f, 1.0f);
            }
        }
        
        return biasList;
    }
    // Initialising Weight
    private float[][] initWeights(int[] layers)
    {
        float[][] weightList = new float[layers.Length-1][];
        for (int layer = 0; layer < weightList.Length; layer++)
        {
            int layerLength = layers[layer] * layers[layer + 1];
            weightList[layer] = new float[layerLength];
            for (int connection = 0; connection < layerLength; connection++)
            {
                // Set new random weight
                weightList[layer][connection] = UnityEngine.Random.Range(-1.0f, 1.0f);
            }
        }
        return weightList;

    }
}
