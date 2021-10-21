using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace ServerTaskExpressionTester
{
    static class Program
    {
        static string tfsPath17 = (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\TeamFoundationServer\17.0", "InstallPath", string.Empty);
        static string tfsPath18 = (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\TeamFoundationServer\18.0", "InstallPath", string.Empty);
        static string localpath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        static string tfsPath = tfsPath17 ?? tfsPath18;

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

            AssemblyName name = new AssemblyName(args.Name);

#if DEBUG
            string path = localpath;
            string assemblyPath = Path.Combine(localpath, "2020", name.Name + ".dll");
#else
            string path = tfsPath;
            string assemblyPath = Path.Combine(tfsPath, assemblies[name.Name], name.Name + ".dll");
#endif

            if (string.IsNullOrWhiteSpace(path))
            {
                MessageBox.Show("Please install Azure DevOps Server 2019 or later before running this tool.", "Cannot load.",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
                Application.Exit();
            }
            else
            {
                if (assemblies.Keys.Contains(name.Name))
                {
                    try
                    {
                        var ass = Assembly.LoadFrom(assemblyPath);
                        return ass;
                    }
                    catch (Exception e)
                    {
                        Console.Error.WriteLine(e.Message);
                        return null;
                    }
                }
            }

            return null;
        }
    }
}
