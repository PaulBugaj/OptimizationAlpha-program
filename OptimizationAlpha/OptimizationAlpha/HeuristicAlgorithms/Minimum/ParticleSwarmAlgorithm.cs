using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OptimizationGlobals;
using System.Collections.ObjectModel;

namespace HeuristicAlgorithms
{
    class ParticleSwarmAlgorithm : Algorithm
    {
        //Variables====================================================================
        public class Particle : IComparable<Particle>
        {
            public FitnessPoint Position { get; set; }
            public Vector Velocity { get; set; }
            public Vector LifeBest { get; set; }

            private LineRandom randomGenerator;

            public Particle(params double[] arguments) : base()
            {
                this.randomGenerator = new LineRandom();

                this.Position = new FitnessPoint(arguments.ToList(), 0);
                this.Velocity = new Vector(arguments.Length);
                this.LifeBest = new Vector(arguments.Length);
            }

            public Particle(List<double> arguments) : base()
            {
                this.randomGenerator = new LineRandom();

                this.Position = new FitnessPoint(arguments, 0);
                this.Velocity = new Vector(arguments.Count);
                this.LifeBest = new Vector(arguments.Count);
            }

            public int CompareTo(Particle other)
            {
                if (this.Position.Fitness > other.Position.Fitness)
                    return 1;
                else if (this.Position.Fitness < other.Position.Fitness)
                    return -1;
                else
                    return 0;
            }
        }

        private List<Particle> particles;
        private const double alfa = 0.2;
        private const double beta = 0.2;

        public ReadOnlyCollection<Particle> Particles { get { return this.particles.AsReadOnly(); } }

        //Constructors====================================================================
        public ParticleSwarmAlgorithm(Function function, int particlesCount, List<Compartment> ranges) : base(function, particlesCount, ranges)
        {
            this.algorithmType = AlgorithmType.Minimum;
            this.particles = new List<Particle>();
            this.CreateAllPoints();
        }

        public ParticleSwarmAlgorithm() : base()
        {
            this.algorithmType = AlgorithmType.Minimum;
            this.particles = new List<Particle>();
        }

        //Functions====================================================================
        private void ResetIfOutOffRange(int index)
        {
            for (int j = 0; j < this.ranges.Count; j++)
            {
                if (this.particles[index].Position.Axis.Values[j] > this.ranges[j].Max
                    || this.particles[index].Position.Axis.Values[j] < this.ranges[j].Min)
                {
                    List<double> arguments = new List<double>();
                    for (int k = 0; k < this.function.ArgumentsSymbol.Count; k++)
                    {
                        arguments.Add(this.randomGenerator.NextDouble(this.ranges[k].Min, this.ranges[k].Max));
                    }
                    this.particles[index].Position.Axis.Values = arguments;
                    try
                    {
                        this.particles[index].Position.Fitness = this.function.Evaluate(this.particles[index].Position.Axis.Values);
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
                this.particles.Add(new Particle(arguments));
                this.particles[this.particles.Count - 1].LifeBest = this.particles[this.particles.Count - 1].Position.Axis;

                try
                {
                    this.particles[this.particles.Count - 1].Position.Fitness = this.function.Evaluate(this.particles[i].Position.Axis.Values);
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

            double globalOldBestFitness = this.particles.Min().Position.Fitness;

            for (int i = 0; i < this.pointsCount; i++)
            {
                double oldFitness = this.particles[i].Position.Fitness;

                for (int j = 0; j < this.function.ArgumentsSymbol.Count; j++)
                {
                    this.particles[i].Velocity.Values[j] = (this.randomGenerator.NextDouble() * this.particles[i].Velocity.Values[j]) + (alfa * this.randomGenerator.NextDouble() * (this.particles.Min().Position.Axis.Values[j] - this.particles[i].Position.Axis.Values[j])) +
                        beta * this.randomGenerator.NextDouble() * (this.particles[i].LifeBest.Values[j] - this.particles[i].Position.Axis.Values[j]);

                    this.particles[i].Position.Axis.Values[j] += this.particles[i].Velocity.Values[j];
                }

                try
                {
                    this.particles[i].Position.Fitness = this.function.Evaluate(this.particles[i].Position.Axis.Values);
                }
                catch
                {
                    throw new AlgorithmException(AlgorithmExceptionType.FunctionNotExecuted);
                }

                ResetIfOutOffRange(i);

                if (this.particles[i].Position.Fitness < oldFitness)
                {
                    this.particles[i].LifeBest = this.particles[i].Position.Axis;
                }
            }

            if (Math.Abs(globalOldBestFitness - this.particles.Min().Position.Fitness) < 0.01)
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
                this.acctualIteration = 1;
                this.canIEndIterator = 0;
                this.particles.Clear();
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
                    result = this.particles.Min().Position;
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
                    result = this.particles.Min().Position;
                    break;
                }
            }

            this.Reset();
            return result;
        }
    }
}
