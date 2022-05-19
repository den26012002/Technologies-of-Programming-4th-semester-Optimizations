using GeneticAlgo.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgo.Core
{
    public class VectorGen : ICloneable<VectorGen>
    {
        public VectorGen(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X { get; init; }
        public double Y { get; init; }

        public VectorGen Clone()
        {
            return new VectorGen(X, Y);
        }
    }
}
