using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    public class NeuralNetwork
    {
        public Layer[] Layers { get; private set; }
        ErrorFunction errorFunc;
        public int InputCount => Layers[0].Neurons.Length;
        public int OutputCount => Layers[Layers.Length - 1].Neurons.Length;

        public NeuralNetwork(ActivationFunction activation, ErrorFunction errorFunc, params int[] neuronsPerLayer)
        {
            if (neuronsPerLayer == null || neuronsPerLayer.Length < 2)
            {
                throw new Exception("Neural network must have at least 2 Layers");
            }

            this.errorFunc = errorFunc;

            Layers = new Layer[neuronsPerLayer.Length];
            Layer previousLayer = null;
            for (int i = 0; i < neuronsPerLayer.Length; i++)
            {
                Layers[i] = new Layer(activation, neuronsPerLayer[i], previousLayer);
                previousLayer = Layers[i];
            }
        }

        public void Randomize(Random random, double min, double max)
        {
            for (int i = 0; i < Layers.Length; i++)
            {
                Layers[i].Randomize(random, min, max);
            }
        }

        public double[] Compute(double[] inputs)
        {
            if (inputs == null || inputs.Length != InputCount)
            {
                throw new Exception("Inputs must be the same length as input count");
            }

            for (int i = 0; i < InputCount; i++)
            {
                Layers[0].Neurons[i].Output = inputs[i];
            }

            double[] outputs = null;
            for (int i = 1; i < Layers.Length; i++)
            {
                outputs = Layers[i].Compute();
            }

            return outputs;
        }

        public double GetError(double[] inputs, double[] desiredOutputs)
        {
            if (desiredOutputs == null || desiredOutputs.Length != OutputCount) 
            { 
                throw new Exception("Desired outputs must be the same length as output count"); 
            }

            double error = 0;

            double[] outputs = Compute(inputs);

            for (int i = 0; i < outputs.Length; i++)
            {
                error += errorFunc.Function(outputs[i], desiredOutputs[i]);
            }

            return error; 
        }
    }
}
