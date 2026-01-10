namespace PerceptronAssignment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Perceptron perceptron = new Perceptron(2);
            
            double[][] inputs = [
                [0, 0],
                [0.3, -0.7],
                [1, 1],
                [-1, -1],
                [-0.5, 0.5]
            ];
            perceptron.weights[0] = 0.75;
            perceptron.weights[1] = -1.25;
            perceptron.bias = 0.5;

            double[] outputs = perceptron.Compute(inputs);

            for (int i = 0; i < outputs.Length; i++)
            {
                Console.WriteLine(outputs[i]);
            }

            /*
             * To test that your compute function works, create a perceptron with initial weights 0.75 and -1.25, 
             * and an initial bias of 0.5. Run the compute function that takes in several rows of inputs, 
             * and use the following values as inputs: {0, 0}, {0.3, -0.7}, {1, 1}, {-1, -1}, 
             * and {-0.5, 0.5}. Check if it outputs the following values: 0.5, 1.6, 0, 1, and -0.5.
             */
        }
    }
}
