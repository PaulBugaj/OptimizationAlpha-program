using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OptimizationGlobals;
using System.Collections.ObjectModel;

namespace HeuristicAlgorithms
{
    class GlowwormAlgorithm : Algorithm
    {
        //Variables====================================================================
        public class Glowworm : IComparable<Glowworm>
        {
            public FitnessPoint Position { get; set; }
            public List<double> SensorRange { get; set; }
            public double Luciferin { get; set; }
            public double MoveProbability { get; set; }


            private LineRandom randomGenerator;

            public Glowworm(params double[] arguments) : base()
            {
                this.randomGenerator = new LineRandom();

                this.Position = new FitnessPoint(arguments.ToList(), 0);
                this.SensorRange = new List<double>();
                this.Luciferin = 0;
                this.MoveProbability = 0;
            }

            public Glowworm(List<double> arguments) : base()
            {
                this.randomGenerator = new LineRandom();

                this.Position = new FitnessPoint(arguments, 0);
                this.SensorRange = new List<double>();
                this.Luciferin = 0;
                this.MoveProbability = 0;
            }

            public int CompareTo(Glowworm other)
            {
                if (this.Position.Fitness > other.Position.Fitness)
                    return 1;
                else if (this.Position.Fitness < other.Position.Fitness)
                    return -1;
                else
                    return 0;
            }
        }

        private List<Glowworm> glowworms;
        private double alpha;
        private double beta;
        private double gamma;
        private List<double> s;
        private double n_t;
        private List<double> r_max;

        public ReadOnlyCollection<Glowworm> Glowworms { get { return this.glowworms.AsReadOnly(); } }

        //Constructors====================================================================
        public GlowwormAlgorithm(Function function, int particlesCount, List<Compartment> ranges) : base(function, particlesCount, ranges)
        {
            this.algorithmType = AlgorithmType.Maximum;
            this.glowworms = new List<Glowworm>();
            this.s = new List<double>();
            this.r_max = new List<double>();

            this.alpha = 0.4;
            this.beta = 0.08;
            this.gamma = 0.6;
            this.n_t = 5;

            for (int i = 0; i < this.ranges.Count; i++)
            {
                this.r_max.Add((2 * Math.Abs(this.ranges[i].Max - this.ranges[i].Min)) / (double)3);
                this.s.Add(Math.Abs(this.ranges[i].Max - this.ranges[i].Min) / (double)(Math.Pow(Math.Abs(this.ranges[i].Max - this.ranges[i].Min), 2) + 2));
            }

            this.CreateAllPoints();
        }

        public GlowwormAlgorithm() : base()
        {
            this.algorithmType = AlgorithmType.Maximum;
            this.glowworms = new List<Glowworm>();
            this.s = new List<double>();
            this.r_max = new List<double>();
        }

        //Functions====================================================================
        private void ResetIfOutOffRange(int index)
        {
            for (int j = 0; j < this.ranges.Count; j++)
            {
                if (this.glowworms[index].Position.Axis.Values[j] > this.ranges[j].Max
                    || this.glowworms[index].Position.Axis.Values[j] < this.ranges[j].Min)
                {
                    List<double> arguments = new List<double>();
                    for (int k = 0; k < this.function.ArgumentsSymbol.Count; k++)
                    {
                        arguments.Add(this.randomGenerator.NextDouble(this.ranges[k].Min, this.ranges[k].Max));
                    }
                    this.glowworms[index].Position.Axis.Values = arguments;
                    try
                    {
                        this.glowworms[index].Position.Fitness = this.function.Evaluate(this.glowworms[index].Position.Axis.Values);
                    }
                    catch
                    {
                        throw new AlgorithmException(AlgorithmExceptionType.FunctionNotExecuted);
                    }

                    for (int k = 0; k < this.ranges.Count; k++)
                    {
                        this.glowworms[index].SensorRange[k] = (2 * Math.Abs(this.ranges[k].Max - this.ranges[k].Min)) / (double)4;
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
                this.glowworms.Add(new Glowworm(arguments));
                this.glowworms[this.glowworms.Count - 1].Luciferin = 5;

                for (int j = 0; j < this.function.ArgumentsSymbol.Count; j++)
                {
                    this.glowworms[this.glowworms.Count - 1].SensorRange.Add((2 * Math.Abs(this.ranges[j].Max - this.ranges[j].Min)) / (double)4);
                }

                try
                {
                    this.glowworms[this.glowworms.Count - 1].Position.Fitness = this.function.Evaluate(this.glowworms[i].Position.Axis.Values);
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

            double lastBestFitness = this.glowworms.Max().Position.Fitness;

            UpdateLuciferin();
            MoveGlowworms();

            if (Math.Abs(lastBestFitness - this.glowworms.Max().Position.Fitness) < 0.01)
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
                this.alpha = 0.4;
                this.beta = 0.08;
                this.gamma = 0.6;
                this.n_t = 5;
                this.r_max.Clear();
                this.s.Clear();

                for (int i = 0; i < this.ranges.Count; i++)
                {
                    this.r_max.Add((2 * Math.Abs(this.ranges[i].Max - this.ranges[i].Min)) / (double)3);
                    this.s.Add(Math.Abs(this.ranges[i].Max - this.ranges[i].Min) / (double)(Math.Pow(Math.Abs(this.ranges[i].Max - this.ranges[i].Min), 2) + 2));
                }

                this.acctualIteration = 1;
                this.canIEndIterator = 0;
                this.glowworms.Clear();
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
                    result = this.glowworms.Max().Position;
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
                    result = this.glowworms.Max().Position;
                    break;
                }
            }

            this.Reset();
            return result;
        }

        private void UpdateLuciferin()
        {
            for (int i = 0; i < this.glowworms.Count; i++)
            {
                this.glowworms[i].Luciferin = ((double)1 - this.alpha) * this.glowworms[i].Luciferin + (this.gamma * this.glowworms[i].Position.Fitness);
            }
        }

        private void MoveGlowworms()
        {
            for (int i = 0; i < this.glowworms.Count; i++)
            {
                List<Glowworm> i_neighbours = new List<Glowworm>();
                double i_neighbours_luciferin_sum = 0;
                for (int j = 0; j < this.glowworms.Count; j++)
                {
                    if (i != j)
                    {
                        double d = this.DistanceBetweenGlowworms(this.glowworms[i], this.glowworms[j]);

                        bool ifIFindNeighbour = false;
                        for (int k = 0; k < this.ranges.Count; k++)
                        {
                            if ((d < this.glowworms[i].SensorRange[k])
                                && (this.glowworms[i].Luciferin < this.glowworms[j].Luciferin))
                            {
                                i_neighbours.Add(this.glowworms[j]);
                                i_neighbours_luciferin_sum += (this.glowworms[j].Luciferin - this.glowworms[i].Luciferin);
                                ifIFindNeighbour = true;
                                break;
                            }
                        }
                        if (ifIFindNeighbour)
                        {
                            break;
                        }
                    }
                }

                for (int j = 0; j < i_neighbours.Count; j++)
                {
                    i_neighbours[j].MoveProbability = (i_neighbours[j].Luciferin - this.glowworms[i].Luciferin) / i_neighbours_luciferin_sum;
                }

                int i_to_j_glowworm_index = GetRandomBestGlowwormIndex(i_neighbours);

                if (i_to_j_glowworm_index != -1)
                {
                    for (int j = 0; j < this.function.ArgumentsSymbol.Count; j++)
                    {
                        double i_j_difference = this.glowworms[i_to_j_glowworm_index].Position.Axis.Values[j] - this.glowworms[i].Position.Axis.Values[j];
                        this.glowworms[i].Position.Axis.Values[j] += this.s[j] * (i_j_difference / Math.Abs(i_j_difference));
                        this.glowworms[i].SensorRange[j] = Math.Min(this.r_max[j], Math.Max(0, this.glowworms[i].SensorRange[j] + this.beta * (this.n_t - Math.Abs(i_neighbours.Count))));
                    }

                    this.ResetIfOutOffRange(i);

                    try
                    {
                        this.glowworms[i].Position.Fitness = this.function.Evaluate(this.glowworms[i].Position.Axis.Values);
                    }
                    catch
                    {
                        throw new AlgorithmException(AlgorithmExceptionType.FunctionNotExecuted);
                    }
                }
            }
        }

        private int GetRandomBestGlowwormIndex(List<Glowworm> glowworms_list)
        {
            if (glowworms_list.Count != 0)
            {
                int result_index = 500;
                double rng_max_range = 0;
                for (int i = 0; i < glowworms_list.Count; i++)
                {
                    rng_max_range += glowworms_list[i].MoveProbability;
                }

                double rng_move_number = this.randomGenerator.NextDouble(0, rng_max_range);
                double sum_range = 0;

                for (int i = 0; i < glowworms_list.Count; i++)
                {
                    if ((rng_move_number >= sum_range)
                        && (rng_move_number <= (sum_range + glowworms_list[i].MoveProbability)))
                    {
                        result_index = this.glowworms.IndexOf(glowworms_list[i]);
                        break;
                    }

                    sum_range += glowworms_list[i].MoveProbability;
                }
                return result_index;
            }
            else
                return -1;
        }

        private double DistanceBetweenGlowworms(Glowworm glowworm1, Glowworm glowworm2)
        {
            double distanceSum = 0;
            for (int i = 0; i < this.ranges.Count; i++)
            {
                distanceSum += Math.Pow(glowworm1.Position.Axis.Values[i] - glowworm2.Position.Axis.Values[i], 2);
            }
            return Math.Sqrt(distanceSum);
        }
    }
}
