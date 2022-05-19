using GeneticAlgo.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgo.Core
{
    public class Genom : ICloneable<Genom>
    {
        public List<VectorGen> Chromosome { get; set; } = new List<VectorGen>();
        public double FitnessValue { get; set; }

        public Genom Clone()
        {
            Genom newGenom = new Genom();
            foreach (var gen in Chromosome)
            {
                newGenom.Chromosome.Add(gen.Clone());
            }

            newGenom.FitnessValue = FitnessValue;
            return newGenom;
        }
    }
}
