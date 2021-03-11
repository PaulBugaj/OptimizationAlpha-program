using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OptimizationGlobals;

namespace DisplayFunction
{
    class DisplayFunction3D
    {
        private FPanel fPanel;
        private string function_expression;

        public DisplayFunction3D(FPanel fPanel, string function_expression)
        {
            this.fPanel = fPanel;
            this.function_expression = function_expression;
            this.fPanel.SetFunction(this.function_expression);
        }

        public DisplayFunction3D(FPanel fPanel)
        {
            this.fPanel = fPanel;
            this.function_expression = string.Empty;
        }

        public void Load(string function_expression)
        {
            this.function_expression = function_expression;
            this.fPanel.SetFunction(this.function_expression);
        }

        public void Graph(Compartment AxisX, Compartment AxisY)
        {
            this.fPanel.Draw(AxisX.Min, AxisX.Max, AxisY.Min, AxisY.Max);
            this.fPanel.Invalidate();
        }

        public async Task GraphAync(Compartment AxisX, Compartment AxisY)
        {
            await Task.Run(() => this.fPanel.Draw(AxisX.Min, AxisX.Max, AxisY.Min, AxisY.Max));
            this.fPanel.Invalidate();
        }

        public void AddPoint(double x, double y, Color color, int size)
        {
            this.fPanel.AddPoint(x, y, color, size);
            this.fPanel.Invalidate();
        }

        public void Clear()
        {
            this.fPanel.ClearAll();
            this.fPanel.Invalidate();
        }

        public void ClearPoints()
        {
            this.fPanel.ClearPoints();
        }
    }
}
