using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgo.Core.GeneticOperators
{
    public interface IMergeOperator
    {
        public void Apply(Genom oldGenom1, Genom oldGenom2);
    }
}
