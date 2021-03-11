using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using OptimizationGlobals;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;

namespace DisplayFunction
{
    class DisplayFunction2D
    {
        private Chart chart;
        private string function_expression;
        private Function function;

        public DisplayFunction2D(Chart chart)
        {
            this.chart = chart;
            this.chart.Series[0].ChartType = SeriesChartType.Line;
            this.chart.Series[1].ChartType = SeriesChartType.Point;
            this.function_expression = string.Empty;
            this.function = null;
        }

        public void Load(string function_expression)
        {
            this.function_expression = function_expression;
            this.function = new Function(this.function_expression, new List<string>() { "x" });
        }

        public void Graph(Compartment AxisX, Compartment AxisY)
        {
            List<string> arguments = new List<string>() { "x" };
            Function function = new Function(this.function_expression, arguments);
            this.chart.Series[0].Points.Clear();

            this.chart.ChartAreas[0].AxisX.Maximum = AxisX.Max;
            this.chart.ChartAreas[0].AxisX.Minimum = AxisX.Min;
            this.chart.ChartAreas[0].AxisY.Maximum = AxisY.Max;
            this.chart.ChartAreas[0].AxisY.Minimum = AxisY.Min;
            this.chart.Series[0].BorderWidth = 3;

            this.chart.ChartAreas[0].AxisX.Crossing = 0; // <--- These two lines
            chart.ChartAreas[0].AxisY.Crossing = 0;

            this.chart.ChartAreas[0].CursorX.IsUserEnabled = true;
            this.chart.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            this.chart.ChartAreas[0].AxisX.ScaleView.Zoomable = true;

            for (int i = (int)AxisX.Min; i <= (int)AxisX.Max; i++)
            {
                double functionValue = function.Evaluate(new List<double>() { i });
                this.chart.Series[0].Points.AddXY(i, functionValue);
                this.chart.Series[0].ChartType = SeriesChartType.Line;
            }
        }

        public void Graph(Compartment AxisX)
        {
            this.chart.Series[0].Points.Clear();

            this.chart.ChartAreas[0].AxisX.Maximum = AxisX.Max + 1;
            this.chart.ChartAreas[0].AxisX.Minimum = AxisX.Min - 1;
            this.chart.ChartAreas[0].AxisY.Maximum = 0;
            this.chart.ChartAreas[0].AxisY.Minimum = 0;
            this.chart.Series[0].BorderWidth = 5;
            this.chart.Legends[0].Name = this.function_expression;

            for (double i = this.chart.ChartAreas[0].AxisX.Minimum; i <= this.chart.ChartAreas[0].AxisX.Maximum; i+= 0.2)
            {
                double functionValue = function.Evaluate(new List<double>() { i });
                if(functionValue > chart.ChartAreas[0].AxisY.Maximum)
                {
                    this.chart.ChartAreas[0].AxisY.Maximum = functionValue;
                }
                if (functionValue < chart.ChartAreas[0].AxisY.Minimum)
                {
                    this.chart.ChartAreas[0].AxisY.Minimum = functionValue;
                }
                this.chart.Series[0].Points.AddXY(i, functionValue);
            }
        }

        public void AddPoint(double x)
        {
            int x_index = 0;
            for (double i = this.chart.ChartAreas[0].AxisX.Minimum; i <= this.chart.ChartAreas[0].AxisX.Maximum; i += 0.2)
            {
                if((x>= i) && (x <= (i+0.20)))
                {
                    break;
                }
                x_index++;
            }           
            if (x_index < this.chart.Series[0].Points.Count)
            {
                this.chart.Series[1].Points.AddXY(this.chart.Series[0].Points[x_index].XValue, this.chart.Series[0].Points[x_index].YValues[0]);
                this.chart.Series[1].Points[0].MarkerStyle = MarkerStyle.Circle;
                this.chart.Series[1].Points[0].MarkerSize = 15;
                this.chart.Series[1].Points[0].MarkerColor = Color.Red;
            }
        }

        public void ClearMarkedPoint()
        {
            this.chart.Series[1].Points.Clear();
            this.chart.Invalidate();
        }

        public void Clear()
        {
            this.chart.Series[0].Points.Clear();
            this.chart.Invalidate();
        }

        public void ClearAll()
        {
            this.chart.Series[1].Points.Clear();
            this.chart.Series[0].Points.Clear();
            this.chart.Invalidate();
        }

    }
}
