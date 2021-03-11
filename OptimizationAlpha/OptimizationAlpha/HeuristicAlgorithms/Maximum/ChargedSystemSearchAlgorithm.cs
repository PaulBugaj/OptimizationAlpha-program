using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OptimizationGlobals;
using System.Collections.ObjectModel;

namespace HeuristicAlgorithms
{
    class ChargedSystemSearchAlgorithm : Algorithm
    {
        //Variables====================================================================
        public class Particle : IComparable<Particle>
        {
            public FitnessPoint Position { get; set; }
            public Vector Velocity { get; set; }
            public double ElectricCharge { get; set; }
            public Vector Force { get; set; }

            private LineRandom randomGenerator;

            public Particle(params double[] arguments) : base()
            {
                this.randomGenerator = new LineRandom();

                this.Position = new FitnessPoint(arguments.ToList(), 0);
                this.Velocity = new Vector(arguments.Length);
                this.Force = new Vector(arguments.Length);
                this.ElectricCharge = 0;
            }

            public Particle(List<double> arguments) : base()
            {
                this.randomGenerator = new LineRandom();

                this.Position = new FitnessPoint(arguments, 0);
                this.Velocity = new Vector(arguments.Count);
                this.Force = new Vector(arguments.Count);
                this.ElectricCharge = 0;
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
        private double particleDiameter;
        private const double constIterations = 200;

        public ReadOnlyCollection<Particle> ChargedMolecules { get { return this.particles.AsReadOnly(); } }

        //Constructors====================================================================
        public ChargedSystemSearchAlgorithm(Function function, int particlesCount, List<Compartment> ranges) : base(function, particlesCount, ranges)
        {
            this.algorithmType = AlgorithmType.Maximum;
            this.particleDiameter = 1;

            this.particles = new List<Particle>();
            this.CreateAllPoints();
        }

        public ChargedSystemSearchAlgorithm() : base()
        {
            this.algorithmType = AlgorithmType.Maximum;
            this.particleDiameter = 1;
            this.particles = new List<Particle>();
        }

        //Functions====================================================================
        private double DistanceBetweenParticles(Particle particle1, Particle particle2)
        {
            double distanceSum = 0;
            for (int i = 0; i < this.ranges.Count; i++)
            {
                distanceSum += Math.Pow(particle1.Position.Axis.Values[i] - particle2.Position.Axis.Values[i], 2);
            }
            return Math.Sqrt(distanceSum);
        }

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

            double lastBestFitness = this.particles.Max().Position.Fitness;

            for (int i = 0; i < this.pointsCount; i++)
            {
                this.particles[i].ElectricCharge = (this.particles[i].Position.Fitness - this.particles.Min().Position.Fitness) / (this.particles.Max().Position.Fitness - this.particles.Min().Position.Fitness);
            }
            double[] helpForce = new double[this.function.ArgumentsSymbol.Count];
            int probability;
            int alfa;
            int beta;

            for (int i = 0; i < this.pointsCount; i++)
            {
                for (int j = 0; j < this.function.ArgumentsSymbol.Count; j++)
                {
                    helpForce[j] = 0;
                }
                for (int j = 0; j < this.pointsCount; j++)
                {
                    if (i == j)
                        continue;


                    if (((this.particles[i].Position.Fitness - this.particles.Max().Position.Fitness) / (this.particles[j].Position.Fitness - this.particles[i].Position.Fitness) > this.randomGenerator.NextDouble())
                        || (this.particles[j].Position.Fitness > this.particles[i].Position.Fitness))
                        probability = 1;
                    else
                        probability = 0;

                    double distanceBetweenParticles = DistanceBetweenParticles(this.particles[i], this.particles[j]);

                    if (distanceBetweenParticles < this.particleDiameter)
                    {
                        alfa = 1;
                        beta = 0;
                    }
                    else
                    {
                        alfa = 0;
                        beta = 1;
                    }

                    for(int k=0; k<this.function.ArgumentsSymbol.Count; k++)
                    {
                        helpForce[k] += ((alfa * this.particles[j].ElectricCharge / Math.Pow(this.particleDiameter, 3) * distanceBetweenParticles)
                                + (beta * this.particles[j].ElectricCharge / Math.Pow(distanceBetweenParticles, 2)))
                                * this.particleDiameter * distanceBetweenParticles * probability * (this.particles[j].Position.Axis.Values[k] - this.particles[i].Position.Axis.Values[k]);
                    }
                }

                double[] old_i_Position = new double[this.function.ArgumentsSymbol.Count];
                for(int j=0; j<this.function.ArgumentsSymbol.Count; j++)
                {
                    this.particles[i].Force.Values[j] = this.particles[i].ElectricCharge * helpForce[j];
                    old_i_Position[j] = this.particles[i].Position.Axis.Values[j];
                    this.particles[i].Position.Axis.Values[j] = (this.particles[i].Position.Axis.Values[j] + 0.5 * (1 - this.acctualIteration / constIterations) * this.randomGenerator.NextDouble() * this.particles[i].Velocity.Values[j])
                    + (0.5 * this.randomGenerator.NextDouble() * (1 + this.acctualIteration / constIterations) * this.particles[i].Force.Values[j]);
                }

                try
                {
                    this.particles[i].Position.Fitness = this.function.Evaluate(this.particles[i].Position.Axis.Values);
                }
                catch
                {
                    throw new AlgorithmException(AlgorithmExceptionType.FunctionNotExecuted);
                }

                this.ResetIfOutOffRange(i);

                for (int j = 0; j < this.function.ArgumentsSymbol.Count; j++)
                {
                    this.particles[i].Velocity.Values[j] = this.particles[i].Position.Axis.Values[j] - old_i_Position[j];
                }
            }

            if (Math.Abs(lastBestFitness - this.particles.Max().Position.Fitness) < 0.01)
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
                this.particleDiameter = 1;
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
                    result = this.particles.Max().Position;
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
                    result = this.particles.Max().Position;
                    break;
                }
            }

            this.Reset();
            return result;
        }
    }
}
