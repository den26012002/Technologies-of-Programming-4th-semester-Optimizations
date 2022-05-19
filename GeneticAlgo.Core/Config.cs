using GeneticAlgo.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgo.Core
{
    public class Config
    {
        private List<BarrierCircle> _barrierCircles = new List<BarrierCircle>();
        public double Accuracy { get; init; }
        public int UnitCount { get; init; }
        public double MaxMove { get; init; }
        public double FirstTypeMutationProbability { get; init; }
        public double SecondTypeMutationProbability { get; init; }
        public double MergeProbability { get; init; }
        public IReadOnlyList<BarrierCircle> BarrierCircles => _barrierCircles;
    }
}
