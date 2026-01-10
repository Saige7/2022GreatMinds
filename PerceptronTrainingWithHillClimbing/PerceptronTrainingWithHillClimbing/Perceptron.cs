using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerceptronTrainingWithHillClimbing
{
    internal class Perceptron
    {
        public double[] weights;
        public double bias;
        public double MutationAmount;
        public Random random;
        public Func<double[], double[], double> ErrorFunc;

        public Perceptron(double[] initialWeightValues, double initialBiasValue, double mutationAmount, Random rand, Func<double[], double[], double> errorFunc)
        {
            weights = initialWeightValues;
            bias = initialBiasValue;
            MutationAmount = mutationAmount;
            random = rand;
            ErrorFunc = errorFunc;
        }
        public Perceptron(int amountOfWeights, double mutationAmount, Random rand, Func<double[], double[], double> errorFunc)
        {
            weights = new double[amountOfWeights];
            MutationAmount = mutationAmount;
            random = rand;
            ErrorFunc = errorFunc;
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
        public double GetError(double[][] inputs, double[] desiredOutputs)
        {
            double[] outputs = Compute(inputs);
            return ErrorFunc(outputs, desiredOutputs);
        }
        public Perceptron Mutate()
        {
            Perceptron current = new Perceptron(weights, bias, MutationAmount, random, ErrorFunc);
            int weightOrBias = current.random.Next(2);
            int mutationChange = current.random.Next(2);

            if (weightOrBias == 0)
            {
                int whichWeight = current.random.Next(current.weights.Length);

                if (mutationChange == 0)
                {
                    current.weights[whichWeight] -= current.MutationAmount;
                }
                else
                {
                    current.weights[whichWeight] += current.MutationAmount;
                }
            }
            else
            {
                if (mutationChange == 0)
                {
                    current.bias -= current.MutationAmount;
                }
                else
                {
                    current.bias += current.MutationAmount;
                }
            }

            return current;
        }
        public double TrainWithHillClimbing(double[][] inputs, double[] desiredOutputs, double currentError)
        {
            Perceptron mutatedPerceptron = Mutate();
            double newError = mutatedPerceptron.GetError(inputs, desiredOutputs);

            if (newError < currentError)
            {
                currentError = newError;
                bias = mutatedPerceptron.bias;
                Array.Copy(mutatedPerceptron.weights, weights, weights.Length);
            }

            return currentError;
        }
    }
}
