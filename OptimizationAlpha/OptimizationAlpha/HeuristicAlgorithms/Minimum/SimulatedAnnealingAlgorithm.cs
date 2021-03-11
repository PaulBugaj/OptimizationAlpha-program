using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OptimizationGlobals;
using System.Collections.ObjectModel;

namespace HeuristicAlgorithms
{
    class SimulatedAnnealingAlgorithm : Algorithm
    {
        //Variables====================================================================
        public class SAobject : IComparable<SAobject>
        {
            public FitnessPoint Position { get; set; }
            public Vector Velocity { get; set; }

            private LineRandom randomGenerator;

            public SAobject(params double[] arguments) : base()
            {
                this.randomGenerator = new LineRandom();

                this.Position = new FitnessPoint(arguments.ToList(), 0);
                this.Velocity = new Vector(arguments.Length);
            }

            public SAobject(List<double> arguments) : base()
            {
                this.randomGenerator = new LineRandom();

                this.Position = new FitnessPoint(arguments, 0);
                this.Velocity = new Vector(arguments.Count);
            }

            public int CompareTo(SAobject other)
            {
                if (this.Position.Fitness > other.Position.Fitness)
                    return 1;
                else if (this.Position.Fitness < other.Position.Fitness)
                    return -1;
                else
                    return 0;
            }
        }

        private List<SAobject> SAobjects;
        private  const double alpha = 0.72;
        private double T;

        public ReadOnlyCollection<SAobject> SAObjects { get { return this.SAobjects.AsReadOnly(); } }

        //Constructors====================================================================
        public SimulatedAnnealingAlgorithm(Function function, int particlesCount, List<Compartment> ranges) : base(function, particlesCount, ranges)
        {
            this.algorithmType = AlgorithmType.Minimum;
            this.T = 100;
            this.SAobjects = new List<SAobject>();
            this.CreateAllPoints();
        }

        public SimulatedAnnealingAlgorithm() : base()
        {
            this.algorithmType = AlgorithmType.Minimum;
            this.T = 100;
            this.SAobjects = new List<SAobject>();
        }

        //Functions====================================================================
        private void ResetIfOutOffRange(int index)
        {
            for (int j = 0; j < this.ranges.Count; j++)
            {
                if (this.SAobjects[index].Position.Axis.Values[j] > this.ranges[j].Max
                    || this.SAobjects[index].Position.Axis.Values[j] < this.ranges[j].Min)
                {
                    List<double> arguments = new List<double>();
                    for (int k = 0; k < this.function.ArgumentsSymbol.Count; k++)
                    {
                        arguments.Add(this.randomGenerator.NextDouble(this.ranges[k].Min, this.ranges[k].Max));
                    }
                    this.SAobjects[index].Position.Axis.Values = arguments;
                    try
                    {
                        this.SAobjects[index].Position.Fitness = this.function.Evaluate(this.SAobjects[index].Position.Axis.Values);
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
                this.SAobjects.Add(new SAobject(arguments));

                try
                {
                    this.SAobjects[this.SAobjects.Count - 1].Position.Fitness = this.function.Evaluate(this.SAobjects[i].Position.Axis.Values);
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

            double globalOldBestFitness = this.SAobjects.Min().Position.Fitness;

            for (int i = 0; i < this.SAobjects.Count; i++)
            {
                List<double> newPosition = new List<double>();
                for (int j = 0; j < this.function.ArgumentsSymbol.Count; j++)
                {
                    newPosition.Add(this.SAobjects[i].Position.Axis.Values[j] + this.randomGenerator.NextDouble(-1, 1));
                }

                double newFitness;
                try
                {
                    newFitness = this.function.Evaluate(newPosition);
                }
                catch
                {
                    throw new AlgorithmException(AlgorithmExceptionType.FunctionNotExecuted);
                }
                double D_F = newFitness - this.SAobjects[i].Position.Fitness;

                if (D_F < 0)
                {
                    this.SAobjects[i].Position.Axis.Values = newPosition;
                    this.SAobjects[i].Position.Fitness = newFitness;

                    this.ResetIfOutOffRange(i);
                }
                else
                {
                    double r = this.randomGenerator.NextDouble();
                    if (r < Math.Exp(-D_F / this.T))
                    {
                        this.SAobjects[i].Position.Axis.Values = newPosition;
                        this.SAobjects[i].Position.Fitness = newFitness;

                        this.ResetIfOutOffRange(i);
                    }
                }
            }

            this.T = this.T * alpha;
            if(this.T < 10e-6)
            {
                return false;
            }

            if (Math.Abs(globalOldBestFitness - this.SAobjects.Min().Position.Fitness) < 0.01)
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
                this.T = 100;
                this.acctualIteration = 1;
                this.canIEndIterator = 0;
                this.SAobjects.Clear();
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
                    result = this.SAobjects.Min().Position;
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
                    result = this.SAobjects.Min().Position;
                    break;
                }
            }

            this.Reset();
            return result;
        }
    }
}
