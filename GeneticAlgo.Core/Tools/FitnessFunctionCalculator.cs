using GeneticAlgo.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgo.Core.Tools
{
    public class FitnessFunctionCalculator : IFitnessFunctionCalculator
    {
        private List<BarrierCircle> _barrierCircles;

        public FitnessFunctionCalculator(List<BarrierCircle> barrierCircles)
        {
            _barrierCircles = barrierCircles;
        }

        public double Calculate(Genom genom, out Point resultPoint)
        {
            int barrierCirclesCount = 0;
            var position = new Point(0, 0);
            for (int i = 0; i < genom.ChromosomeLength; ++i)
            {
                position = new Point(position.X + genom.Chromosome[i].X, position.Y + genom.Chromosome[i].Y);
                foreach (var circle in _barrierCircles)
                {
                    if (Distance(position, circle.Center) < circle.Radius)
                    {
                        ++barrierCirclesCount;
                    }
                }
            }

            resultPoint = position;
            return 1.0 / (0.4 * 20 * barrierCirclesCount + 0.5 * 1000 * Distance(position, new Point(1, 1)) + 0.1 * genom.ChromosomeLength);
            // return Math.Max(1.0 * _barrierCircles.Count - Distance(position, new Point(1, 1)) / Math.Sqrt(2) - 0.05 * barrierCirclesCount - 0.01 * genom.Chromosome.Count, 0.0001);
        }

        private double Distance(Point point1, Point point2)
        {
            return Math.Sqrt(Math.Pow(point1.X - point2.X, 2) + Math.Pow(point1.Y - point2.Y, 2));
        }
    }
}
