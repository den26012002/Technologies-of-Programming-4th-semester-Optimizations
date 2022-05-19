using GeneticAlgo.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgo.Core.Tools
{
    public interface IFitnessFunctionCalculatorBuilder
    {
        void AddBarrierCircles(List<BarrierCircle> barrierCircles);

        IFitnessFunctionCalculator GetResult();
    }
}
