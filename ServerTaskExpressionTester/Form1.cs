using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.TeamFoundation.DistributedTask.Orchestration.Server.Extensions.ServerExecutionTasks.HttpRequest;
using Newtonsoft.Json;
using ServerTaskExpressionTester.Properties;

namespace ServerTaskExpressionTester
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Evaluate();
        }

        private void Evaluate()
        {
            var text = expressionEditor.Text;
            foreach (var row in dataGridView1.Rows.Cast<DataGridViewRow>())
            {
                text = text.Replace("$(" + ((string) row.Cells[0].Value) + ")", (string) row.Cells[1].Value);
            }

            try
            {
                bool result = HttpRequestExpressionParser.EvaluateExpression(null, text, responseEditor.Text);
                if (result)
                {
                    Status.Text = "Success";
                    Status.ForeColor = Color.Aquamarine;
                }
                else
                {
                    Status.Text = "Failed";
                    Status.ForeColor = Color.Orange;
                }
                
                Llog.Text = string.Empty;
            }
            catch (Exception ex)
            {
                Status.Text = "Error";
                Status.ForeColor = Color.DarkRed;
                Llog.Text = ex.GetType() + Environment.NewLine + ex.Message;
            }
        }

        public IList<string> GetVariables()
        {
            var matches = Regex.Matches(expressionEditor.Text, @"\$\(([^\)]+)\)");
            List<string> result = new List<string>(matches.Count);
            foreach (var m in matches.Cast<Match>())
            {
                result.Add(m.Groups[1].Value);
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
            MessageBox.Show(Llog.Text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            Evaluate();
        }
    }
}
