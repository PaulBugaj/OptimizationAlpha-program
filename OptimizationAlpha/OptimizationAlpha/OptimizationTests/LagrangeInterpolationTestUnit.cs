using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Communication;

namespace OptimizationTests
{
    public class LagrangeInterpolationTestUnit
    {
        [XmlElement(ElementName = "FileToTestPath")]
        public List<LITest> FileToTest { get; set; }

        public LagrangeInterpolationTestUnit()
        {
            this.FileToTest = new List<LITest>();
        }
    }

    public class LITest
    {
        public string Path { get; set; }
        public FileType FileType { get; set; }

        public LITest()
        {
            this.Path = string.Empty;
            this.FileType = FileType.TXT;
        }

        public LITest(string path, FileType fileType)
        {
            this.Path = path;
            this.FileType = fileType;
        }
    }
}
