using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeknikEnvanter_3
{
    public class Job
    {
        public long Id { get; set; }
        public string ModuleClassName { get; set; }
        public string MethodName { get; set; }
        public DateTime ExecutionTime { get; set; }
        public List<JobParameter> Parameters { get; set; }

        public object[] GetObjectParameters()
        {
            if (Parameters != null && Parameters.Count > 0)
            {
                List<object> objectList = new List<object>();
                foreach (var item in Parameters.OrderBy(a => a.OrderNo))
                {
                    object obj = null;
                    try
                    {
                        switch (item.ParameterType)
                        {
                            case ParameterType.String:
                                obj = item.ParameterValue.ToString();
                                break;
                            case ParameterType.Number:
                                obj = Convert.ToInt32(item.ParameterValue);
                                break;
                            case ParameterType.Boolean:
                                obj = Convert.ToBoolean(item.ParameterValue);
                                break;
                            case ParameterType.DateTime:
                                obj = Convert.ToDateTime(item.ParameterValue);
                                break;
                        }
                    }
                    catch (Exception) { }
                    finally { objectList.Add(obj); }
                }
                return objectList.ToArray();
            }
            else
                return null;
        }
    }
}

