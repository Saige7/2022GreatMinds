namespace MorePerceptron
{
    internal class Program
    {
        static double ErrorFunc(double[] outputs, double[] desiredOutputs)
        {
            double error = 0;
            for (int i = 0; i < desiredOutputs.Length; i++)
            {
                error += Math.Pow((desiredOutputs[i] - outputs[i]), 2);
            }

            return error / desiredOutputs.Length;
        }

        static double[] getDesiredOutputsForLineOfBestFit(double[][] inputs, double slope, double yint)
        {
            double[] outputs = new double[inputs.Length];
            for (int i = 0; i < inputs.Length; i++)
            {
                outputs[i] = Normalize(slope * inputs[i][0] + yint, 0, 30, 0, 1);
            }

            return outputs;
        }
        static void LineOfBestFit(Perceptron perceptron, double[][] inputs, double[] desiredOutputs)
        {
            double currentError = perceptron.GetError(inputs, desiredOutputs);
            double preverror = 0;
            while (currentError != preverror)
            {
                //currentError = perceptron.TrainWithHillClimbing(inputs, desiredOutputs, currentError);
                preverror = currentError;
                currentError = perceptron.BatchTrain(inputs, desiredOutputs);
                Console.WriteLine($"total error: {currentError}");
            }
        }

        static double[] getDesiredOutputsAndGate(double[][] inputs)
        {
            double[] outputs = new double[inputs.Length];
            for (int i = 0; i < inputs.Length; i++)
            {
                outputs[i] = (int)inputs[i][0] & (int)inputs[i][1];
            }

            return outputs;
        }
        static void AndGate(Perceptron perceptron, double[] desiredOutputs, double[][] inputs)
        {
            double currentError = perceptron.GetError(inputs, desiredOutputs);
            //while (currentError > 0.15)
            for (int i = 0; i < 6000; i++)
            {
                currentError = perceptron.TrainWithHillClimbing(inputs, desiredOutputs, currentError);
                Console.WriteLine(currentError);
            }
        }
        static double[] getDesiredOutputsOrGate(double[][] inputs)
        {
            double[] outputs = new double[inputs.Length];
            for (int i = 0; i < inputs.Length; i++)
            {
                outputs[i] = (int)inputs[i][0] | (int)inputs[i][1];
            }

            return outputs;
        }
        static void OrGate(Perceptron perceptron, double[] desiredOutputs, double[][] inputs)
        {
            double currentError = perceptron.GetError(inputs, desiredOutputs);
            //while (currentError > 0.07)
            for (int i = 0; i < 3000; i++)
            {
                currentError = perceptron.TrainWithHillClimbing(inputs, desiredOutputs, currentError);
                Console.WriteLine($"or:{currentError}");
            }
        }

        static double Normalize(double value, double min, double max, double nMin, double nMax)
        {
            return ((value - min) / (max - min)) * (nMax - nMin) + nMin;
        }
        static double[] Filtering(double[] outputs)
        {
            double[] filteredOutputs = new double[outputs.Length];
            for (int i = 0; i < outputs.Length; i++)
            {
                if (outputs[i] >= 0.5)
                {
                    filteredOutputs[i] = 1;
                }
                else
                {
                    filteredOutputs[i] = 0;
                }
            }

            return filteredOutputs;
        }
        static void Main(string[] args)
        {
            Random random = new Random();
            double[] weights = new double[4];
            for (int i = 0; i < weights.Length; i++)
            {
                weights[i] = random.NextDouble();
            }
            double[][] inputsForGates = { [0, 0],
                                          [0, 1],
                                          [1, 0],
                                          [1, 1]};
            Perceptron perceptronAnd = new Perceptron(weights, random.NextDouble(), 0.01, random, ActivationFunctions.Sigmoid, ErrorFunctions.MSE);
            Perceptron perceptronOr = new Perceptron(weights, random.NextDouble(), 0.01, random, ActivationFunctions.Sigmoid, ErrorFunctions.MSE);

            //OrGate(perceptronOr, getDesiredOutputsOrGate(inputsForGates), inputsForGates);
            //double[] or = perceptronOr.ComputeMoreWithActivationFunction(inputsForGates);
            //AndGate(perceptronAnd, getDesiredOutputsAndGate(inputsForGates), inputsForGates);
            //double[] and = perceptronAnd.ComputeMoreWithActivationFunction(inputsForGates);

            //double[] filteredAnd = Filtering(and);
            //double[] filteredOr = Filtering(or);

            double[][] inputs = new double[10][];
            for (int i = 0; i < inputs.Length; i++)
            {
                inputs[i] = new double[2];
                //inputs[i][0] = i;
                inputs[i][0] = Normalize(i, 0, 10, 0, 1);
            }
            double[] desiredOutputs = getDesiredOutputsForLineOfBestFit(inputs, 20, 6);
            for (int i = 0; i < inputs.Length; i++)
            {
                inputs[i][1] = desiredOutputs[i];
            }

            //Perceptron perceptronLineOfBestFit = new Perceptron(10, 0.1, random, ErrorFunc);
            //perceptronLineOfBestFit.Randomize(random, 0, 8);

            Perceptron gradientDescentPerceptron = new Perceptron(5, .001, ActivationFunctions.Tanh, ErrorFunctions.MSE);

            gradientDescentPerceptron.Randomize(random, 0, 1);
            LineOfBestFit(gradientDescentPerceptron, inputs, desiredOutputs);
            double[] gradientDescentResult = gradientDescentPerceptron.Compute(inputs);

            //LineOfBestFit(perceptronLineOfBestFit, inputs, desiredOutputs);
            //double[] result = perceptronLineOfBestFit.Compute(inputs);

        }
    }
}
