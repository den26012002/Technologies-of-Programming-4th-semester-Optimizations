using GeneticAlgo.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgo.Core.GeneticOperators
{
    public class SecondTypeMutationOperator : IMutationOperator
    {
        public IRandomizer _randomizer;

        public SecondTypeMutationOperator(IRandomizer randomizer)
        {
            _randomizer = randomizer;
        }

        public Genom Apply(Genom oldGenom)
        {
            int position = _randomizer.NextCutPlace(oldGenom.Chromosome.Count);
            oldGenom.Chromosome[position] = new VectorGen(_randomizer.NextGenPart(), _randomizer.NextGenPart());
            return oldGenom;
        }
    }
}
