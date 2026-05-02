using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkAssignments
{
    internal class GeneticLearning
    {
        public (NeuralNetwork Network, double fitness)[] Population;
        public GeneticLearning(ActivationFunction activationFunction, ErrorFunction errorFunction, int populationSize, params int[] neuronsPerLayer)
        {
            Population = new (NeuralNetwork, double)[populationSize];
            for (int i = 0; i < populationSize; i++)
            {
                Population[i].Network = new NeuralNetwork(activationFunction, errorFunction, neuronsPerLayer);
            }
        }
        public double Fitness(NeuralNetwork neuralNetwork)
        {

        }
        public void Mutate(NeuralNetwork neuralNetwork, Random random, double mutationRate)
        {
            for (int i = 0; i < neuralNetwork.Layers.Length; i++)
            {
                for (int j = 0; j < neuralNetwork.Layers[i].Neurons.Length; j++)
                {
                    for(int k = 0; k < neuralNetwork.Layers[i].Neurons[j].Dendrites.Length; k++)
                    {
                        if (random.NextDouble() < mutationRate)
                        {
                            if (random.Next(2) == 0)
                            {
                                neuralNetwork.Layers[i].Neurons[j].Dendrites[k].Weight += ((random.NextDouble() * 2) - 1);
                            }
                            else
                            {
                                neuralNetwork.Layers[i].Neurons[j].Dendrites[k].Weight *= -1;
                            }
                        }
                    }

                    if (random.NextDouble() < mutationRate)
                    {
                        if (random.Next(2) == 0)
                        {
                            neuralNetwork.Layers[i].Neurons[j].bias *= -1;
                        }
                        else
                        {
                            neuralNetwork.Layers[i].Neurons[j].bias += ((random.NextDouble() * 2) - 1);
                        }
                    }
                }
            }
        }
        public void Crossover(NeuralNetwork parent, NeuralNetwork child, Random random)
        {
            for (int i = 0; i < parent.Layers.Length; i++)
            {
                int cutpoint = random.Next(parent.Layers[i].Neurons.Length);
                int side = random.Next(2);

                if(side == 0)
                {
                    for (int j = 0; j < cutpoint; j++)
                    {
                        for (int k = 0; k < parent.Layers[i].Neurons[j].Dendrites.Length; k++)
                        {
                            double parentWeight = parent.Layers[i].Neurons[j].Dendrites[k].Weight;
                            child.Layers[i].Neurons[j].Dendrites[k].Weight = parentWeight;
                        }

                        double parentBias = parent.Layers[i].Neurons[j].bias;
                        child.Layers[i].Neurons[j].bias = parentBias;
                    }
                }
                else
                {
                    for (int j = cutpoint; j < parent.Layers[i].Neurons.Length; j--)
                    {
                        for (int k = 0; k < parent.Layers[i].Neurons[j].Dendrites.Length; k++)
                        {
                            double parentWeight = parent.Layers[i].Neurons[j].Dendrites[k].Weight;
                            child.Layers[i].Neurons[j].Dendrites[k].Weight = parentWeight;
                        }

                        double parentBias = parent.Layers[i].Neurons[j].bias;
                        child.Layers[i].Neurons[j].bias = parentBias;
                    }
                }
            }
        }
        public void Train((NeuralNetwork Network, double fitness)[] Population, double mutationRate, Random random)
        {
            Array.Sort(Population, (x, y) => x.fitness.CompareTo(y.fitness));
            int topPercent = (int)(Population.Length * 0.1) + 2;
            int bottomPercent = (int)(Population.Length * 0.05) + 2;

            for (int i = topPercent; i < Population.Length - bottomPercent; i++)
            {
                int randomTop = random.Next(topPercent);
                Crossover(Population[randomTop].Network, Population[i].Network, random);
                Mutate(Population[i].Network, random, mutationRate);
            }

            for (int i = Population.Length - bottomPercent; i < Population.Length; i++)
            {
                Population[i].Network.Randomize(random, 0, 10);//?
            }
        }
    }
}
