using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkAssignments
{
    internal class NeuralNetwork
    {
        Layer[] Layers;
        ErrorFunction ErrorFunction;

        public NeuralNetwork(ActivationFunction activationFunction, ErrorFunction errorFunction, params int[] neuronsPerLayer)
        {
            ErrorFunction = errorFunction;
            Layers = new Layer[neuronsPerLayer.Length];

            Layers[0] = new Layer(activationFunction, neuronsPerLayer[0], null);
            for (int i = 1; i < neuronsPerLayer.Length; i++)
            {
                Layers[i] = new Layer(activationFunction, neuronsPerLayer[i], Layers[i - 1]);
            }
            
        }
        public void Randomize(Random random, double min, double max)
        {
            for (int i = 0; i < Layers.Length; i++)
            {
                Layers[i].Randomize(random, min, max);
            }
        }
        public double[] Compute(double[] inputs)
        {
            for (int i = 0; i < Layers[0].Neurons.Length; i++)
            {
                Layers[0].Neurons[i].Output = inputs[i];
            }

            for (int i = 1; i < Layers.Length; i++)
            {
                Layers[i].Compute();
            }

            return Layers[Layers.Length - 1].Outputs;
        }
        public double GetError(double[] inputs, double[] desiredOutputs)
        {
            double[] outputs = Compute(inputs);
            double error = 0;

            for (int i = 0; i < outputs.Length; i++)
            {
                error += ErrorFunction.function(desiredOutputs[i], outputs[i]);
            }

            return error / desiredOutputs.Length;
        }
    }
}
