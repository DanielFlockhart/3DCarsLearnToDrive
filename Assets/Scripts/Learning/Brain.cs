﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Brain : MonoBehaviour
{
    private NeuralNetwork net;
    private Utils utilities;
    public int[] layers;

    private float[][] weights;
    private float[][] biases;
    
    private void Start()
    {
        utilities = FindObjectOfType<GameManager>().GetComponent<Utils>();
        net = GetComponent<NeuralNetwork>();
    }
    public void build(int[] netLayers) {
        layers = netLayers;
        weights = initWeights(layers);
        biases = initBiases(layers);
    }


    public float[] getOutputs(float[] inputs)
    {
        return net.forwardPass(layers, inputs, weights, biases);
    }
    public float[][] initBiases(int[] layers)
    {
        float[][] biasList = new float[layers.Length-1][];
        for (int layer = 0; layer < biasList.Length; layer++)
        {
            int layerLength = layers[layer + 1];
            biasList[layer] = new float[layerLength];
            for (int node = 0; node < layerLength; node++)
            {
                biasList[layer][node] = utilities.RandomFloat();
            }
        }
        return biasList;
    }
    public float[][] initWeights(int[] layers)
    {

        float[][] weightList = new float[layers.Length-1][];
        for (int layer = 0; layer < weightList.Length; layer++)
        {
            int layerLength = layers[layer] * layers[layer + 1];
            weightList[layer] = new float[layerLength];
            for (int connection = 0; connection < layerLength; connection++)
            {
                weightList[layer][connection] = utilities.RandomFloat();
            }
        }
        return weightList;

    }
}
