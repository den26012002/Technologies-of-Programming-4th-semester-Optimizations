using GeneticAlgo.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgo.Core.GeneticOperators
{
    public class SelectionOperator : ISelectionOperator
    {
        private IRandomizer _randomizer;

        public SelectionOperator(IRandomizer randomizer)
        {
            _randomizer = randomizer;
        }
        public Genom[] Apply(Genom[] oldPopulation)
        {
            double[] prefixSums = new double[oldPopulation.Length];
            prefixSums[0] = oldPopulation[0].FitnessValue;
            for (int i = 1; i < oldPopulation.Length; ++i)
            {
                prefixSums[i] = prefixSums[i - 1] + oldPopulation[i].FitnessValue;
            }

            Genom[] newPopulation = new Genom[oldPopulation.Length];

            for (int i = 0; i < oldPopulation.Length / 2; ++i)
            {
                newPopulation[i] = GetGenomWithMaxFitnessValue(oldPopulation).Clone();
            }

            for (int i = oldPopulation.Length / 2; i < oldPopulation.Length; ++i)
            {
                double spinResult = _randomizer.NextSpinResult(prefixSums.Last());
                int spinnedGenom = Array.FindIndex(prefixSums, x => x > spinResult);
                // int spinResult = (int)prefixSums.ToArray().GetLowerBound(_randomizer.NextSpinResult(prefixSums.Last()));
                var genomClone = oldPopulation[spinnedGenom].Clone();
                newPopulation[i] = genomClone;
            }

            return newPopulation;
        }

        private Genom GetGenomWithMaxFitnessValue(Genom[] population)
        {
            Genom ans = population.First();
            foreach (var genom in population)
            {
                if (genom.FitnessValue > ans.FitnessValue)
                {
                    ans = genom;
                }
            }

            return ans;
        }
    }
}
