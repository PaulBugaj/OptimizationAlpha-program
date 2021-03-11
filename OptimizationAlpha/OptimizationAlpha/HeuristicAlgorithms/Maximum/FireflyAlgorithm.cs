using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OptimizationGlobals;
using System.Collections.ObjectModel;

namespace HeuristicAlgorithms
{
    class FireflyAlgorithm : Algorithm
    {
        //Variables====================================================================
        public class Firefly : IComparable<Firefly>
        {
            public FitnessPoint Position { get; set; }
            public double Attractiveness { get; set; }

            private LineRandom randomGenerator;

            public Firefly(params double[] arguments) : base()
            {
                this.randomGenerator = new LineRandom();

                this.Position = new FitnessPoint(arguments.ToList(), 0);
                this.Attractiveness = 1;
            }

            public Firefly(List<double> arguments) : base()
            {
                this.randomGenerator = new LineRandom();

                this.Position = new FitnessPoint(arguments, 0);
                this.Attractiveness = 1;
            }

            public int CompareTo(Firefly other)
            {
                if (this.Position.Fitness > other.Position.Fitness)
                    return 1;
                else if (this.Position.Fitness < other.Position.Fitness)
                    return -1;
                else
                    return 0;
            }
        }

        private List<Firefly> fireflys;

        private double alfa;
        private double absorptionCoefficientOfLight;

        public ReadOnlyCollection<Firefly> Fireflys { get { return this.fireflys.AsReadOnly(); } }

        //Constructors====================================================================
        public FireflyAlgorithm(Function function, int particlesCount, List<Compartment> ranges) : base(function, particlesCount, ranges)
        {
            this.algorithmType = AlgorithmType.Maximum;
            this.fireflys = new List<Firefly>();
            this.absorptionCoefficientOfLight = 1;
            this.alfa = 0.2;
            this.CreateAllPoints();
        }

        public FireflyAlgorithm() : base()
        {
            this.algorithmType = AlgorithmType.Maximum;
            this.absorptionCoefficientOfLight = 1;
            this.alfa = 0.2;
            this.fireflys = new List<Firefly>();
        }

        //Functions====================================================================
        private double CalculateDistanceBetweenFireflies(Firefly firefly1, Firefly firefly2)
        {
            double distanceSum = 0;
            for(int i=0; i<this.ranges.Count; i++)
            {
                distanceSum += Math.Pow(firefly1.Position.Axis.Values[i] - firefly2.Position.Axis.Values[i], 2);
            }
            return Math.Sqrt(distanceSum);
        }

        private double CalculateLightIntensity(Firefly firefly, double distance)
        {
            return firefly.Position.Fitness * Math.Exp(((-1) * absorptionCoefficientOfLight * distance * distance));
        }

        private double CalculateBeta(Firefly firefly, double distance)
        {
            return firefly.Attractiveness * Math.Exp(((-1) * absorptionCoefficientOfLight * distance * distance));
        }

        private void ResetIfOutOffRange(int index)
        {
            for (int j = 0; j < this.ranges.Count; j++)
            {
                if (this.fireflys[index].Position.Axis.Values[j] > this.ranges[j].Max
                    || this.fireflys[index].Position.Axis.Values[j] < this.ranges[j].Min)
                {
                    List<double> arguments = new List<double>();
                    for (int k = 0; k < this.function.ArgumentsSymbol.Count; k++)
                    {
                        arguments.Add(this.randomGenerator.NextDouble(this.ranges[k].Min, this.ranges[k].Max));
                    }
                    this.fireflys[index].Position.Axis.Values = arguments;
                    try
                    {
                        this.fireflys[index].Position.Fitness = this.function.Evaluate(this.fireflys[index].Position.Axis.Values);
                    }
                    catch
                    {
                        throw new AlgorithmException(AlgorithmExceptionType.FunctionNotExecuted);
                    }
                    break;
                }
            }
        }

        private void CreateAllPoints()
        {
            for (int i = 0; i < this.pointsCount; i++)
            {
                List<double> arguments = new List<double>();
                for (int j = 0; j < this.function.ArgumentsSymbol.Count; j++)
                {
                    arguments.Add(this.randomGenerator.NextDouble(this.ranges[j].Min, this.ranges[j].Max));
                }
                this.fireflys.Add(new Firefly(arguments));

                try
                {
                    this.fireflys[this.fireflys.Count - 1].Position.Fitness = this.function.Evaluate(this.fireflys[i].Position.Axis.Values);
                }
                catch
                {
                    throw new AlgorithmException(AlgorithmExceptionType.FunctionNotExecuted);
                }
            }
        }

        public override bool NextIteration()
        {
            if (this.function == null)
            {
                throw new AlgorithmException(AlgorithmExceptionType.ParametersNotSeted);
            }

            double lastBestFitness = this.fireflys.Max().Position.Fitness;

            for (int i = 0; i < this.pointsCount; i++)
            {
                for (int j = 0; j < this.pointsCount; j++)
                {
                    if (i == j)
                        continue;

                    double distance = CalculateDistanceBetweenFireflies(this.fireflys[i], this.fireflys[j]);
                    double lightIntensity1 = CalculateLightIntensity(this.fireflys[i], distance);
                    double lightIntensity2 = CalculateLightIntensity(this.fireflys[j], distance);
                    if (lightIntensity1 < lightIntensity2)
                    {
                        double attractiveness = CalculateBeta(this.fireflys[i], distance);
                        alfa = 0.2 * Math.Pow(this.randomGenerator.NextDouble(), this.acctualIteration);
                        if (attractiveness < 0.000000000001)
                        {
                            for (int k = 0; k < this.function.ArgumentsSymbol.Count; k++)
                            {
                                this.fireflys[i].Position.Axis.Values[k] += alfa * (this.randomGenerator.NextDouble() - 0.5);
                            }
                        }
                        else
                        {
                            for (int k = 0; k < this.function.ArgumentsSymbol.Count; k++)
                            {
                                this.fireflys[i].Position.Axis.Values[k] += (attractiveness * (this.fireflys[j].Position.Axis.Values[k] - this.fireflys[i].Position.Axis.Values[k])) +
                                    (alfa * (this.randomGenerator.NextDouble() - 0.5));
                            }
                        }
                        this.ResetIfOutOffRange(i);
                        try
                        {
                            this.fireflys[i].Position.Fitness = this.function.Evaluate(this.fireflys[i].Position.Axis.Values);
                        }
                        catch
                        {
                            throw new AlgorithmException(AlgorithmExceptionType.FunctionNotExecuted);
                        }
                    }
                }
            }


            if (Math.Abs(lastBestFitness - this.fireflys.Max().Position.Fitness) < 0.01)
            {
                this.canIEndIterator++;
                if (this.canIEndIterator == 20)
                {
                    return false;
                }
            }
            else
            {
                this.canIEndIterator = 0;
            }

            if (this.acctualIteration > 200)
            {
                return false;
            }

            this.acctualIteration++;
            return true;
        }

        public override void SetNewAndReset(Function function, int agentsCount, List<Compartment> ranges)
        {
            this.ranges = new List<Compartment>(ranges);
            this.function = function;
            this.pointsCount = agentsCount;

            this.Reset();
        }

        public override void Reset()
        {
            if (this.function == null)
            {
                throw new AlgorithmException(AlgorithmExceptionType.ParametersNotSeted);
            }
            else
            {
                this.alfa = 0.2;
                this.absorptionCoefficientOfLight = 1;
                this.acctualIteration = 1;
                this.canIEndIterator = 0;
                this.fireflys.Clear();
                this.CreateAllPoints();
            }
        }

        public override FitnessPoint GenerateBestValue()
        {
            if (this.function == null)
            {
                throw new AlgorithmException(AlgorithmExceptionType.ParametersNotSeted);
            }

            FitnessPoint result = null;
            while (true)
            {
                if (!this.NextIteration())
                {
                    result = this.fireflys.Max().Position;
                    break;
                }
            }

            this.Reset();
            return result;
        }

        public override async Task<FitnessPoint> GenerateBestValueAsync()
        {
            if (this.function == null)
            {
                throw new AlgorithmException(AlgorithmExceptionType.ParametersNotSeted);
            }

            FitnessPoint result = null;
            while (true)
            {
                bool isNotEnd = await Task.Run(() => this.NextIteration());
                if (!isNotEnd)
                {
                    result = this.fireflys.Max().Position;
                    break;
                }
            }

            this.Reset();
            return result;
        }
    }
}
