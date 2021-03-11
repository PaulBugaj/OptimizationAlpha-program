using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Z.Expressions;

namespace OptimizationGlobals
{
    class Function
    {
        public string FunctionExpression { get; private set; }
        private List<string> argumentsSymbol;

        public ReadOnlyCollection<string> ArgumentsSymbol { get { return this.argumentsSymbol.AsReadOnly(); } }

        public Function(string functionExpression, List<string> argumentsSymbol)
        {
            this.FunctionExpression = string.Copy(functionExpression);
            this.argumentsSymbol = new List<string>(argumentsSymbol);
        }

        public double Evaluate(List<double> argumentsValues)
        {
            if(argumentsValues.Count != this.argumentsSymbol.Count)
            {
                throw new Exception();
            }

            Dictionary<string, double> argument_value = new Dictionary<string, double>();
            for(int i=0; i<this.argumentsSymbol.Count; i++)
            {
                argument_value.Add(this.argumentsSymbol[i], argumentsValues[i]);
            }

            double result = 0;
            try
            {
                result = Eval.Execute<double>(this.FunctionExpression, argument_value);
            }
            catch
            {
                throw new Exception();
            }

            return result;
        }

        public static double Evaluate(string expression, Dictionary<string, double> symbolsValues)
        {
            double result = 0;
            try
            {
                result = Eval.Execute<double>(expression, symbolsValues);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return result;
        }
    }
}
