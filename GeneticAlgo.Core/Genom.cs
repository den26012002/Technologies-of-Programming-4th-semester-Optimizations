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
        public int ChromosomeLength { get; set; }
        public VectorGen[] Chromosome { get; set; } = null;
        public double FitnessValue { get; set; }

        public Genom(int chromosomeLength)
        {
            Chromosome = CountedArrayPoolDecorator<VectorGen>.Rent(chromosomeLength);
            ChromosomeLength = chromosomeLength;
        }

        public Genom Clone()
        {
            Genom newGenom = new Genom(ChromosomeLength);
            for (int i = 0; i < ChromosomeLength; ++i)
            {
                newGenom.Chromosome[i] = Chromosome[i].Clone();
            }

            newGenom.FitnessValue = FitnessValue;
            return newGenom;
        }

        public VectorGen[] GetClonedChromosome()
        {
            VectorGen[] newChromosome = CountedArrayPoolDecorator<VectorGen>.Rent(ChromosomeLength);
            for (int i = 0; i < ChromosomeLength; ++i)
            {
                newChromosome[i] = Chromosome[i].Clone();
            }

            return newChromosome;
        }
    }
}
