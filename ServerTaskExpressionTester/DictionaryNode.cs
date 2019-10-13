using Microsoft.TeamFoundation.DistributedTask.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerTaskExpressionTester
{
    internal class DictionaryNode : NamedValueNode, INamedValueInfo
    {
        private readonly IDictionary<string, string> values;
        private string name;

        internal DictionaryNode(string name, IDictionary<string, string> values)
        {
            this.name = name;
            this.Name = name;
            this.values = values;
        }

        string INamedValueInfo.Name
        {
            get { return name; }
        }

        public NamedValueNode CreateNode()
        {
            return this;
        }

        protected override object EvaluateCore(EvaluationContext context)
        {
            return values;
        }
    }
}
