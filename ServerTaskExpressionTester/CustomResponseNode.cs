using Microsoft.TeamFoundation.DistributedTask.Expressions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerTaskExpressionTester
{
    internal class CustomResponseNode : NamedValueNode, INamedValueInfo
    {
        private readonly JToken content;
        private readonly string statuscode;
        private readonly IDictionary<string, string> headers;
        private string name;

        internal CustomResponseNode(string name, JToken content, string statuscode, IDictionary<string, string> headers)
        {
            this.content = content;
            this.statuscode = statuscode;
            this.headers = headers;
            this.name = name;
        }

        string INamedValueInfo.Name
        {
            get { return name; }
        }

        public NamedValueNode CreateNode()
        {
            return this;
        }

        protected override sealed object EvaluateCore(EvaluationContext evaluationContext)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            dictionary["content"] = content;
            dictionary["statuscode"] = statuscode;
            dictionary["headers"] = headers;
            return dictionary;
        }
    }
}
