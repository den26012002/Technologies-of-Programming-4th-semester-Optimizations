using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgo.Core.Tools
{
    public interface IRandomizer
    {
        double NextGenPart();
        int NextCutPlace(int maxValue);
        double NextSpinResult(double maxValue);
    }
}
