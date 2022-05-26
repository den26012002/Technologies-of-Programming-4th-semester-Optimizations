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
        public void Apply(Genom[] oldPopulation)
        {
            double[] prefixSums = new double[oldPopulation.Length];
            prefixSums[0] = oldPopulation[0].FitnessValue;
            for (int i = 1; i < oldPopulation.Length; ++i)
            {
                prefixSums[i] = prefixSums[i - 1] + oldPopulation[i].FitnessValue;
            }

            // Genom[] newPopulation = new Genom[oldPopulation.Length];
            VectorGen[][] newChromosomes = new VectorGen[oldPopulation.Length][];
            int[] newChromosomesLengths = new int[oldPopulation.Length];

            for (int i = 0; i < oldPopulation.Length / 2; ++i)
            {
                Genom genomWithMaxFitnessValue = GetGenomWithMaxFitnessValue(oldPopulation);
                newChromosomes[i] = genomWithMaxFitnessValue.GetClonedChromosome();
                newChromosomesLengths[i] = genomWithMaxFitnessValue.ChromosomeLength;
            }

            for (int i = oldPopulation.Length / 2; i < oldPopulation.Length; ++i)
            {
                double spinResult = _randomizer.NextSpinResult(prefixSums.Last());
                int spinnedGenom = Array.FindIndex(prefixSums, x => x > spinResult);
                newChromosomes[i] = oldPopulation[spinnedGenom].GetClonedChromosome();
                newChromosomesLengths[i] = oldPopulation[spinnedGenom].ChromosomeLength;
            }

            for (int i = 0; i < oldPopulation.Length; ++i)
            {
                CountedArrayPoolDecorator<VectorGen>.Return(oldPopulation[i].Chromosome);
                oldPopulation[i].Chromosome = newChromosomes[i];
                oldPopulation[i].ChromosomeLength = newChromosomesLengths[i];
            }

            // return oldPopulation;
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
