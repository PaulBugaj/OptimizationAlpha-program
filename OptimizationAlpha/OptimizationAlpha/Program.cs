using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using OptimizationTests;
using OptimizationGlobals;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using System.IO;
using System.Text;

namespace OptimizationAlpha
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] argv)
        {
            if(argv.Length != 0)
            {
                if(argv[0] == "--tests")
                {
                    Debug.TestState = true;
                }
            }
            //Debug.TestState = true;
            if (!Debug.TestState)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new TestForm());
            }
        }
    }
}
