using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OptimizationGlobals;
using System.Xml.Serialization;

namespace OptimizationTests
{
    public class HATestData
    {
        public AlgorithmType Type { get; set; }
        public string FunctionExpression { get; set; }


        [XmlElement(ElementName = "ArgumentsSymbolsItem")]
        public List<string> ArgumentsSymbols { get; set; }


        [XmlElement(ElementName = "RangesItem")]
        public List<Compartment> Ranges { get; set; }

        public double ExpectedValue { get; set; }

        public HATestData()
        {
            this.Type = AlgorithmType.None;
            this.FunctionExpression = string.Empty;
            this.ArgumentsSymbols = new List<string>();
            this.Ranges = new List<Compartment>();
            this.ExpectedValue = 0;
        }
    }

    public class HATestUnit
    {
        [XmlElement(ElementName = "HATestDataItem")]
        public List<HATestData> HATestDatas { get; set; }

        public HATestUnit()
        {
            this.HATestDatas = new List<HATestData>();
        }
    }
}
