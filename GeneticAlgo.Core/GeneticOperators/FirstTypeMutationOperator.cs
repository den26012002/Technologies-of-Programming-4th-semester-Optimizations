using GeneticAlgo.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgo.Core.GeneticOperators
{
    public class FirstTypeMutationOperator : IMutationOperator
    {
        public IRandomizer _randomizer;

        public FirstTypeMutationOperator(IRandomizer randomizer)
        {
            _randomizer = randomizer;
        }

        public void Apply(Genom oldGenom)
        {
            VectorGen[] newChromosome = CountedArrayPoolDecorator<VectorGen>.Rent(oldGenom.ChromosomeLength + 1);
            for (int i = 0; i < oldGenom.ChromosomeLength; ++i)
            {
                newChromosome[i] = oldGenom.Chromosome[i].Clone();
            }
            newChromosome[oldGenom.ChromosomeLength] = new VectorGen(_randomizer.NextGenPart(), _randomizer.NextGenPart());

            CountedArrayPoolDecorator<VectorGen>.Return(oldGenom.Chromosome);
            oldGenom.Chromosome = newChromosome;
            oldGenom.ChromosomeLength = oldGenom.ChromosomeLength + 1;

            // return oldGenom;
        }
    }
}
