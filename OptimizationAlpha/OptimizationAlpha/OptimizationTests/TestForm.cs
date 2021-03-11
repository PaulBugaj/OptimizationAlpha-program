using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OptimizationTests
{
    public partial class TestForm : Form
    {
        private Tests tests;
        private string selectedTestName;
        public TestForm()
        {
            InitializeComponent();
            this.tests = new Tests();
            this.selectedTestName = string.Empty;

            this.button_beginTest.Enabled = false;

            foreach(string test in this.tests.AllTests.Keys)
            {
                this.comboBox_allTests.Items.Add(test);
            }

            this.comboBox_allTests.SelectedIndexChanged += ComboBox_allTests_SelectedIndexChanged;
        }

        private void ComboBox_allTests_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBox_allTests.SelectedIndex >= 0)
            {
                this.selectedTestName = this.comboBox_allTests.Items[this.comboBox_allTests.SelectedIndex].ToString();
                this.button_beginTest.Enabled = true;
            }
        }

        private void button_beginTest_Click(object sender, EventArgs e)
        {
            if(this.comboBox_allTests.SelectedIndex >= 0)
            {
                if (this.tests.AllTests[this.selectedTestName] == true)
                {
                    string filePath = string.Empty;
                    using (OpenFileDialog openFileDialog = new OpenFileDialog())
                    {
                        openFileDialog.ShowDialog();
                        filePath = openFileDialog.FileName;
                    }

                    if (filePath != string.Empty)
                    {
                        if (this.comboBox_allTests.SelectedIndex == 0)
                        {
                            HATestUnit hATestUnit = new HATestUnit();
                            if (!XmlManager.Load<HATestUnit>(filePath, out hATestUnit))
                            {
                                filePath = string.Empty;
                            }
                            else
                            {
                                this.button_beginTest.Enabled = false;
                                this.tests.Begin(this.comboBox_allTests.SelectedIndex, this.listBox_testLogs, hATestUnit);
                            }
                        }
                        else if (this.comboBox_allTests.SelectedIndex == 1)
                        {
                            LagrangeInterpolationTestUnit liTestUnit = new LagrangeInterpolationTestUnit();
                            if (!XmlManager.Load<LagrangeInterpolationTestUnit>(filePath, out liTestUnit))
                            {
                                filePath = string.Empty;
                            }
                            else
                            {
                                this.button_beginTest.Enabled = false;
                                this.tests.Begin(this.comboBox_allTests.SelectedIndex, this.listBox_testLogs, liTestUnit);
                            }
                        }
                    }
                }
                else
                {
                    this.tests.Begin(this.comboBox_allTests.SelectedIndex, this.listBox_testLogs);
                }
            }
        }
    }
}
