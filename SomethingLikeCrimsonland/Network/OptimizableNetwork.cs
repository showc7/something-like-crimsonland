using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network
{
    public class OptimizableNetwork : GeneticAlgorithm
    {
        public Network [] Networks { get; set; }
        public OptimizableNetwork()
            : base()
        {

        }
        public OptimizableNetwork(IFiteness fitness)
            : base (fitness)
        {

        }
        public OptimizableNetwork(IFiteness fitness,Network [] networks)
            : base(fitness)
        {
            Networks = networks;
        }

        public new void InitializePopulation(Network[] networks, int input, int output, int layers, int perLayer, IFiteness fitness)
        {
            base.InitializePopulation(networks,input,output,layers,perLayer,fitness);
            this.Networks = networks;
        }

        public new void InitializePopulation(Network[] networks, int input, int output, int layers, int perLayer)
        {
            base.InitializePopulation(networks, input, output, layers, perLayer);
            this.Networks = networks;
        }

        public void Run(float [] input)
        {
            foreach(Network n in Networks)
            {
                n.Calculate(input);
            }
        }

        public void Mate()
        {
            this.Networks = base.Mate(this.Networks);
        }
    }
}
