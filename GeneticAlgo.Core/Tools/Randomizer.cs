using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgo.Core.Tools
{
    public class Randomizer : IRandomizer
    {
        private readonly Random _random;
        private readonly double _maxValue;
        public Randomizer(Random random, double maxValue)
        {
            _random = random;
            _maxValue = maxValue;
        }

        public int NextCutPlace(int maxValue)
        {
            return _random.Next(maxValue);
        }

        public double NextGenPart()
        {
            return _random.NextDouble() * _maxValue;
        }

        public double NextSpinResult(double maxValue)
        {
            return _random.NextDouble() * maxValue;
        }
    }
}
