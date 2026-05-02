namespace NeuralNetworkAssignments
{
    internal class Program
    {
        static void Main(string[] args)
        {            
            Random random = new Random();
            NeuralNetwork neuralNetwork = new NeuralNetwork(ActivationFunctions.Tanh, ErrorFunctions.MSE, 2, 5, 8, 10, 1);

            neuralNetwork.Randomize(random, 1, 50);
            //double[] result = neuralNetwork.Compute();
        }
    }
}
