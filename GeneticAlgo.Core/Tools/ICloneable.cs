using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgo.Core.Tools
{
    public interface ICloneable<Type>
    {
        Type Clone();
    }
}
