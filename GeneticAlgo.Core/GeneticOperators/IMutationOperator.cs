using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgo.Core.GeneticOperators
{
    public interface IMutationOperator
    {
        void Apply(Genom oldGenom);
    }
}
