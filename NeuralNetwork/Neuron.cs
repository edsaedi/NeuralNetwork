using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    public class Neuron
    {
        double bias { get; set; }
        Dendrite[] dendrites { get; set; }
        public double Output { get; set; }
        public double Input { get; private set; }
        public ActivationFunction Activation { get; set; }

        public Neuron(ActivationFunction activation, Neuron[] previousNerons)
        {
            Activation = activation;

            if (previousNerons != null)
            {
                dendrites = new Dendrite[previousNerons.Length];
                for (int i = 0; i < dendrites.Length; i++)
                {
                    dendrites[i] = new Dendrite(previousNerons[i], this, 0);
                }
            }
            else
            {
                dendrites = null;
            }
        }

        public void Randomize(Random random, double min, double max)
        {
            bias = random.NextDouble(min, max);
            for (int i = 0; i < dendrites.Length; i++)
            {
                dendrites[i].Weight = random.NextDouble(min, max);
            }
        }

        public double Compute()
        {
            double finalOutput = bias;

            for (int i = 0; i < dendrites.Length; i++)
            {
                finalOutput += dendrites[i].Weight;
            }

            return Activation.Function(finalOutput);
        }
    }
}
