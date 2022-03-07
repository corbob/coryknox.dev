using Statiq.App;
using Statiq.Web;
using System.Diagnostics;

if (args.Where(val => val.Equals("--attach")).ToArray().Length != 0 && !Debugger.IsAttached)
{
    Console.WriteLine(string.Format("Waiting for a debugger for PID {0}", Process.GetCurrentProcess().Id));
    while (!Debugger.IsAttached)
    {
        Thread.Sleep(250);
    }
}

var something = Bootstrapper
    .Factory
    .CreateWeb(args);
await something.RunAsync();
