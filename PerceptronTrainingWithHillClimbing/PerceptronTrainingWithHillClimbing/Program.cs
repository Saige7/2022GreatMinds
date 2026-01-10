namespace PerceptronTrainingWithHillClimbing
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
                outputs[i] = slope * inputs[i][0] + yint;
            }

            return outputs;
        }
        static void LineOfBestFit(Perceptron perceptron, double[][] inputs, double[] desiredOutputs)
        {
            double currentError = perceptron.GetError(inputs, desiredOutputs);
            while (currentError > .5)
            {
                currentError = perceptron.TrainWithHillClimbing(inputs, desiredOutputs, currentError);
                Console.WriteLine(currentError);
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
            Perceptron perceptronAnd = new Perceptron(weights, random.NextDouble(), 0.01, random, ErrorFunc);
            Perceptron perceptronOr = new Perceptron(weights, random.NextDouble(), 0.01, random, ErrorFunc);

            AndGate(perceptronAnd, getDesiredOutputsAndGate(inputsForGates), inputsForGates);
            OrGate(perceptronOr, getDesiredOutputsOrGate(inputsForGates), inputsForGates);
            double[] or = perceptronOr.Compute(inputsForGates);
            double[] and = perceptronAnd.Compute(inputsForGates);

            
            weights = new double[10];
            double[][] inputs = new double[10][];
            for (int i = 0; i < weights.Length; i++)
            {
                weights[i] = random.NextDouble();
            }
            for (int i = 0; i < inputs.Length; i++)
            {
                inputs[i] = [i];
            }
            Perceptron perceptronLineOfBestFit = new Perceptron(weights, random.NextDouble(), 0.1, random, ErrorFunc);

            //LineOfBestFit(perceptronLineOfBestFit, inputs, getDesiredOutputsForLineOfBestFit(inputs, 2, 3));
            

        }
    }
}
