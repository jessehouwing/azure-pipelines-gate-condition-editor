using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace ServerTaskExpressionTester
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        private static System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            Dictionary<string, string> assemblies = new Dictionary<string, string>
            {
                ["Microsoft.TeamFoundation.DistributedTask.Orchestration.Server"] = @"Tools\",
                ["Microsoft.TeamFoundation.DistributedTask.Orchestration.Server.Extensions"] = @"Tools\Plugins",
                ["Microsoft.TeamFoundation.DistributedTask.WebApi"] = @"Tools\",
                ["Microsoft.VisualStudio.Services.Common"] = @"Tools\",
                ["Microsoft.VisualStudio.Services.WebApi"] = @"Tools\",
                ["Microsoft.TeamFoundation.Framework.Server"] = @"Tools\",
                ["Newtonsoft.Json"] = @"Tools\"
            };

            var tfsPath17 = (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\TeamFoundationServer\17.0", "InstallPath", string.Empty);

            if (string.IsNullOrWhiteSpace(tfsPath17))
            {
                MessageBox.Show("Please install Azure DevOps Server 2019 before running this tool.", "Cannot load.",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
                Application.Exit();
            }
            else
            {
                AssemblyName name = new AssemblyName(args.Name);

                if (assemblies.Keys.Contains(name.Name))
                {
                    return Assembly.LoadFile(System.IO.Path.Combine(tfsPath17, assemblies[name.Name], name.Name + ".dll"));
                }
            }

            return null;
        }
    }
}
