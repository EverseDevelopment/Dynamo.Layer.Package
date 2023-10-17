using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elements
{
    internal class Param
    {
        public string DataType { get; set; }
        public string Name { get; set; }
        public string ParameterGroup { get; set; }
        public string RevitId { get; set; }
        public double Scalar { get; set; }
        public string Unit { get; set; }
        public string Value { get; set; }
    }
}
