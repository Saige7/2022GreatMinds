using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkAssignments
{
    internal class Neuron
    {
        public double bias;
        public Dendrite[] Dendrites;
        public double Output { get; set; }
        public double Input { get; private set; }
        public ActivationFunction ActivationFunction { get; set; }

        public Neuron(ActivationFunction activationFunction, Neuron[] previousNeurons)
        {
            ActivationFunction = activationFunction;
            if (previousNeurons != null)
            {
                Dendrites = new Dendrite[previousNeurons.Length];
            }
        }

        public void Randomize(Random random, double min, double max)
        {
            for (int i = 0; i < Dendrites.Length; i++)
            {
                Dendrites[i].Weight = (random.NextDouble() * (max - min)) + min;
            }

            bias = (random.NextDouble() * (max - min)) - min;
        }
        public double Compute()
        {
            for (int i = 0; i < Dendrites.Length; i++)
            {
                Input += Dendrites[i].Compute();
            }
            Input += bias;

            Output = ActivationFunction.function(Input);

            return Output;
        }
    }
}
