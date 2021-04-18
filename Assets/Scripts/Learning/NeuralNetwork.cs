using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuralNetwork : MonoBehaviour
{
    private Utils utilities;
    private void Awake()
    {
        utilities = FindObjectOfType<GameManager>().GetComponent<Utils>();
    }
    public float[] forwardPass(int[] layers, float[] inputs, float[][] weights, float[][] biases) {
        for (int layer = 0; layer < layers.Length-1; layer++) {
            inputs = layerDense(inputs, weights[layer], biases[layer], layers[layer + 1]);
        }
        return inputs;
    }

    public float[] layerDense(float[] inputs, float[] weights, float[] biases, int outCount) {
        float[] outputs = new float[outCount];
        for (int node = 0; node < inputs.Length; node++) {
            int wPerNode = (weights.Length / inputs.Length);
            for (int weight = 0; weight < wPerNode; weight++)
            {
                outputs[weight] += weights[(node * wPerNode) + weight] * inputs[node];
            }
        }
        for (int outs = 0; outs < outCount; outs++) {
            outputs[outs] = utilities.tanh(outputs[outs] + biases[outs]);
        }
        return outputs;
    }
}
