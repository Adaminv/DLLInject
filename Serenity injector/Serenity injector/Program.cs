using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Memory;
using System.Security.Permissions;

namespace Serenity_injector
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Mem mem = new Mem();
            Console.Write("Process Name: ");
            string processName = Console.ReadLine();
            int pid = mem.getProcIDFromName(processName);
            mem.OpenProcess(pid);
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.FileName = "";
            openFileDialog.Filter = "DLL Files|*.dll";
            openFileDialog.Title = "Select DLL";
            openFileDialog.ShowDialog();
            string dllPath = openFileDialog.FileName.Replace('\\', '/');
            try {
                mem.InjectDLL(dllPath);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"The DLL {openFileDialog.SafeFileName}" +
                    $" has been injected into {processName} ({pid.ToString()})." +
                    $" Exiting in 5 seconds.");
                System.Threading.Thread.Sleep(5000);
                Environment.Exit(0);
                    }
            
            catch { Console.ForegroundColor = ConsoleColor.DarkRed; Console.WriteLine("Could not inject the DLL"); }
            Console.ReadLine();
        }
    }
}
