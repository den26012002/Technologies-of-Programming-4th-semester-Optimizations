using GeneticAlgo.Core.GeneticOperators;
using GeneticAlgo.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgo.Core.GeneticOperatorsBuilders
{
    public class SelectionOperatorBuilder : ISelectionOperatorBuilder
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

        public ISelectionOperator GetResult()
        {
            if (_randomizer != null)
            {
                return new SelectionOperator(_randomizer);
            }

            return null;
        }
    }
}
