using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace HeuristicAlgorithms
{
    enum AlgorithmExceptionType { ParametersNotSeted, FunctionNotExecuted, DifferenceArguments, FunctionNotSeted, WrongParametersArguments, BadRanges }

    class AlgorithmException : Exception
    {
        public AlgorithmExceptionType Fail { get; private set; }

        public AlgorithmException(AlgorithmExceptionType fail)
        {
            this.Fail = fail;
        }
    }
}
