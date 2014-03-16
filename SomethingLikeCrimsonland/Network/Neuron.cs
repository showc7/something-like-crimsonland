using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network
{
    public class Neuron
    {
        public float Weight { get; set; }
        public float StartWeight { get; set; }
        public Neuron(float weight)
        {
            StartWeight = weight;
        }
        public Neuron(float weight, float startWeight)
            : this (weight)
        {
            StartWeight = startWeight;
        }
        public float Signal()
        {
            return Function(StartWeight + Weight);
        }

        protected float Function(float val)
        {
            //return (float)(1 / (1 + Math.Exp(-val)));
            //return (float) ((Math.Exp(val) - Math.Exp(-val))/(Math.Exp(val) + Math.Exp(-val)));
            return (float) (1 / (1 + Math.Exp(val)));
        }

        public object Clone()
        {
            return new Neuron(Weight,StartWeight);
        }
    }
}
