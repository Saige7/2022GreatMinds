using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorePerceptron
{
    public class ErrorFunction
    {
        public Func<double, double, double> function;
        public Func<double, double, double> derivative;

        public ErrorFunction(Func<double, double, double> func, Func<double, double, double> deriv)
        {
            function = func;
            derivative = deriv;
        }
    }

    public static class ErrorFunctions
    {
        public static ErrorFunction MAE => new ErrorFunction((x, y) => Math.Abs(x - y), (x, y) => y - x > 0 ? -1 : 1);
        public static ErrorFunction MSE => new ErrorFunction((x, y) => (x - y) * (x - y), (x, y) => -2 * (x - y));
    }
}
