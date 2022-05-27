using GeneticAlgo.Core.GeneticOperators;
using GeneticAlgo.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgo.Core.GeneticOperatorsBuilders
{
    public interface IMutationOperatorBuilder
    {
        void AddRandomizer(IRandomizer randomizer);
        IMutationOperator GetResult();
        void Clear();
    }
}
