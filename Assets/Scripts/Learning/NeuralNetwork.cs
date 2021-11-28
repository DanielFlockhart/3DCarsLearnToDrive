using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuralNetwork : MonoBehaviour
{
    /*Main Neural Network template for feed forward neural network.
    May be subject to change to NEAT if I have time*/
    private Utils utilities;

    // Assign utilties gameobject
    private void Awake()
    {
        utilities = FindObjectOfType<Utils>();
    }

    // One full pass of a FFNN returning outputs in float[]
    public float[] forwardPass(int[] layers, float[] inputs, float[][] weights, float[][] biases) {
        //print(layers[0] + " " + layers[1] + " " + layers[2] + layers[3] + layers[4] + layers[5]);
        //print(weights[0].Length + " " + weights[1].Length + " " + weights[2].Length + " " + weights[3].Length + weights[4].Length + weights[5].Length);
        
        for (int layer = 0; layer < layers.Length-1; layer++) {
            // IS IT JUST CALLING THE INPUTS ON EACH LAYERRR???
            inputs = layerDense(inputs, weights[layer], biases[layer], layers[layer + 1]);
        }
        return inputs;
    }
    // One matrix multiplication between dense layers
    public float[] layerDense(float[] inputs, float[] weights, float[] biases, int outCount) {
        //print(inputs.Length + " " + weights.Length + " " + biases.Length + " " + outCount);
        print("FUCK"  + inputs.Length+" " + weights.Length);
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
            /* Activation function to create non-linearity
            Will eventually test out an experiment with different activation functions to find optimal
            Including sigmoid,tanh,relu and leaky relu
            */
            outputs[outs] = utilities.tanh(outputs[outs] + biases[outs]);
        }
        return outputs;
    }
}
