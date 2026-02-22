using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkAssignments
{
    internal class Layer
    {
        public Neuron[] Neurons { get; }
        public double[] Outputs { get; }

        public Layer(ActivationFunction activationFunction, int neuronCount, Layer previousLayer)
        {
            Neurons = new Neuron[neuronCount];
            for (int i = 0; i < Neurons.Length; i++)
            {
                if (previousLayer == null)
                {
                    Neurons[i] = new Neuron(activationFunction, null);
                }
                Neurons[i] = new Neuron(activationFunction, previousLayer.Neurons);
            }
        }

        public void Randomize(Random random, double min, double max)
        {
            for (int i = 0; i < Neurons.Length; i++)
            {
                Neurons[i].Randomize(random, min, max);
            }
        }
        public double[] Compute()
        {
            double[] outputs = new double[Neurons.Length];
            for(int i = 0; i < Neurons.Length; i++)
            {
                outputs[i] = Neurons[i].Compute();
            }

            return outputs;
        }
    }
}
