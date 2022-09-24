using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    public class ActivationFunction
    {
        Func<double, double> function;
        Func<double, double> derivative;
        public ActivationFunction(Func<double, double> function, Func<double, double> derivative)
        {
            if (function == null || derivative == null)
            {
                throw new NullReferenceException();
            }

            this.function = function;
            this.derivative = derivative;
        }

        public double Function(double input)
        {
            return function.Invoke(input);
        }

        public double Derivative(double input)
        {
            return derivative.Invoke(input);
        }
    }
}
