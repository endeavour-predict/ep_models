using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ep_models.TestWrapper
{
    public class ParamTestPair
    {
        public string TestName { get; set; }
        public EPInputModel EPInputModel { get; set; }
        public PredictionModel PredictionModel { get; set; }
    }
}
