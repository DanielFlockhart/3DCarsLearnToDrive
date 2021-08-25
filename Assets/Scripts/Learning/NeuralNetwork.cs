﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuralNetwork : MonoBehaviour
{
    /*Main Neural Network template for feed forward neural network.
    May be subject to change to NEAT if I have time*/
    
    // it is currently 00:40 i am tipsy and i am starting to go insane 
    private Utils utilities;

    // Assign utilties gameobject
    private void Awake()
    {
        utilities = FindObjectOfType<Utils>();
    }

    // One full pass of a FFNN returning outputs in float[]
    public float[] forwardPass(int[] layers, float[] inputs, float[][] weights, float[][] biases) {
        for (int layer = 0; layer < layers.Length-1; layer++) {
            inputs = layerDense(inputs, weights[layer], biases[layer], layers[layer + 1]);
        }
        return inputs;
    }


    // One dot product multiplication between dense layers
    public float[] layerDense(float[] inputs, float[] weights, float[] biases, int outCount) {
        float[] outputs = new float[outCount];
        for (int node = 0; node < inputs.Length; node++) {
            int wPerNode = (weights.Length / inputs.Length);
            for (int weight = 0; weight < wPerNode; weight++)
            {
                // Adds the value of the weight times the input to the node value
                outputs[weight] += weights[(node * wPerNode) + weight] * inputs[node];
            }
        }
        for (int outs = 0; outs < outCount; outs++) {
            // Activation function to create non-linearity
            outputs[outs] = utilities.tanh(outputs[outs] + biases[outs]);
        }
        return outputs;
    }
}
