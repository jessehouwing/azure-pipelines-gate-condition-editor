using Microsoft.TeamFoundation.DistributedTask.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerTaskExpressionTester
{
    internal class ObjectNode : NamedValueNode, INamedValueInfo
    {
        private readonly object value;
        private string name;

        internal ObjectNode(string name, object value)
        {
            this.name = name;
            this.Name = name;
            this.value = value;
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
            return value;
        }
    }
}
