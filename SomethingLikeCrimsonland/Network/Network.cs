using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network
{
    public class Network : IComparable
    {
        public IFiteness Fitness { get; set; }
        private Layer _input;
        private Layer _output;
        private Layer[] _layers;
        public Network()
        {

        }
        public Network(Layer input, Layer output, Layer[] layers)
        {
            _input = input;
            _output = output;
            _layers = layers;
        }

        public float [] Calculate(float [] weights)
        {
            for(int i=0;i<_input.Length;i++)
            {
                _input[i].Weight += weights[i];
            }

            for(int i=0;i<_input.Length;i++)
            {
                float weight = _input[i].Signal();
                for(int j=0;j<_layers[0].Length;j++)
                {
                    _layers[0][i].Weight += weight;
                }
            }

            for(int i=0;i<_layers.Length-1;i++)
            {
                for(int j=0;j<_layers[i].Length;j++)
                {
                    float weight = _layers[i][j].Signal();
                    for(int k=0;k<_layers[i+1].Length;k++)
                    {
                        _layers[i + 1][k].Weight += weight;
                    }
                }
            }

            for(int i=0;i<_layers[0].Length;i++)
            {
                float weight = _layers[_layers.Length - 1][i].Weight;
                for(int j=0;j<_output.Length;j++)
                {
                    _output[j].Weight += weight;
                }
            }

            float [] result = new float[_output.Length];
            
            for (int i = 0; i < _output.Length; i++ )
                result[i] = _output[i].Signal();

            return result;
        }
        public Layer InputLayer
        {
            get { return _input; }
            set { _input = value; }
        }
        public Layer OutputLayer
        {
            get { return _output; }
            set { _output = value; }
        }
        public Layer[] Mainlayer
        {
            get { return _layers; }
            set { _layers = value; }
        }
        // RECHECK
        public int CompareTo(object obj)
        {
            if (!(obj is Network))
                return 0;
            //float a = this.Fitness.GetFitness(this);
            //float b = ((obj as Network).Fitness.GetFitness((Network) obj));

            float a = this.Fitness.GetFitness();
            float b = (obj as Network).Fitness.GetFitness();

            //if (a < 0) a = 0;
            //if (b < 0) b = 0;

            return (int ) ((b - a ));
            //return (int) ((this.Fitness.GetFitness(this) - (obj as Network).Fitness.GetFitness((Network) obj)) * 1000000);
        }
    }
}
