using GeneticAlgo.Core.GeneticOperators;
using GeneticAlgo.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgo.Core.GeneticOperatorsBuilders
{
    public class MergeOperatorBuilder : IMergeOperatorBuilder
    {
        private IRandomizer _randomizer = null;

        public void AddRandomizer(IRandomizer randomizer)
        {
            _randomizer = randomizer;
        }

        public void Clear()
        {
            _randomizer = null;
        }

        public IMergeOperator GetResult()
        {
            if (_randomizer != null)
            {
                return new MergeOperator(_randomizer);
            }

            return null;
        }
    }
}
