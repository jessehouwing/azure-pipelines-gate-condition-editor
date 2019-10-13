using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.TeamFoundation.DistributedTask.Expressions;
using Microsoft.TeamFoundation.DistributedTask.Orchestration.Server.Extensions.ServerExecutionTasks.HttpRequest;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ServerTaskExpressionTester.Properties;

namespace ServerTaskExpressionTester
{
    public partial class Form1 : Form
    {
        private IFunctionInfo _jsonpathFunctionInfo;
        private IFunctionInfo _countFunctionInfo;
        private IFunctionInfo _isNullOrEmptyFunctionInfo;
        private IFunctionInfo _splitFunctionInfo;
        private IFunctionInfo _intersectFunctionInfo;

        public Form1()
        {
            InitializeComponent();

            // (INamedValueInfo)new NamedValueInfo<TaskInputsNode>("TaskInputs")

            // 2018
            _jsonpathFunctionInfo = InitializeFunctionNode("JsonPath", 1, 1);
            _countFunctionInfo = InitializeFunctionNode("Count", 1, 1);

            // 2019
            _isNullOrEmptyFunctionInfo = InitializeFunctionNode("IsNullOrEmpty", 1, 1);
            _splitFunctionInfo = InitializeFunctionNode("Split", 2, 2);
            _intersectFunctionInfo = InitializeFunctionNode("Intersect", 2, 2);

            // (IFunctionInfo) new FunctionInfo<LengthNode>(InputValidationConstants.Length, 1, 1),
            // (IFunctionInfo) new FunctionInfo<IsUrlNode>(InputValidationConstants.IsUrl, 1, 1)

            expressionEditor.MaxLength = int.MaxValue;
            responseEditor.MaxLength = int.MaxValue;
            Llog.MaxLength = int.MaxValue;
        }

        private IFunctionInfo InitializeFunctionNode(string name,int a, int b)
        {
            Type jsonpathFunction = Type.GetType("Microsoft.TeamFoundation.DistributedTask.Expressions.FunctionInfo`1[[Microsoft.TeamFoundation.DistributedTask.Orchestration.Server.Extensions.ServerExecutionTasks.HttpRequest." + name + "Node, Microsoft.TeamFoundation.DistributedTask.Orchestration.Server.Extensions]], Microsoft.TeamFoundation.DistributedTask.WebApi");
            return (IFunctionInfo)jsonpathFunction.GetConstructor(new[]{
                typeof(string), typeof(int), typeof(int) }
            ).Invoke(new object[] { name, a, b });
        }

        private void Evaluate()
        {
            var text = expressionEditor.Text;
            var response = responseEditor.Text;
            if (string.IsNullOrWhiteSpace(text) || string.IsNullOrWhiteSpace(response))
            {
                return;
            }

            IDictionary<string, string> variables = new Dictionary<string, string>();
            foreach (var row in dataGridView1.Rows.Cast<DataGridViewRow>())
            {
                variables.Add((string)row.Cells[0].Value, (string)row.Cells[1].Value);
                text = text.Replace("$(" + (string) row.Cells[0].Value + ")", (string) row.Cells[1].Value);
            }

            try
            {
                Llog.Text = string.Empty;

                IFunctionInfo[] functionInfoArray = {
                    _countFunctionInfo,
                    _jsonpathFunctionInfo,
                    _isNullOrEmptyFunctionInfo,
                    _splitFunctionInfo,
                    _intersectFunctionInfo
                };

                bool result = false;
                try
                {
                    JToken jtoken = JToken.Parse(response);

                    ExpressionParser parser = new ExpressionParser();
                    var tree = parser.CreateTree(
                        expression: text,
                        trace: null,
                        namedValues: new INamedValueInfo[] {
                            new DictionaryNode("Variables", variables),
                            new ObjectNode("Root", jtoken)
                        },
                        functions: functionInfoArray
                    );

                    result = tree.EvaluateBoolean(
                        trace: new TextBoxTraceWriter(Llog),
                        secretMasker: null,
                        state: jtoken
                    );
                }
                catch (Exception e)
                {
                    Llog.Text += Environment.NewLine + e.Message;
                }

                if (result)
                {
                    Status.Text = "Success";
                    Status.ForeColor = Color.Aquamarine;
                }
                else
                {
                    Status.Text = "Failed";
                    Status.ForeColor = Color.DarkOrange;
                }
            }
            catch (Exception ex)
            {
                Status.Text = "Error";
                Status.ForeColor = Color.DarkRed;
                Llog.Text = ex.Message;
            }
        }

        public IList<string> GetVariables()
        {
            var matches = Regex.Matches(expressionEditor.Text, @"\$\((?<variablename>[^\)]+)\)|variables\[\'(?<variablename>[^\)]+)'\]|variables\.(?<variablename>[a-zA-Z0-9-_.]+)");
            List<string> result = new List<string>(matches.Count);
            foreach (var m in matches.Cast<Match>())
            {
                result.Add(m.Groups["variablename"].Value);
            }

            return result;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            var variables = GetVariables();
            var existingVariables = dataGridView1.Rows.Cast<DataGridViewRow>().Select(row => row.Cells[0].Value).Distinct().ToArray();
            var newVariables = variables.Except(existingVariables).Distinct();
            var deleteVariables = existingVariables.Except(variables).Distinct();

            var rowsToDelete = dataGridView1.Rows.Cast<DataGridViewRow>()
                .Where(row => deleteVariables.Contains(row.Cells[0].Value));

            foreach (var row in rowsToDelete)
            {
                dataGridView1.Rows.Remove(row);
            }

            foreach (var variable in newVariables)
            {
                dataGridView1.Rows.Add(variable, "");
            }

            Evaluate();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Evaluate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var json = Settings1.Default.Variables;
            if (!string.IsNullOrWhiteSpace(json))
            {
                var variables = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                foreach (var variable in variables)
                {
                    dataGridView1.Rows.Add(variable.Key, variable.Value);
                }
            }

            expressionEditor.Text = Settings1.Default.Expression;
            responseEditor.Text = Settings1.Default.JsonBody;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings1.Default.JsonBody = responseEditor.Text;
            Settings1.Default.Expression = expressionEditor.Text;

            var variables = new Dictionary<string, string>();
            foreach (var row in dataGridView1.Rows.Cast<DataGridViewRow>())
            {
                variables.Add((string)row.Cells[0].Value, (string)row.Cells[1].Value);
            }

            Settings1.Default.Variables = JsonConvert.SerializeObject(variables);
            Settings1.Default.Save();
        }

        private void LLog_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Llog.Text))
            {
                MessageBox.Show(Llog.Text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            Evaluate();
        }
    }
}
