using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    public class ErrorFunction
    {
        Func<double, double, double> function;
        Func<double, double, double> derivative;

        public ErrorFunction(Func<double, double, double> function, Func<double, double, double> derivative)
        {
            if (function == null)
            {
                throw new NullReferenceException("Error Function: Function is null");    
            }

            this.function = function;

            if(derivative == null)
            {
                throw new NullReferenceException("Error Function: Derivative is null");
            }

            this.derivative = derivative;
        }

        public double Function(double output, double desiredOutput)
        {
            return function.Invoke(output, desiredOutput);
        }

        public double Derivative(double output, double desriedOutput)
        {
            return derivative.Invoke(output, desriedOutput);
        }
    }
}
