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

        public (Genom newGenom1, Genom newGenom2) Apply(Genom oldGenom1, Genom oldGenom2)
        {
            int cutPlace1 = _randomizer.NextCutPlace(oldGenom1.Chromosome.Count);
            int cutPlace2 = _randomizer.NextCutPlace(oldGenom2.Chromosome.Count);
            Genom newGenom1 = new Genom();
            Genom newGenom2 = new Genom();
            for (int i = 0; i < cutPlace1; ++i)
            {
                newGenom1.Chromosome.Add(oldGenom1.Chromosome[i].Clone());
            }

            for (int i = cutPlace2; i < oldGenom2.Chromosome.Count; ++i)
            {
                newGenom1.Chromosome.Add(oldGenom2.Chromosome[i].Clone());
            }

            for (int i = 0; i < cutPlace2; ++i)
            {
                newGenom2.Chromosome.Add(oldGenom2.Chromosome[i].Clone());
            }

            for (int i = cutPlace1; i < oldGenom1.Chromosome.Count; ++i)
            {
                newGenom2.Chromosome.Add(oldGenom1.Chromosome[i].Clone());
            }

            return (newGenom1, newGenom2);
        }
    }
}
