using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network
{
    public class GeneticAlgorithm
    {
        private const int ELITE = 5;
        private Random rand;
        private IFiteness _fiteness;
        public GeneticAlgorithm()
        {
            rand = new Random();
        }
        public GeneticAlgorithm(IFiteness fiteness)
            : this()
        {
            _fiteness = fiteness;
            //rand = new Random();
        }
        /// <summary>
        /// Initilize generation of neural networks
        /// </summary>
        /// <param name="networks">networks array</param>
        /// <param name="input">count of neurons in input layer</param>
        /// <param name="output">count of neurons in output layer</param>
        /// <param name="layers">count of layers</param>
        /// <param name="perLayer">count of neurons per layer</param>
        public void InitializePopulation(Network [] networks,int input,int output,int layers,int perLayer,IFiteness fitness)
        {
            for(int i=0;i<networks.Length;i++)
            {
                Neuron [] neurons = new Neuron [input];
                for(int j=0;j<input;j++)
                {
                    neurons[j] = new Neuron(GetRandomFloat(rand));
                }
                Layer inputL = new Layer(neurons); 
                neurons = new Neuron[output];
                for(int j=0;j<output;j++)
                {
                    neurons[j] = new Neuron(GetRandomFloat(rand));
                }
                Layer outputL = new Layer(neurons);
                Layer [] layersL = new Layer[layers];
                for(int j=0;j<layers;j++)
                {
                    neurons = new Neuron [perLayer];
                    for(int k=0;k<perLayer;k++)
                    {
                        neurons[k] = new Neuron(GetRandomFloat(rand));
                    }
                    layersL[j] = new Layer(neurons);
                }
                networks[i] = new Network(inputL,outputL,layersL);
                networks[i].Fitness = fitness;
            }
        }

        /// <summary>
        /// Initilize generation of neural networks
        /// </summary>
        /// <param name="networks">networks array</param>
        /// <param name="input">count of neurons in input layer</param>
        /// <param name="output">count of neurons in output layer</param>
        /// <param name="layers">count of layers</param>
        /// <param name="perLayer">count of neurons per layer</param>
        public void InitializePopulation(Network[] networks, int input, int output, int layers, int perLayer)
        {
            for (int i = 0; i < networks.Length; i++)
            {
                Neuron[] neurons = new Neuron[input];
                for (int j = 0; j < input; j++)
                {
                    neurons[j] = new Neuron(GetRandomFloat(rand));
                }
                Layer inputL = new Layer(neurons);
                neurons = new Neuron[output];
                for (int j = 0; j < output; j++)
                {
                    neurons[j] = new Neuron(GetRandomFloat(rand));
                }
                Layer outputL = new Layer(neurons);
                Layer[] layersL = new Layer[layers];
                for (int j = 0; j < layers; j++)
                {
                    neurons = new Neuron[perLayer];
                    for (int k = 0; k < perLayer; k++)
                    {
                        neurons[k] = new Neuron(GetRandomFloat(rand));
                    }
                    layersL[j] = new Layer(neurons);
                }
                networks[i] = new Network(inputL, outputL, layersL);
            }
        }

        public float GetRandomFloat(Random rand)
        {
            double mantissa = (rand.NextDouble() * 2.0) - 1.0;
            double exponent = Math.Pow(2.0, rand.Next(-126, 128));
            return (float) (mantissa * exponent);
        }

        public void Mutate(Network network)
        {
            //mutate input
            if(rand.Next(1,100) > 80)
            {
                float w = GetRandomFloat(rand);
                w -= (int) w;
                network.InputLayer[rand.Next(0, network.InputLayer.Length - 1)].StartWeight = w;
            }
            //mutate output
            if(rand.Next(1,100) > 80)
            {
                float w = GetRandomFloat(rand);
                w -= (int)w;
                network.OutputLayer[rand.Next(0, network.InputLayer.Length - 1)].StartWeight = w;
            }
            //mutate mainLayer
            int countOfLayers = rand.Next(0,network.Mainlayer.Length);
            for(int i=0;i<countOfLayers / 2;i++)
            {
                int index = rand.Next(0,countOfLayers-1);
                int gene = rand.Next(0,network.Mainlayer[index].Length-1);
                float w = GetRandomFloat(rand);
                w -= (int)w;
                network.Mainlayer[index][gene].StartWeight = w;
            }
        }

        public Network [] Mate(Network[] networks)
        {
            Array.Sort(networks);
            Network [] resNetworks = new Network[networks.Length];
            SelectElite(networks,resNetworks);
            CrossOver(resNetworks);
            PerformMutate(resNetworks);
            return resNetworks;
        }

        private void SelectElite(Network [] networks, Network [] elite)
        {
            for (int i = 0; i < networks.Length / ELITE; i++)
                elite[i] = networks[i];
        }

        private void CrossOver(Network [] networks)
        {
            for (int i = networks.Length / ELITE; i < networks.Length - networks.Length / ELITE; i++)
            {
                networks[i] = CrossOver(networks[rand.Next(0, networks.Length / ELITE - 1)], networks[rand.Next(0, networks.Length / ELITE - 1)]);
            }
        }

        private Network CrossOver(Network network1, Network network2)
        {
            Network result = new Network();
            result.InputLayer = (Layer) network1.InputLayer.Clone();
            result.OutputLayer = (Layer)network2.OutputLayer.Clone();
            result.Mainlayer = new Layer[network1.Mainlayer.Length];
            result.Fitness = network1.Fitness;
            for (int i = 0; i < result.Mainlayer.Length / 2; i++ )
            {
                result.Mainlayer[i] = (Layer) network1.Mainlayer[i].Clone();
            }
            for (int i = result.Mainlayer.Length / 2; i < result.Mainlayer.Length;i++ )
            {
                result.Mainlayer[i] = (Layer) network2.Mainlayer[i].Clone();
            }
            return result;
        }

        private void PerformMutate(Network [] network)
        {
            for (int i = network.Length - network.Length / ELITE; i < network.Length; i++)
            {
                network[i] = network[rand.Next(0, network.Length - network.Length / ELITE)];
                Mutate(network[i]);
            }
        }
    }
}
