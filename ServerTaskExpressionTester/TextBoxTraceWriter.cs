using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.TeamFoundation.DistributedTask.Expressions;

namespace ServerTaskExpressionTester
{
    internal class TextBoxTraceWriter :ITraceWriter
    {
        private TextBox target;

        public TextBoxTraceWriter(TextBox target)
        {
            this.target = target;
        }

        public void Info(string message)
        {
            target.Text += Environment.NewLine + message;
        }

        public void Verbose(string message)
        {
            target.Text += Environment.NewLine + message;
        }
    }
}
