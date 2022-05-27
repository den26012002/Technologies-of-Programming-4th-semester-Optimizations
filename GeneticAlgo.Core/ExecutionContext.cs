using GeneticAlgo.Shared;
using GeneticAlgo.Shared.Models;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneticAlgo.Core.Tools;
using GeneticAlgo.Core.GeneticOperators;
using GeneticAlgo.Core.GeneticOperatorsBuilders;

namespace GeneticAlgo.Core
{
    public class ExecutionContext : IExecutionContext
    {
        private readonly Config _config;
        private readonly Randomizer _randomizer;
        private readonly IFitnessFunctionCalculator _fitnessFunctionCalculator;
        private readonly ISelectionOperator _selectionOperator;
        private readonly IMutationOperator _firstTypeMutationOperator;
        private readonly IMutationOperator _secondTypeMutationOperator;
        private readonly IMergeOperator _mergeOperator;
        public Genom[] _population;
        private Point[] _populationPoints;

        public ExecutionContext(
            Random random,
            IFitnessFunctionCalculatorBuilder fitnessFunctionCalculatorBuilder,
            ISelectionOperatorBuilder selectionOperatorBuilder,
            IMutationOperatorBuilder firstTypeMutationOperatorBuilder,
            IMutationOperatorBuilder secondTypeMutationOperatorBuilder,
            IMergeOperatorBuilder mergeOperatorBuilder)
        {
            string json = File.ReadAllText("../../../../GeneticAlgo.Core/config.json");
            _config = JsonConvert.DeserializeObject<Config>(json);
            _randomizer = new Randomizer(random, _config.MaxMove);

            fitnessFunctionCalculatorBuilder.AddBarrierCircles(_config.BarrierCircles.ToList());
            _fitnessFunctionCalculator = fitnessFunctionCalculatorBuilder.GetResult();

            selectionOperatorBuilder.AddRandomizer(_randomizer);
            firstTypeMutationOperatorBuilder.AddRandomizer(_randomizer);
            secondTypeMutationOperatorBuilder.AddRandomizer(_randomizer);
            mergeOperatorBuilder.AddRandomizer(_randomizer);

            _selectionOperator = selectionOperatorBuilder.GetResult();
            _firstTypeMutationOperator = firstTypeMutationOperatorBuilder.GetResult();
            _secondTypeMutationOperator = secondTypeMutationOperatorBuilder.GetResult();
            _mergeOperator = mergeOperatorBuilder.GetResult();

            _population = new Genom[_config.UnitCount];
            _populationPoints = new Point[_config.UnitCount];
            for (int i = 0; i < _config.UnitCount; ++i)
            {
                var genom = new Genom(1);
                genom.Chromosome[0] = new VectorGen(_randomizer.NextGenPart(), _randomizer.NextGenPart());
                Point resultPoint = new Point();
                genom.FitnessValue = _fitnessFunctionCalculator.Calculate(genom, out resultPoint);
                _populationPoints[i] = resultPoint;
                _population[i] = genom;
            }
        }

        public Task<IterationResult> ExecuteIterationAsync()
        {
            bool[] wasMutation = new bool[this._population.Length];
            _selectionOperator.Apply(_population);
            for (int i = 0; i < _population.Length / 3; ++i)
            {
                wasMutation[i] = true;
            }

            for (int i = 0; i < _population.Length; ++i)
            {
                double spinResult = _randomizer.NextSpinResult(1);
                if (spinResult <= _config.FirstTypeMutationProbability && !wasMutation[i])
                {
                    int sizeBefore = _population[i].ChromosomeLength;
                    _firstTypeMutationOperator.Apply(_population[i]);
                    int sizeAfter = _population[i].ChromosomeLength;
                    if (sizeBefore + 1 != sizeAfter)
                    {
                        Console.WriteLine("Warning: chromosome lenght wasn't increased");
                    }
                    /* Point resultPoint = new Point();
                     _population[i].FitnessValue = _fitnessFunctionCalculator.Calculate(_population[i], out resultPoint);*/
                    wasMutation[i] = true;
                }
            }

            for (int i = 0; i < _population.Length; ++i)
            {
                double spinResult = _randomizer.NextSpinResult(1);
                if (spinResult <= _config.SecondTypeMutationProbability && !wasMutation[i])
                {
                    _secondTypeMutationOperator.Apply(_population[i]);
                    wasMutation[i] = true;
                }
            }

            List<int> mergeUnitsPositions = new List<int>();
            for (int i = 0; i < _population.Length; ++i)
            {
                double spinResult = _randomizer.NextSpinResult(1);
                if (spinResult <= _config.MergeProbability && !wasMutation[i])
                {
                    mergeUnitsPositions.Add(i);
                    wasMutation[i] = true;
                }
            }

            for (int i = 0; i < mergeUnitsPositions.Count - 1; i += 2)
            {
                int first = mergeUnitsPositions[i];
                int second = mergeUnitsPositions[i + 1];
                _mergeOperator.Apply(_population[first], _population[second]);
            }

            _populationPoints = new Point[_config.UnitCount];
            for (int i = 0; i < _population.Length; ++i)
            {
                Point resultPoint = new Point();
                _population[i].FitnessValue = _fitnessFunctionCalculator.Calculate(_population[i], out resultPoint);
                _populationPoints[i] = resultPoint;
            }

            // this._population = _population;
            return Task.FromResult(IterationResult.IterationFinished);
        }

        public void ReportStatistics(IStatisticsConsumer statisticsConsumer)
        {
            Statistic[] statistics = new Statistic[_config.UnitCount];
            for (int i = 0; i < statistics.Length; ++i)
            {
                statistics[i] = new Statistic(i, _populationPoints[i], _population[i].FitnessValue);
            }

            statisticsConsumer.Consume(statistics, _config.BarrierCircles);
        }

        public void Reset()
        {
        }
    }
}
