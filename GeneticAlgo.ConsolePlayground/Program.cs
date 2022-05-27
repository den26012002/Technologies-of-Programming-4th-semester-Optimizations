/*// See https://aka.ms/new-console-template for more information

using GeneticAlgo.Shared;
using GeneticAlgo.Shared.Tools;
using Serilog;

Logger.Init();
Log.Information("Start console polygon");
var dummyExecutionContext = new DummyExecutionContext(10, 10, 3);
dummyExecutionContext.Reset();
await dummyExecutionContext.ExecuteIterationAsync();
Log.Information("Polygon end");

Console.WriteLine("Hello, World!");
*/

/*var executionContext = new GeneticAlgo.Core.ExecutionContext(10);
Console.WriteLine(executionContext.ToString());*/

using GeneticAlgo.Core;
using GeneticAlgo.Core.GeneticOperatorsBuilders;
using GeneticAlgo.Core.Tools;
using GeneticAlgo.Shared.Models;

/*List<Genom> genoms = new List<Genom>();
genoms.Add(new Genom());
List<Genom> genoms2 = genoms.ToList();
genoms[0].Chromosome.Add(new VectorGen(10, 20));
Console.WriteLine(genoms2[0].Chromosome[0].Y);*/

static void Print(GeneticAlgo.Core.ExecutionContext executionContext, bool needFullLogs = true)
{
    double fitnessSum = 0;
    double maxFitnessValue = executionContext._population.First().FitnessValue;
    double minFitnessValue = executionContext._population.First().FitnessValue;
    foreach (var unit in executionContext._population)
    {
        if (needFullLogs)
        {
            for (int i = 0; i < unit.ChromosomeLength; ++i)
            {
                Console.Write($"-{{{string.Format("{0:f1}", unit.Chromosome[i].X)}, {string.Format("{0:f1}", unit.Chromosome[i].Y)}}}");
            }

            Console.WriteLine($" {string.Format("{0:f3}", unit.FitnessValue)}");
        }
        fitnessSum += unit.FitnessValue;
        maxFitnessValue = Math.Max(maxFitnessValue, unit.FitnessValue);
        minFitnessValue = Math.Min(minFitnessValue, unit.FitnessValue);
    }
    Console.WriteLine($"Fitness sum: {fitnessSum}, min: {minFitnessValue}, max: {maxFitnessValue}");
}


GeneticAlgo.Core.ExecutionContext executionContext = new GeneticAlgo.Core.ExecutionContext(
    new Random(),
    new FitnessFunctionCalculatorBuilder(),
    new SelectionOperatorBuilder(),
    new FirstTypeMutationOperatorBuilder(),
    new SecondTypeMutationOperatorBuilder(),
    new MergeOperatorBuilder());
//Print(executionContext);

for (int i = 0; i < 100; i++)
{
    executionContext.ExecuteIterationAsync();
    // Console.WriteLine(i);
    // Print(executionContext, false);
    //Console.Write($" {i}");
}

// Print(executionContext);
// CountedArrayPoolDecorator<VectorGen>.PrintSizes();

/*var executionContext = new GeneticAlgo.Core.ExecutionContext(
    new Random(),
    new FitnessFunctionCalculatorBuilder(),
    new SelectionOperatorBuilder(),
    new FirstTypeMutationOperatorBuilder(),
    new SecondTypeMutationOperatorBuilder(),
    new MergeOperatorBuilder());

IterationResult iterationResult;
do
{
    iterationResult = await executionContext.ExecuteIterationAsync();
    // Print(executionContext, false);
}
while (iterationResult == IterationResult.IterationFinished);*/