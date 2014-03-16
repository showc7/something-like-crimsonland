using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network
{
    public class Layer
    {
        private Neuron[] _neurons;
        public Layer(Neuron [] neurons)
        {
            _neurons = neurons;
        }

        public Neuron this[int index]
        {
            get
            {
                return _neurons[index];
            }
            set
            {
                _neurons[index] = value;
            }
        }

        public int Length
        {
            get { return _neurons.Length; }
        }

        public object Clone()
        {
            Neuron [] neurons = new Neuron [this.Length];
            for(int i=0;i<this.Length;i++)
            {
                neurons[i] = new Neuron(this[i].Weight);
                neurons[i].StartWeight = this[i].StartWeight;
            }
            Layer l = new Layer(neurons);
            return l;
        }
    }
}
