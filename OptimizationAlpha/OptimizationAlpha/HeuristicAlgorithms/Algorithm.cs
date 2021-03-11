using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OptimizationGlobals;

namespace HeuristicAlgorithms
{
    abstract class Algorithm
    {
        protected AlgorithmType algorithmType;
        protected Function function;
        protected List<Compartment> ranges;
        protected int pointsCount;
        protected LineRandom randomGenerator;
        protected int canIEndIterator;
        protected int acctualIteration;

        public int AcctualIteration { get { return this.acctualIteration; } }
        public AlgorithmType AlgorithmType { get { return this.algorithmType; } }

        protected Algorithm(Function function, int pointsCount, List<Compartment> ranges)
        {
            this.algorithmType = AlgorithmType.None;
            this.randomGenerator = new LineRandom();
            this.canIEndIterator = 0;
            this.acctualIteration = 1;

            if (function.ArgumentsSymbol.Count != ranges.Count)
            {
                throw new AlgorithmException(AlgorithmExceptionType.DifferenceArguments);
            }

            this.pointsCount = pointsCount;
            this.function = function;
            this.ranges = new List<Compartment>(ranges);
        }

        protected Algorithm()
        {
            this.algorithmType = AlgorithmType.None;
            this.randomGenerator = new LineRandom();
            this.canIEndIterator = 0;
            this.acctualIteration = 1;

            this.pointsCount = 0;
            this.function = null;
            this.ranges = new List<Compartment>();
        }

        public abstract bool NextIteration();
        public abstract void SetNewAndReset(Function function, int agentsCount, List<Compartment> ranges);
        public abstract void Reset();
        public abstract FitnessPoint GenerateBestValue();
        public abstract Task<FitnessPoint> GenerateBestValueAsync();
    }
}
