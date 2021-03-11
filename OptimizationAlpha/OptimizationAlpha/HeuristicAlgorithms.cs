using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeuristicAlgorithms;
using OptimizationGlobals;

namespace OptimizationAlpha
{
    class HeuristicAlgorithms
    {
        private int iterations;
        private List<Algorithm> algorithms;

        public BatAlgorithm BatAlgorithm { get { return (BatAlgorithm)this.algorithms[0]; } }
        public ParticleSwarmAlgorithm ParticleSwarmAlgorithm { get { return (ParticleSwarmAlgorithm)this.algorithms[1]; } }
        public SimulatedAnnealingAlgorithm SimulatedAnnealingAlgorithm { get { return (SimulatedAnnealingAlgorithm)this.algorithms[2]; } }
        public FireflyAlgorithm FireflyAlgorithm { get { return (FireflyAlgorithm)this.algorithms[3]; } }
        public GlowwormAlgorithm GlowwormAlgorithm { get { return (GlowwormAlgorithm)this.algorithms[4]; } }
        public ChargedSystemSearchAlgorithm ChargedSystemSearchAlgorithm { get { return (ChargedSystemSearchAlgorithm)this.algorithms[5]; } }


        public int Iterations
        {
            get
            {
                return this.iterations;
            }
            set
            {
                if(value > 20)
                {
                    this.iterations = 20;
                }
                else if(value <= 0)
                {
                    this.iterations = 1;
                }
                else
                {
                    this.iterations = value;
                }
            }
        }

        public HeuristicAlgorithms()
        {
            this.algorithms = new List<Algorithm>();
            this.iterations = 10;

            //minimum
            this.algorithms.Add(new BatAlgorithm());
            this.algorithms.Add(new ParticleSwarmAlgorithm());
            this.algorithms.Add(new SimulatedAnnealingAlgorithm());

            //maximum
            this.algorithms.Add(new FireflyAlgorithm());
            this.algorithms.Add(new GlowwormAlgorithm());
            this.algorithms.Add(new ChargedSystemSearchAlgorithm());
        }

        public FitnessPoint GetOptimalPoint(AlgorithmType type, string functionExpression, List<string> argumentsSymbols, List<Compartment> ranges)
        {
            if (argumentsSymbols.Count != ranges.Count)
            {
                throw new AlgorithmException(AlgorithmExceptionType.DifferenceArguments);
            }
            for (int i = 0; i < argumentsSymbols.Count; i++)
            {
                if (!functionExpression.Contains(argumentsSymbols[i]))
                {
                    throw new AlgorithmException(AlgorithmExceptionType.WrongParametersArguments);
                }
            }

            Function function = new Function(functionExpression, argumentsSymbols);

            int agentsCount = 0;
            for (int i = 0; i < ranges.Count; i++)
            {
                agentsCount += (int)Math.Abs(ranges[i].Min - ranges[i].Max);
            }
            for (int i = 0; i < this.algorithms.Count; i++)
            {
                if (this.algorithms[i].AlgorithmType == type)
                {
                    this.algorithms[i].SetNewAndReset(function, agentsCount, ranges);
                }
            }

            List<FitnessPoint> bestPoints = new List<FitnessPoint>();
            for (int i = 0; i < this.Iterations; i++)
            {
                for (int j = 0; j < this.algorithms.Count; j++)
                {
                    if (this.algorithms[j].AlgorithmType == type)
                    {
                        FitnessPoint best_point = this.algorithms[j].GenerateBestValue();
                        bestPoints.Add(best_point);
                    }
                }
            }

            FitnessPoint result = null;
            if (type == AlgorithmType.Maximum)
            {
                result = bestPoints.Max();
            }
            else if (type == AlgorithmType.Minimum)
            {
                result = bestPoints.Min();
            }

            return result;
        }

        public async Task<FitnessPoint> GetOptimalPointAsync(AlgorithmType type, string functionExpression, List<string> argumentsSymbols, List<Compartment> ranges)
        {
            if(argumentsSymbols.Count != ranges.Count)
            {
                throw new AlgorithmException(AlgorithmExceptionType.DifferenceArguments);
            }
            for(int i=0; i< argumentsSymbols.Count; i++)
            {
                if(!functionExpression.Contains(argumentsSymbols[i]))
                {
                    throw new AlgorithmException(AlgorithmExceptionType.WrongParametersArguments);
                }
            }

            Function function = new Function(functionExpression, argumentsSymbols);

            int agentsCount = 0;
            for(int i=0; i<ranges.Count; i++)
            {
                agentsCount += (int)Math.Abs(ranges[i].Min - ranges[i].Max);
            }
            for (int i = 0; i < this.algorithms.Count; i++)
            {
                if (this.algorithms[i].AlgorithmType == type)
                {
                    this.algorithms[i].SetNewAndReset(function, agentsCount, ranges);
                }
            }

            List<FitnessPoint> bestPoints = new List<FitnessPoint>();
            for (int i = 0; i < this.Iterations; i++)
            {
                for (int j = 0; j < this.algorithms.Count; j++)
                {
                    if (this.algorithms[j].AlgorithmType == type)
                    {
                        FitnessPoint best_point = await this.algorithms[j].GenerateBestValueAsync();
                        bestPoints.Add(best_point);
                    }
                }
            }

            FitnessPoint result = null;
            if(type == AlgorithmType.Maximum)
            {
                result = bestPoints.Max();
            }
            else if (type == AlgorithmType.Minimum)
            {
                result = bestPoints.Min();
            }

            return result;
        }

        public List<FitnessPoint> GetAllOptimalPoints(AlgorithmType type, string functionExpression, List<string> argumentsSymbols, List<Compartment> ranges)
        {
            if (argumentsSymbols.Count != ranges.Count)
            {
                throw new AlgorithmException(AlgorithmExceptionType.DifferenceArguments);
            }
            for (int i = 0; i < argumentsSymbols.Count; i++)
            {
                if (!functionExpression.Contains(argumentsSymbols[i]))
                {
                    throw new AlgorithmException(AlgorithmExceptionType.WrongParametersArguments);
                }
            }

            Function function = new Function(functionExpression, argumentsSymbols);

            int agentsCount = 0;
            for (int i = 0; i < ranges.Count; i++)
            {
                agentsCount += (int)Math.Abs(ranges[i].Min - ranges[i].Max);
            }
            for (int i = 0; i < this.algorithms.Count; i++)
            {
                if (this.algorithms[i].AlgorithmType == type)
                {
                    this.algorithms[i].SetNewAndReset(function, agentsCount, ranges);
                }
            }

            List<FitnessPoint> bestPoints = new List<FitnessPoint>();
            for (int i = 0; i < this.Iterations; i++)
            {
                for (int j = 0; j < this.algorithms.Count; j++)
                {
                    if (this.algorithms[j].AlgorithmType == type)
                    {
                        FitnessPoint best_point = this.algorithms[j].GenerateBestValue();
                        bestPoints.Add(best_point);
                    }
                }
            }

            return bestPoints;
        }

        public async Task<List<FitnessPoint>> GetAllOptimalPointsAsync(AlgorithmType type, string functionExpression, List<string> argumentsSymbols, List<Compartment> ranges)
        {
            if (argumentsSymbols.Count != ranges.Count)
            {
                throw new AlgorithmException(AlgorithmExceptionType.DifferenceArguments);
            }
            for (int i = 0; i < argumentsSymbols.Count; i++)
            {
                if (!functionExpression.Contains(argumentsSymbols[i]))
                {
                    throw new AlgorithmException(AlgorithmExceptionType.WrongParametersArguments);
                }
            }

            Function function = new Function(functionExpression, argumentsSymbols);

            int agentsCount = 0;
            for (int i = 0; i < ranges.Count; i++)
            {
                agentsCount += (int)Math.Abs(ranges[i].Min - ranges[i].Max);
            }
            for (int i = 0; i < this.algorithms.Count; i++)
            {
                if (this.algorithms[i].AlgorithmType == type)
                {
                    this.algorithms[i].SetNewAndReset(function, agentsCount, ranges);
                }
            }

            List<FitnessPoint> bestPoints = new List<FitnessPoint>();
            for (int i = 0; i < this.Iterations; i++)
            {
                for (int j = 0; j < this.algorithms.Count; j++)
                {
                    if (this.algorithms[j].AlgorithmType == type)
                    {
                        FitnessPoint best_point = await this.algorithms[j].GenerateBestValueAsync();
                        bestPoints.Add(best_point);
                    }
                }
            }

            return bestPoints;
        }
    }
}
