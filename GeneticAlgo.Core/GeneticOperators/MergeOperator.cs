using GeneticAlgo.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgo.Core.GeneticOperators
{
    public class MergeOperator : IMergeOperator
    {
        private IRandomizer _randomizer;

        public MergeOperator(IRandomizer randomizer)
        {
            _randomizer = randomizer;
        }

        public void Apply(Genom oldGenom1, Genom oldGenom2)
        {
            int cutPlace1 = _randomizer.NextCutPlace(oldGenom1.ChromosomeLength);
            int cutPlace2 = _randomizer.NextCutPlace(oldGenom2.ChromosomeLength);
            var newChromosome1 = CountedArrayPoolDecorator<VectorGen>.Rent(cutPlace1 + oldGenom2.ChromosomeLength - cutPlace2);
            var newChromosome2 = CountedArrayPoolDecorator<VectorGen>.Rent(cutPlace2 + oldGenom1.ChromosomeLength - cutPlace1);
            for (int i = 0; i < cutPlace1; ++i)
            {
                newChromosome1[i] = oldGenom1.Chromosome[i].Clone();
            }

            for (int i = cutPlace2; i < oldGenom2.ChromosomeLength; ++i)
            {
                newChromosome1[cutPlace1 + i - cutPlace2] = oldGenom2.Chromosome[i].Clone();
            }

            for (int i = 0; i < cutPlace2; ++i)
            {
                newChromosome2[i] = oldGenom2.Chromosome[i].Clone();
            }

            for (int i = cutPlace1; i < oldGenom1.ChromosomeLength; ++i)
            {
                newChromosome2[cutPlace2 + i - cutPlace1] = oldGenom1.Chromosome[i].Clone();
            }

            CountedArrayPoolDecorator<VectorGen>.Return(oldGenom1.Chromosome);
            CountedArrayPoolDecorator<VectorGen>.Return(oldGenom2.Chromosome);

            int oldChromosomeLength1 = oldGenom1.ChromosomeLength;
            int oldChromosomeLength2 = oldGenom2.ChromosomeLength;

            oldGenom1.Chromosome = newChromosome1;
            oldGenom1.ChromosomeLength = cutPlace1 + oldChromosomeLength2 - cutPlace2;
            oldGenom2.Chromosome = newChromosome2;
            oldGenom2.ChromosomeLength = cutPlace2 + oldChromosomeLength1 - cutPlace1;
        }
    }
}
