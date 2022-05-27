using GeneticAlgo.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgo.Core.Tools
{
    public class FitnessFunctionCalculatorBuilder : IFitnessFunctionCalculatorBuilder
    {
        private List<BarrierCircle> _barrierCircles;
        public void AddBarrierCircles(List<BarrierCircle> barrierCircles)
        {
            _barrierCircles = barrierCircles;
        }

        public IFitnessFunctionCalculator GetResult()
        {
            return new FitnessFunctionCalculator(_barrierCircles);
        }
    }
}
