using GeneticAlgo.Core.GeneticOperators;
using GeneticAlgo.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgo.Core.GeneticOperatorsBuilders
{
    public interface ISelectionOperatorBuilder
    {
        void AddRandomizer(IRandomizer randomizer);
        ISelectionOperator GetResult();
        void Clear();
    }
}
