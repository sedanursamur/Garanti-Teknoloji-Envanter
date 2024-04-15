using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeknikEnvanter_3
{
    public class JobParameter
    {
        public int JobId { get; set; }
        public ParameterType ParameterType { get; set; }
        public int OrderNo { get; set; }
        public string ParameterValue { get; set; }
    }
}
