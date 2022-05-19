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

        public Genom Apply(Genom oldGenom)
        {
            var newGenom = oldGenom.Clone();
            newGenom.Chromosome.Add(new VectorGen(_randomizer.NextGenPart(), _randomizer.NextGenPart()));
            return newGenom;
        }
    }
}
