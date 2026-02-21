using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorePerceptron
{
    internal class Perceptron
    {
        public double[] weights;
        public double bias;
        public double MutationAmount;
        public Random random;
        public double LearningRate { get; set; }

        ActivationFunction ActivationFunction;
        ErrorFunction ErrorFunction;

        public Perceptron(double[] initialWeightValues, double initialBiasValue, double mutationAmount, Random rand, ActivationFunction activationFunction, ErrorFunction errorFunc)
        {
            weights = initialWeightValues;
            bias = initialBiasValue;
            MutationAmount = mutationAmount;
            random = rand;
            ActivationFunction = activationFunction;
            ErrorFunction = errorFunc;
        }
        public Perceptron(int amountOfWeights, double mutationAmount, Random rand, ActivationFunction activationFunction, ErrorFunction errorFunction)
        {
            weights = new double[amountOfWeights];
            MutationAmount = mutationAmount;
            random = rand;
            ActivationFunction = activationFunction;
            ErrorFunction = errorFunction;
        }
        public Perceptron(int amountOfWeights, double learningRate, ActivationFunction activationFunction, ErrorFunction errorFunction)
        {
            weights = new double[amountOfWeights];
            LearningRate = learningRate;
            ActivationFunction = activationFunction;
            ErrorFunction = errorFunction;
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
        public double ComputeWithActivationFunction(double[] inputs)
        {
            return ActivationFunction.function(Compute(inputs));
        }
        public double[] ComputeMoreWithActivationFunction(double[][] inputs)
        {
            double[] output = new double[inputs.Length];
            for(int rows = 0; rows < inputs.GetLength(0); rows++)
            {
                output[rows] = ComputeWithActivationFunction(inputs[rows]);
            }
            return output;
        }
        public double GetError(double[][] inputs, double[] desiredOutputs)
        {
            double[] outputs = Compute(inputs);
            double error = 0;
            for (int i = 0; i < outputs.Length; i++)
            {
                error += ErrorFunction.function(desiredOutputs[i], outputs[i]);
                
            }
            Console.WriteLine(error);
            return error / desiredOutputs.Length;
        }
        public Perceptron Mutate()
        {
            Perceptron current = new Perceptron(weights, bias, MutationAmount, random, ActivationFunction, ErrorFunction);
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
        public double Train(double[] inputs, double desiredOutput)
        {
            double output = Compute(inputs);
            double change = LearningRate * -ErrorFunction.derivative(desiredOutput, output);

            for (int i = 0; i < weights.Length; i++)
            {
                weights[i] += change;
            }
            bias += change;

            return ErrorFunction.derivative(desiredOutput, output);
        }
        public double BatchTrain(double[][] inputs, double[] desiredOutput)
        {
            double[] output = Compute(inputs);
            double change = 0;
            for (int i = 0; i < output.Length; i++)
            {
                change += LearningRate * -ErrorFunction.derivative(desiredOutput[i], output[i]);
            }

            for (int i = 0; i < weights.Length; i++)
            {
                weights[i] += change;
            }
            bias += change;

            return GetError(inputs, desiredOutput);
        }
    }
}
