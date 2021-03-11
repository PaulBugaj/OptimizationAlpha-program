using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OptimizationGlobals
{
    static class Debug
    {
        public static bool DebugState { get; set; } = false;
        public static bool TestState { get; set; } = false;

        public static void Show(string info)
        {
            if (DebugState == true)
            {
                MessageBox.Show(info);
            }
        }
    }
}
