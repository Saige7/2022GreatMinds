using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkAssignments
{
    public class ActivationFunction
    {
        public Func<double, double> function;
        public Func<double, double> derivative;

        public ActivationFunction(Func<double, double> func, Func<double, double> deriv)
        {
            function = func;
            derivative = deriv;
        }
    }
    public static class ActivationFunctions
    {
        public static ActivationFunction Tanh => new ActivationFunction((x) => ( ( Math.Pow(Math.E, x) - Math.Pow(Math.E, -x) ) / ( Math.Pow(Math.E, x) + Math.Pow(Math.E, -x) ) ), 
                                                      (x) => ( 1 - Math.Pow(((Math.Pow(Math.E, x) - Math.Pow(Math.E, -x)) / (Math.Pow(Math.E, x) + Math.Pow(Math.E, -x))), 2) ));
        public static ActivationFunction Sigmoid => new ActivationFunction((x) => ( 1 / (1 + Math.Pow(Math.E, -x)) ), 
                                (x) => ( (1 / (1 + Math.Pow(Math.E, -x))) * (1 - (1 / (1 + Math.Pow(Math.E, -x)))) ) );
    }

}
