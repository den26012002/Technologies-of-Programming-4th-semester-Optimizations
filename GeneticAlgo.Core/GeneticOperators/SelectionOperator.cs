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
        public List<Genom> Apply(List<Genom> oldPopulation)
        {
            double[] prefixSums = new double[oldPopulation.Count];
            prefixSums[0] = oldPopulation[0].FitnessValue;
            for (int i = 1; i < oldPopulation.Count; ++i)
            {
                prefixSums[i] = prefixSums[i - 1] + oldPopulation[i].FitnessValue;
            }

            List<Genom> newPopulation = new List<Genom>();

            for (int i = 0; i < oldPopulation.Count / 2; ++i)
            {
                newPopulation.Add(GetGenomWithMaxFitnessValue(oldPopulation).Clone());
            }

            for (int i = newPopulation.Count; i < oldPopulation.Count; ++i)
            {
                double spinResult = _randomizer.NextSpinResult(prefixSums.Last());
                int spinnedGenom = Array.FindIndex(prefixSums, x => x > spinResult);
                // int spinResult = (int)prefixSums.ToArray().GetLowerBound(_randomizer.NextSpinResult(prefixSums.Last()));
                var genomClone = oldPopulation[spinnedGenom].Clone();
                newPopulation.Add(genomClone);
            }

            return newPopulation;
        }

        private Genom GetGenomWithMaxFitnessValue(List<Genom> population)
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
