using System;
using System.Management.Automation;
using System.Threading;
using System.IO;

namespace powershell_Invoke
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("please enter the path to script, include the script name, e.g. 'C:/DNS.ps1' ");

            String path = Console.ReadLine();

            using PowerShell PowerShellInstance = PowerShell.Create();
            PSDataCollection<PSObject> output = new();

            String script = File.ReadAllText(path);
            PowerShellInstance.AddScript(script);

            // begin invoke execution on the pipeline
            //IAsyncResult result = PowerShellInstance.BeginInvoke();
            IAsyncResult result = PowerShellInstance.BeginInvoke<PSObject, PSObject>(null, output);

            // simulate being busy until execution has completed with sleep or wait
            while (result.IsCompleted == false)
            {
                Console.WriteLine("Pipeline is being executed...");
                Thread.Sleep(3000);
                //optionally add a timeout here...            
            }
            foreach (PSObject o in output)
            {
                Console.WriteLine(o.GetType());
                Console.WriteLine(o);
            }

            Console.ReadKey();
        }
    }
}
