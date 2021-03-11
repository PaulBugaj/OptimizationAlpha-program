using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OptimizationAlpha
{
    public partial class HelpForm : Form
    {
        public HelpForm()
        {
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listOfFunctionsBox.SelectedIndex == listOfFunctionsBox.Items.Count - 1)
            {
                DialogResult dialogResult = MessageBox.Show("Do you want visit site with the documentation?", "Go to link", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    ProcessStartInfo sInfo = new ProcessStartInfo("https://bit.ly/2Egsnpq");
                    Process.Start(sInfo);
                }
            }
        }
    }
}
