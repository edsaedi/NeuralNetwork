using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    internal class Genetic_Learning
    {
        //This implementation will either change the weight/bias by some percentage change the sign of the weight/bias
        public void Mutate(NeuralNetwork net, Random random, double mutationRate)
        {
            foreach (Layer layer in net.Layers)
            {
                foreach (Neuron neuron in layer.Neurons)
                {
                    foreach (Dendrite dendrite in neuron.dendrites)
                        //Mutate the Weights
                        if (random.NextDouble() < mutationRate)
                        {
                            if (random.Next(2) == 0)
                            {
                                dendrite.Weight *= random.NextDouble(0.5, 1.5); //scale weight
                            }
                            else
                            {
                                dendrite.Weight *= -1; //flip sign
                            }
                        }

                    //Mutate the Bias
                    if (random.NextDouble() < mutationRate)
                    {
                        if (random.Next(2) == 0)
                        {
                            neuron.bias *= random.NextDouble(0.5, 1.5); //scale weight
                        }
                        else
                        {
                            neuron.bias *= -1; //flip sign
                        }
                    }
                }
            }
        }

        public void Crossover(NeuralNetwork winner, NeuralNetwork loser, Random random)
        {
            for (int i = 0; i < winner.Layers.Length; i++)
            {
                //References to the Layers
                Layer winLayer = winner.Layers[i];
                Layer childLayer = loser.Layers[i];

                int cutPoint = random.Next(winLayer.Neurons.Length); //calculate a cut point for the layer
                bool flip = random.Next(2) == 0; //randomly decide which side of the cut point will come from winner

                //Either copy from 0->cutPoint or cutPoint->Neurons.Length from the winner based on the flip variable
                for (int j = (flip ? 0 : cutPoint); j < (flip ? cutPoint : winLayer.Neurons.Length); j++)
                {
                    //References to the Neurons
                    Neuron winNeuron = winLayer.Neurons[j];
                    Neuron childNeuron = childLayer.Neurons[j];

                    //Copy the winners Weights and Bias into the loser/child neuron
                    for(int k = 0; k < winNeuron.dendrites.Length; k++) 
                    {
                        childNeuron.dendrites[k] = winNeuron.dendrites[k];
                    }

                    childNeuron.bias = winNeuron.bias;
                }
            }
        }


        //This function assumes all the fitness values have already been calculated
        public void Train((NeuralNetwork network, double fitness)[] population, Random random, double mutationRate)
        {
            Array.Sort(population, (a, b) => b.fitness.CompareTo(a.fitness));

            int start = (int)(population.Length * 0.1);
            int end = (int)(population.Length * 0.9);

            //Notice that this process is only called on networks in the middle 80% of the array
            for (int i = start; i < end; i++)
            {
                Crossover(population[random.Next(start)].network, population[i].network, random);
                Mutate(population[i].network, random, mutationRate);
            }

            //Removes the worst performing networks
            for (int i = end; i < population.Length; i++)
            {
                population[i].network.Randomize(random);
            }
        }
    }
}
