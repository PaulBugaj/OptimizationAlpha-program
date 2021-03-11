using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OptimizationGlobals;
using DisplayFunction;
using HeuristicAlgorithms;

namespace OptimizationAlpha
{
    public partial class Form1 : Form
    {
        private AlgorithmType _myAlgorithmType;
        private string _myFunctionExpression;
        private List<string> _myArgumentsSymbol;
        private List<Compartment> _myRanges;
        private Compartment _axisXRange;
        private Compartment _axisYRange;
        private FitnessPoint _bestValue;

        private DisplayFunction3D myDisplayFunction3D;
        private DisplayFunction2D myDisplayFunction2D;

        public Form1()
        {
            InitializeComponent();
            PostInitializeComponent();

            //disable legend in chart
            myChart.Series[0].IsVisibleInLegend = false;

            this.myDisplayFunction2D = new DisplayFunction2D(this.myChart);
            this.myDisplayFunction3D = new DisplayFunction3D(this.panel_3D);


            this.listView_results.SelectedIndexChanged += ListView_results_SelectedIndexChanged;
        }

        private void ListView_results_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listView_results.SelectedItems.Count > 0)
            {
                if (_myArgumentsSymbol.Count == 1)
                {
                    double x = double.Parse(this.listView_results.SelectedItems[0].SubItems[1].Text);
                    this.myDisplayFunction2D.ClearMarkedPoint();
                    this.myDisplayFunction2D.AddPoint(x);
                }
                else if (_myArgumentsSymbol.Count == 2)
                {
                    double x = double.Parse(this.listView_results.SelectedItems[0].SubItems[1].Text);
                    double y = double.Parse(this.listView_results.SelectedItems[0].SubItems[2].Text);
                    this.myDisplayFunction3D.ClearPoints();
                    this.myDisplayFunction3D.AddPoint(x,y, Color.Purple, 20);
                }
            }
        }

        private void PostInitializeComponent()
        {
            this.numericUpDown_X_range_from.Enabled = false;
            this.numericUpDown_X_range_to.Enabled = false;
            this.numericUpDown_Y_range_from.Enabled = false;
            this.numericUpDown_Y_range_to.Enabled = false;
            this.numericUpDown_Z_range_from.Enabled = false;
            this.numericUpDown_Z_range_to.Enabled = false;
        }

        private void textBox_function_Text_Click(object sender, EventArgs e)
        {
            if (this.textBox_function.Text.Contains("f(x)=") ||
                this.textBox_function.Text.Contains("f(x, y)=") ||
                this.textBox_function.Text.Contains("f(x, y, z)="))
            {
                textBox_function.Text = "";
            }
        }

        private void textBox_function_Leave(object sender, EventArgs e)
        {
            if (textBox_function.Text == "")
            {
                textBox_function.Text = listBox_variables.Items[listBox_variables.SelectedIndex].ToString();
            }
        }

        private void radioButton_one_var_CheckedChanged(object sender, EventArgs e)
        {
            listBox_variables.Enabled = true;

            if (radioButton_one_var.Checked == true)
            {
                _myAlgorithmType = AlgorithmType.Minimum;
            }
        }

        private void radioButton_two_var_CheckedChanged(object sender, EventArgs e)
        {
            listBox_variables.Enabled = true;
            if (radioButton_two_var.Checked == true)
            {
                _myAlgorithmType = AlgorithmType.Maximum;
            }
        }

        private void listBox_variables_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox_function.Enabled = true;
            button_search.Enabled = true;
            button_read_from_file.Enabled = true;
            textBox_function.Text = listBox_variables.Items[listBox_variables.SelectedIndex].ToString();

            switch (listBox_variables.SelectedIndex)
            {
                case 0:
                    this.numericUpDown_X_range_from.Enabled = true;
                    this.numericUpDown_X_range_to.Enabled = true;
                    this.numericUpDown_Y_range_from.Enabled = false;
                    this.numericUpDown_Y_range_to.Enabled = false;
                    this.numericUpDown_Z_range_from.Enabled = false;
                    this.numericUpDown_Z_range_to.Enabled = false;
                    this.button_read_from_file.Enabled = true;
                    break;
                case 1:
                    this.numericUpDown_X_range_from.Enabled = true;
                    this.numericUpDown_X_range_to.Enabled = true;
                    this.numericUpDown_Y_range_from.Enabled = true;
                    this.numericUpDown_Y_range_to.Enabled = true;
                    this.numericUpDown_Z_range_from.Enabled = false;
                    this.numericUpDown_Z_range_to.Enabled = false;
                    this.button_read_from_file.Enabled = false;
                    break;
                case 2:
                    this.numericUpDown_X_range_from.Enabled = true;
                    this.numericUpDown_X_range_to.Enabled = true;
                    this.numericUpDown_Y_range_from.Enabled = true;
                    this.numericUpDown_Y_range_to.Enabled = true;
                    this.numericUpDown_Z_range_from.Enabled = true;
                    this.numericUpDown_Z_range_to.Enabled = true;
                    this.button_read_from_file.Enabled = false;
                    break;
            }

        }

        private FitnessPoint FindBestPointInRange(AlgorithmType algorithmType, List<FitnessPoint> options, params Compartment[] compartments)
        {
            FitnessPoint result = null;
            FitnessPoint tempResult = null;

            int numberOfRanges = compartments.Length;

            List<FitnessPoint> cloneOptions = new List<FitnessPoint>(options);
            switch (algorithmType)
            {
                case AlgorithmType.Maximum:
                    {
                        FitnessPoint tempBest = cloneOptions.Max();
                        for (int i = 0; i < numberOfRanges; i++)
                        {
                            if(tempBest.Axis.Values[i] >= compartments[i].Min && tempBest.Axis.Values[i] <= compartments[i].Max)
                            {
                                tempResult = new FitnessPoint(tempBest);
                            }
                            else
                            {
                                i = -1;
                                cloneOptions.Remove(tempBest);
                                tempBest = cloneOptions.Max();
                                tempResult = null;
                            }
                        }
                        result = tempResult;
                        break;
                    }
                case AlgorithmType.Minimum:
                    {
                        FitnessPoint tempBest = cloneOptions.Min();
                        for (int i = 0; i < numberOfRanges; i++)
                        {
                            if (tempBest.Axis.Values[i] >= compartments[i].Min && tempBest.Axis.Values[i] <= compartments[i].Max)
                            {
                                tempResult = new FitnessPoint(tempBest);
                            }
                            else
                            {
                                i = -1;
                                cloneOptions.Remove(tempBest);
                                tempBest = cloneOptions.Min();
                                tempResult = null;
                            }
                        }
                        result = tempResult;
                        break;
                    }
            }

            return result;
        }

        private async void button_search_Click(object sender, EventArgs e)
        {
            this.button_search.Enabled = false;
            // CODE FOR SEARCHING MIN/MAX ALGHORITM
            // clear listbox results
            listView_results.Items.Clear();

            HeuristicAlgorithms myHeuristicAlgorithms = new HeuristicAlgorithms();
            // list of points
            List<FitnessPoint> myListOfFitnessPoints = new List<FitnessPoint>();
            // best fitness point
            _bestValue = new FitnessPoint();

            // assign function expresion to variable
            _myFunctionExpression = textBox_function.Text;

            // create list of argument symbols
            AssignArgumentSymbols();

            // create list of ranges for each variable
            _myRanges = new List<Compartment>();

            // validation range of each variable
            switch (_myArgumentsSymbol.Count)
            {
                case 1:
                    {
                        if(!ValidateRangeValues(this.numericUpDown_X_range_from.Value, this.numericUpDown_X_range_to.Value))
                        {
                            this.button_search.Enabled = true;
                            return;
                        }
                        break;
                    }
                case 2:
                    {
                        if(!ValidateRangeValues(this.numericUpDown_X_range_from.Value, this.numericUpDown_X_range_to.Value))
                        {
                            this.button_search.Enabled = true;
                            return;
                        }
                        if(!ValidateRangeValues(this.numericUpDown_Y_range_from.Value, this.numericUpDown_Y_range_to.Value))
                        {
                            this.button_search.Enabled = true;
                            return;
                        }
                        break;
                    }
                case 3:
                    {
                        if(!ValidateRangeValues(this.numericUpDown_X_range_from.Value, this.numericUpDown_X_range_to.Value))
                        {
                            this.button_search.Enabled = true;
                            return;
                        }
                        if(!ValidateRangeValues(this.numericUpDown_Y_range_from.Value, this.numericUpDown_Y_range_to.Value))
                        {
                            this.button_search.Enabled = true;
                            return;
                        }
                        if(!ValidateRangeValues(this.numericUpDown_Z_range_from.Value, this.numericUpDown_Z_range_to.Value))
                        {
                            this.button_search.Enabled = true;
                            return;
                        }
                        break;
                    }
                default:
                    break;
            }

            if (!IsDigitsOnly(_myFunctionExpression))
            {


                try
                {
                    // find list of fitness points
                    myListOfFitnessPoints = await myHeuristicAlgorithms.GetAllOptimalPointsAsync(_myAlgorithmType, _myFunctionExpression, _myArgumentsSymbol, _myRanges);
                    // find best fitness point (minimum or maximum)
                    switch(_myArgumentsSymbol.Count)
                    {
                        case 1:
                            {
                                _bestValue = this.FindBestPointInRange(_myAlgorithmType, myListOfFitnessPoints, new Compartment((double)this.numericUpDown_X_range_from.Value, (double)this.numericUpDown_X_range_to.Value));
                                break;
                            }
                        case 2:
                            {
                                _bestValue = this.FindBestPointInRange(_myAlgorithmType, myListOfFitnessPoints, 
                                    new Compartment((double)this.numericUpDown_X_range_from.Value, (double)this.numericUpDown_X_range_to.Value),
                                    new Compartment((double)this.numericUpDown_Y_range_from.Value, (double)this.numericUpDown_Y_range_to.Value));
                                break;
                            }
                        case 3:
                            {
                                _bestValue = this.FindBestPointInRange(_myAlgorithmType, myListOfFitnessPoints,
                                    new Compartment((double)this.numericUpDown_X_range_from.Value, (double)this.numericUpDown_X_range_to.Value),
                                    new Compartment((double)this.numericUpDown_Y_range_from.Value, (double)this.numericUpDown_Y_range_to.Value),
                                    new Compartment((double)this.numericUpDown_Z_range_from.Value, (double)this.numericUpDown_Z_range_to.Value));
                                break;
                            }
                    }

                }
                catch (AlgorithmException ex)
                {
                    switch (ex.Fail)
                    {
                        case AlgorithmExceptionType.ParametersNotSeted:
                            AddResult("Parameters no set");
                            break;
                        case AlgorithmExceptionType.FunctionNotExecuted:
                            AddResult("Function not executed");
                            break;
                        case AlgorithmExceptionType.DifferenceArguments:
                            AddResult("Difference arguments");
                            break;
                        case AlgorithmExceptionType.FunctionNotSeted:
                            AddResult("Function no set");
                            break;
                        case AlgorithmExceptionType.WrongParametersArguments:
                            AddResult("Wrong parameters arguments");
                            break;
                        case AlgorithmExceptionType.BadRanges:
                            AddResult("Bad ranges");
                            break;
                    }
                    this.button_search.Enabled = true;
                    return;
                }

                double twinRangeFitness = (this.numericUpDown_precison.Value == 0) ? 0 : 1;
                for (int i=0; i< (int)this.numericUpDown_precison.Value; i++)
                {
                    twinRangeFitness /= 10;
                }
                twinRangeFitness = _bestValue.Fitness - twinRangeFitness;
                foreach (FitnessPoint item in myListOfFitnessPoints)
                {
                    if(item.Fitness == _bestValue.Fitness)
                    {
                        AddResult(item.Fitness, item.Axis.Values, Color.Green);
                        continue;
                    }
                    if (Math.Round(item.Fitness, (int)this.numericUpDown_precison.Value, MidpointRounding.AwayFromZero) <= _bestValue.Fitness &&
                        Math.Round(item.Fitness, (int)this.numericUpDown_precison.Value, MidpointRounding.AwayFromZero) >= twinRangeFitness)
                    {
                        AddResult(item.Fitness, item.Axis.Values, Color.Orange);
                    }
                    else
                    {
                        AddResult(item.Fitness, item.Axis.Values, Color.White);
                    }
                }

            }

            // display graph for function with 1 variable of for constant functions
            if (_myArgumentsSymbol.Count == 1)
            {
                this.myDisplayFunction2D.ClearAll();
                this.myDisplayFunction2D.Load(_myFunctionExpression);
                // set axis X range
                _axisXRange = new Compartment(_myRanges[0].Min, _myRanges[0].Max);

                // show function graph
                this.myDisplayFunction2D.Graph(_axisXRange);
            }
            // display graph for function with 2 variable
            else if (_myArgumentsSymbol.Count == 2)
            {
                this.myDisplayFunction3D.ClearPoints();
                this.myDisplayFunction3D.Load(_myFunctionExpression);
                // set axis X range
                _axisXRange = new Compartment(_myRanges[0].Min, _myRanges[0].Max);
                // set axis Y range
                _axisYRange = new Compartment(_myRanges[1].Min, _myRanges[1].Max);
                // show function graph
                await this.myDisplayFunction3D.GraphAync(_axisXRange, _axisYRange);
            }
            this.button_search.Enabled = true;
        }

        bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9' || c == ',')
                    return false;
            }

            return true;
        }

        private void AssignArgumentSymbols()
        {
            // assign argument symbols to variable
            switch (listBox_variables.SelectedIndex)
            {
                case 0:
                    _myArgumentsSymbol = new List<string> { "x" };
                    break;
                case 1:
                    _myArgumentsSymbol = new List<string> { "x", "y" };
                    break;
                case 2:
                    _myArgumentsSymbol = new List<string> { "x", "y", "z" };
                    break;
                default:
                    break;
            }
        }

        private void AddResult(string s)
        {
            MessageBox.Show(s);
        }

        private void AddResult(double val, List<double> values, Color color)
        {
            ListViewItem listViewItem = new ListViewItem(Math.Round(val, (int)this.numericUpDown_precison.Value, MidpointRounding.AwayFromZero).ToString());

            for (int i = 0; i < values.Count; i++)
            {
                listViewItem.SubItems.Add(Math.Round(values[i], (int)this.numericUpDown_precison.Value, MidpointRounding.AwayFromZero).ToString());
            }
            this.listView_results.Items.Add(listViewItem);
            if (color != Color.White)
            {
                this.listView_results.Items[this.listView_results.Items.Count - 1].BackColor = color;
            }
        }

        private bool ValidateRangeValues(decimal valueFrom, decimal valueTo)
        {
            if (valueFrom >= valueTo)
            {
                MessageBox.Show("From value can not be higher than to value. Try again");
                return false;
            }
            else
            {
                _myRanges.Add(new Compartment((double)valueFrom, (double)valueTo));
                return true;
            }
        }

        private void button_help_Click(object sender, EventArgs e)
        {
            // DISPLAY HELP
            HelpForm form = new HelpForm();
            form.Show();
        }

        private void button_read_from_file_Click(object sender, EventArgs e)
        {
            // READ FROM FILE
            Communication.LagrangeInterpolation lagrangeInterpolation = new Communication.LagrangeInterpolation();

            string path = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // get the path of specified file
                    path = openFileDialog.FileName;
                }
            }

            try
            {
                if (lagrangeInterpolation.LoadFile(path, Communication.FileType.TXT))
                {
                    if (lagrangeInterpolation.GenerateFunctionExpression())
                    {
                        textBox_function.Text = lagrangeInterpolation.FunctionExpression;
                    }
                }
            }

            catch (Communication.CommunicationException ex)
            {
                switch (ex.Fail)
                {
                    case Communication.CommunicationExceptionType.CannotLoadFile:
                        AddResult("Can not load file");
                        break;
                    case Communication.CommunicationExceptionType.CannotReadLine:
                        AddResult("Can not read line");
                        break;
                }
            }


        }
    }
}
