using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerceptronAssignment
{
    internal class Perceptron
    {
        public double[] weights;
        public double bias;

        public Perceptron(double[] initialWeightValues, double initialBiasValue)
        {
            weights = initialWeightValues;
            bias = initialBiasValue;
        }
        public Perceptron(int amountOfWeights)
        {
            weights = new double[amountOfWeights];
        }

        public void Randomize(Random random, double min, double max)
        {
            for (int i = 0; i < weights.Length; i++)
            {
                weights[i] = (random.NextDouble() * (max - min)) + min;
            }

            bias = (random.NextDouble() * (max - min)) + min;
        }
        public double Compute(double[] inputs)
        {
            double output = 0;
            for (int i = 0; i < inputs.Length; i++)
            {
                output += (inputs[i] * weights[i]);
            }
            return output + bias;
        }
        public double[] Compute(double[][] inputs)
        {
            double[] output = new double[inputs.Length];
            for (int rows = 0; rows < inputs.GetLength(0); rows++)
            {
                output[rows] = Compute(inputs[rows]);
            }
            return output;
        }
    }
}
