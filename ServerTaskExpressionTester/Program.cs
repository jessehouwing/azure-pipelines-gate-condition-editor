using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.Win32;

namespace ServerTaskExpressionTester
{
    static class Program
    {
        private static readonly string[] ServerVersions = { "20.0", "19.0", "18.0", "17.0" };
        private static string serverPath;

        [STAThread]
        static void Main()
        {
            try
            {
                serverPath = FindServerInstallation();
                if (serverPath == null)
                {
                    throw new InvalidOperationException(
                        "Azure DevOps Server 2019, 2020, 2022 or 2025 must be installed with its Tools assemblies.");
                }

                AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                RunApplication();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString(), "Cannot start Gate Condition Editor",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void RunApplication()
        {
            Application.Run(new Form1());
        }

        private static string FindServerInstallation()
        {
            foreach (string version in ServerVersions)
            {
                foreach (RegistryView view in new[] { RegistryView.Registry64, RegistryView.Registry32 })
                {
                    using (RegistryKey machine = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, view))
                    using (RegistryKey key = machine.OpenSubKey(@"SOFTWARE\Microsoft\TeamFoundationServer\" + version))
                    {
                        string path = key == null ? null : key.GetValue("InstallPath") as string;
                        if (!string.IsNullOrWhiteSpace(path) &&
                            File.Exists(Path.Combine(path, @"Tools\Microsoft.TeamFoundation.DistributedTask.WebApi.dll")) &&
                            File.Exists(Path.Combine(path, @"Tools\Plugins\Microsoft.TeamFoundation.DistributedTask.Orchestration.Server.Extensions.dll")))
                        {
                            return path;
                        }
                    }
                }
            }

            return null;
        }

        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            var requested = new AssemblyName(args.Name);
            if (string.IsNullOrEmpty(requested.Name) ||
                requested.Name != Path.GetFileName(requested.Name) ||
                (!string.IsNullOrEmpty(requested.CultureName) &&
                 !string.Equals(requested.CultureName, "neutral", StringComparison.OrdinalIgnoreCase)))
            {
                return null;
            }

            string[] directories = requested.Name == "Microsoft.TeamFoundation.DistributedTask.Orchestration.Server.Extensions"
                ? new[] { @"Tools\Plugins", "Tools", @"Application Tier\Message Queue\bin" }
                : new[] { "Tools", @"Tools\Plugins", @"Application Tier\Message Queue\bin" };

            foreach (string directory in directories)
            {
                string path = Path.Combine(serverPath, directory, requested.Name + ".dll");
                if (!File.Exists(path))
                {
                    continue;
                }

                AssemblyName candidate = AssemblyName.GetAssemblyName(path);
                byte[] requestedToken = requested.GetPublicKeyToken();
                if (!string.Equals(candidate.Name, requested.Name, StringComparison.OrdinalIgnoreCase) ||
                    (requestedToken != null && requestedToken.Length != 0 &&
                     !TokensEqual(candidate.GetPublicKeyToken(), requestedToken)))
                {
                    continue;
                }

                return Assembly.LoadFrom(path);
            }

            return null;
        }

        private static bool TokensEqual(byte[] left, byte[] right)
        {
            if (left == null || right == null || left.Length != right.Length)
            {
                return false;
            }

            for (int i = 0; i < left.Length; i++)
            {
                if (left[i] != right[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
