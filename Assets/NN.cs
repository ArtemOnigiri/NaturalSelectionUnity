using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NN
{
    public Layer[] layers;

    public NN(params int[] sizes)
    {
        layers = new Layer[sizes.Length];
        for (int i = 0; i < sizes.Length; i++)
        {
            int nextSize = 0;
            if(i < sizes.Length - 1) nextSize = sizes[i + 1];
            layers[i] = new Layer(sizes[i], nextSize);
            for (int j = 0; j < sizes[i]; j++)
            {
                for (int k = 0; k < nextSize; k++)
                {
                    layers[i].weights[j, k] = UnityEngine.Random.Range(-1f, 1f);
                }
            }
        }
    }

    public float[] FeedForward(float[] inputs)
    {
        Array.Copy(inputs, 0, layers[0].neurons, 0, inputs.Length);
        for (int i = 1; i < layers.Length; i++) 
        {
            float min = 0f;
            if(i == layers.Length - 1) min = -1f;
            Layer l = layers[i - 1];
            Layer l1 = layers[i];
            for (int j = 0; j < l1.size; j++)
            {
                l1.neurons[j] = 0;
                for (int k = 0; k < l.size; k++)
                {
                    l1.neurons[j] += l.neurons[k] * l.weights[k, j];
                }
                l1.neurons[j] = Mathf.Min(1f, Mathf.Max(min, l1.neurons[j]));
            }
        }
        return layers[layers.Length - 1].neurons;
    }

}