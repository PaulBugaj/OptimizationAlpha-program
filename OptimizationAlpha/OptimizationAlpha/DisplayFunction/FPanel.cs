using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.ObjectModel;
using OptimizationGlobals;

namespace DisplayFunction
{
    public sealed class FPanel : Panel
    {
        private class FPointUint
        {
            public double X { get; set; }
            public double Y { get; set; }

            public double OriginX { get; set; }
            public double OriginY { get; set; }
            public double Unit { get; }

            public FPointUint(double x, double y, double origin_x, double origin_y, double unit)
            {
                this.X = x;
                this.Y = y;
                this.OriginX = origin_x;
                this.OriginY = origin_y;
                this.Unit = unit;
            }
        }
        private class FPointColor
        {
            public double X { get; set; }
            public double Y { get; set; }
            public double Value { get; set; }
            public Color Color { get; set; }

            public FPointColor(double x, double y, double value, Color color)
            {
                this.X = x;
                this.Y = y;
                this.Color = color;
                this.Value = value;
            }
        }
        private class CenterPoint
        {
            double x_y;
            double x_y_value;
            public double XY { get { return this.x_y; } set { this.x_y = value; } }
            public double XYValue { get { return this.x_y_value; } set { this.x_y_value = value; } }

            public CenterPoint(double x_y, double x_y_value)
            {
                this.x_y = x_y;
                this.x_y_value = x_y_value;
            }
        }
        private bool is_have_function;
        private bool is_have_background;
        private double Xmin, Xmax, Ymin, Ymax, Step_X, Step_Y, axis_unit_X, axis_unit_Y, MAX_X, MAX_Y;
        private int color_background_axis_step, color_background_axis_size_x, color_background_axis_size_y;
        private CenterPoint Center_X, Center_Y;
        private List<FPointUint> axis_unit_points_x, axis_unit_points_y;
        private Font font_x, font_y;
        private FPointColor[,] background_colour_points;
        private Function function;
        private List<FPointColor> points;

        public int ColorBackgroundAxisStep
        {
            get { return this.color_background_axis_step; }
            set
            {
                if (value > 0)
                    this.color_background_axis_step = value;

                if (this.is_have_function)
                {
                    this.PrepereBackground();
                    this.Invalidate();
                }
            }
        }

        public FPanel()
        {
            this.DoubleBuffered = true;
            this.font_x = new Font("Arial", 8);
            this.font_y = new Font("Arial", 8);
            this.points = new List<FPointColor>();
            this.color_background_axis_step = 1;
            this.background_colour_points = null;
            this.is_have_background = false;
            this.axis_unit_points_x = new List<FPointUint>();
            this.axis_unit_points_y = new List<FPointUint>();
            this.is_have_function = false;
            this.function = null;
            this.Center_X = null;
            this.Center_Y = null;
            this.Xmin = 0;
            this.Xmax = 10;
            this.Ymin = 0;
            this.Ymax = 10;
            this.FindStepsAndCenter();

            this.Paint += FPanel_Paint;
            this.SizeChanged += FPanel_SizeChanged;
            this.Invalidate();
        }

        public void AddPoint(double x, double y, Color color, int size)
        {
            double help_x = Math.Sqrt(Math.Pow(Math.Abs(x) - Math.Abs(this.Center_X.XYValue), 2));
            double jump_x = this.Width / this.MAX_X;
            if (x >= 0)
            {
                help_x = this.Center_X.XY + (help_x * jump_x);
            }
            else
            {
                help_x = this.Center_X.XY - (help_x * jump_x);
            }
            double help_y = Math.Sqrt(Math.Pow(Math.Abs(y) - Math.Abs(this.Center_Y.XYValue), 2));
            double jump_y = this.Height / this.MAX_Y;
            if (y >= 0)
            {
                help_y = this.Center_Y.XY - (help_y * jump_y);
            }
            else
            {
                help_y = this.Center_Y.XY + (help_y * jump_y);
            }
            this.points.Add(new FPointColor(help_x- (int)Math.Floor(size / (decimal)2), help_y- (int)Math.Floor(size / (decimal)2), size, color));
        }

        public void ClearPoints()
        {
            this.points.Clear();
        }

        public void ClearAll()
        {
            this.font_x = new Font("Arial", 8);
            this.font_y = new Font("Arial", 8);
            this.background_colour_points = null;
            this.Center_X = null;
            this.Center_Y = null;
            this.is_have_background = false;
            this.axis_unit_points_x.Clear();
            this.axis_unit_points_y.Clear();
            this.is_have_function = false;
            this.function = null;
            this.Xmin = 0;
            this.Xmax = 10;
            this.Ymin = 0;
            this.Ymax = 10;
            this.points.Clear();
            this.FindStepsAndCenter();

            this.Invalidate();
        }

        public void SetFunction(string function_expression)
        {
            this.function = new Function(function_expression, new List<string>() { "x", "y" });
            this.is_have_function = true;
        }

        private void FPanel_SizeChanged(object sender, EventArgs e)
        {
            if (this.Height > 20 && this.Width > 20)
                this.FindStepsAndCenter();
            this.Invalidate();
        }

        private void FPanel_Paint(object sender, PaintEventArgs e)
        {
            //background*********************
            if (this.is_have_function && this.is_have_background)
            {
                for (int i = 0; i < this.color_background_axis_size_x; i++)
                {
                    for (int j = 0; j < this.color_background_axis_size_y; j++)
                    {
                        e.Graphics.FillRectangle(new SolidBrush(this.background_colour_points[i, j].Color), new Rectangle((int)this.background_colour_points[i, j].X, (int)this.background_colour_points[i, j].Y, this.color_background_axis_step, this.color_background_axis_step));
                    }
                }
            }

            //axis***************************
            e.Graphics.DrawLine(new Pen(Color.Black, 2), new Point((int)this.Center_X.XY, 0), new Point((int)this.Center_X.XY, this.Height));
            e.Graphics.DrawLine(new Pen(Color.Black, 2), new Point(0, (int)this.Center_Y.XY), new Point(this.Width, (int)this.Center_Y.XY));
            foreach (FPointUint point in this.axis_unit_points_x)
            {
                e.Graphics.DrawString(point.Unit.ToString(), this.font_x, Brushes.Black, new Point((int)point.X, (int)point.Y));
                e.Graphics.DrawLine(new Pen(Color.Black, 1), new Point((int)point.OriginX, (int)point.OriginY + 2), new Point((int)point.OriginX, (int)point.OriginY - 2));
            }
            foreach (FPointUint point in this.axis_unit_points_y)
            {
                e.Graphics.DrawString(point.Unit.ToString(), this.font_y, Brushes.Black, new Point((int)point.X, (int)point.Y));
                e.Graphics.DrawLine(new Pen(Color.Black, 1), new Point((int)point.OriginX + 2, (int)point.OriginY), new Point((int)point.OriginX - 2, (int)point.OriginY));
            }

            //points**********************
            foreach (FPointColor point in this.points)
            {
                e.Graphics.FillEllipse(new SolidBrush(point.Color), new Rectangle((int)point.X, (int)point.Y, (int)point.Value, (int)point.Value));
            }
        }

        public void Draw(double Xmin, double Xmax, double Ymin, double Ymax)
        {
            this.Xmin = Xmin;
            this.Xmax = Xmax;
            this.Ymin = Ymin;
            this.Ymax = Ymax;
            this.FindStepsAndCenter();
            if (this.is_have_function)
            {
                this.PrepereBackground();
                this.is_have_background = true;
            }
            this.Invalidate();
        }

        private void FindStepsAndCenter()
        {
            this.axis_unit_points_x.Clear();
            this.axis_unit_points_y.Clear();

            if (this.Xmax >= 0 && this.Xmin >= 0)
            {
                this.MAX_X = this.Xmax - this.Xmin;
            }
            else if (this.Xmax <= 0 && this.Xmin <= 0)
            {
                this.MAX_X = Math.Abs(Xmin) - Math.Abs(Xmax);
            }
            else if (this.Xmin <= 0 && this.Xmax >= 0)
            {
                this.MAX_X = Math.Abs(Xmin) + Math.Abs(Xmax);
            }

            if (this.Ymax >= 0 && this.Ymin >= 0)
            {
                this.MAX_Y = this.Ymax - this.Ymin;
            }
            else if (this.Ymax <= 0 && this.Ymin <= 0)
            {
                this.MAX_Y = Math.Abs(Ymin) - Math.Abs(Ymax);
            }
            else if (this.Ymin <= 0 && this.Ymax >= 0)
            {
                this.MAX_Y = Math.Abs(Ymin) + Math.Abs(Ymax);
            }
            //step*************************************
            this.Step_X = this.MAX_X / this.Width;
            this.Step_Y = this.MAX_Y / this.Height;
            //X****************************************
            if (this.Xmax >= 0 && this.Xmin >= 0)
            {
                this.Center_X = new CenterPoint(5, this.Xmin);
            }
            else if (this.Xmax <= 0 && this.Xmin <= 0)
            {
                this.Center_X = new CenterPoint(this.Width - 5, this.Xmax);
            }
            else if (this.Xmin <= 0 && this.Xmax >= 0)
            {
                this.Center_X = new CenterPoint(Math.Abs(Xmin) / this.Step_X, 0);
            }
            //Y********************************
            if (this.Ymax >= 0 && this.Ymin >= 0)
            {
                this.Center_Y = new CenterPoint(this.Height - 5, this.Ymin);
            }
            else if (this.Ymax <= 0 && this.Ymin <= 0)
            {
                this.Center_Y = new CenterPoint(5, this.Ymax);
            }
            else if (this.Ymin <= 0 && this.Ymax >= 0)
            {
                this.Center_Y = new CenterPoint(Math.Abs(this.Ymax) / this.Step_Y, 0);
            }
            //axis_step**********************************
            double number_step_x = (this.Width - 10) / MAX_X;
            double number_step_y = (this.Height - 10) / MAX_Y;

            if (MAX_X < 2)
            {
                this.axis_unit_X = 0.1;
                this.font_x = new Font("Arial", 10, FontStyle.Bold);
                number_step_x *= this.axis_unit_X;
            }
            else if (MAX_X >= 2 && MAX_X < 10)
            {
                this.axis_unit_X = 0.5;
                this.font_x = new Font("Arial", 9, FontStyle.Bold);
                number_step_x *= this.axis_unit_X;
            }
            else if (MAX_X >= 10 && MAX_X < 30)
            {
                this.axis_unit_X = 1;
                this.font_x = new Font("Arial", 8, FontStyle.Bold);
                number_step_x *= this.axis_unit_X;
            }
            else if (MAX_X >= 30 && MAX_X < 100)
            {
                this.axis_unit_X = 2;
                this.font_x = new Font("Arial", 7, FontStyle.Bold);
                number_step_x *= this.axis_unit_X;
            }
            else
            {
                this.axis_unit_X = 5;
                this.font_x = new Font("Arial", 6, FontStyle.Bold);
                number_step_x *= this.axis_unit_X;
            }

            if (MAX_Y < 2)
            {
                this.axis_unit_Y = 0.1;
                this.font_y = new Font("Arial", 10, FontStyle.Bold);
                number_step_y *= this.axis_unit_Y;
            }
            else if (MAX_Y >= 2 && MAX_Y < 10)
            {
                this.axis_unit_Y = 0.5;
                this.font_y = new Font("Arial", 9, FontStyle.Bold);
                number_step_y *= this.axis_unit_Y;
            }
            else if (MAX_Y >= 10 && MAX_Y < 30)
            {
                this.axis_unit_Y = 1;
                this.font_y = new Font("Arial", 8, FontStyle.Bold);
                number_step_y *= this.axis_unit_Y;
            }
            else if (MAX_Y >= 30 && MAX_Y < 100)
            {
                this.axis_unit_Y = 2;
                this.font_y = new Font("Arial", 7, FontStyle.Bold);
                number_step_y *= this.axis_unit_Y;
            }
            else
            {
                this.axis_unit_Y = 5;
                this.font_y = new Font("Arial", 6, FontStyle.Bold);
                number_step_y *= this.axis_unit_Y;
            }

            if (this.Xmax >= 0 && this.Xmin >= 0)
            {
                double start_to_right = this.Center_X.XY;
                double unit_to_right = this.Xmin;
                while (true)
                {
                    start_to_right += number_step_x;
                    unit_to_right += this.axis_unit_X;
                    if (start_to_right > this.Width)
                        break;

                    if (this.Center_Y.XY >= (this.Height - 15))
                        this.axis_unit_points_x.Add(new FPointUint(start_to_right - 5, this.Center_Y.XY - 15, start_to_right, this.Center_Y.XY, unit_to_right));
                    else
                        this.axis_unit_points_x.Add(new FPointUint(start_to_right - 5, this.Center_Y.XY, start_to_right, this.Center_Y.XY, unit_to_right));
                }
            }
            else if (this.Xmax <= 0 && this.Xmin <= 0)
            {
                double start_to_left = this.Center_X.XY;
                double unit_to_left = this.Xmax;
                while (true)
                {
                    start_to_left -= number_step_x;
                    unit_to_left -= this.axis_unit_X;
                    if (start_to_left < 0)
                        break;

                    if (this.Center_Y.XY >= (this.Height - 15))
                        this.axis_unit_points_x.Add(new FPointUint(start_to_left - 5, this.Center_Y.XY - 15, start_to_left, this.Center_Y.XY, unit_to_left));
                    else
                        this.axis_unit_points_x.Add(new FPointUint(start_to_left - 5, this.Center_Y.XY, start_to_left, this.Center_Y.XY, unit_to_left));
                }
            }
            else if (this.Xmin <= 0 && this.Xmax >= 0)
            {
                double start_to_right = this.Center_X.XY;
                double unit_to_right = 0;
                while (true)
                {
                    start_to_right += number_step_x;
                    unit_to_right += this.axis_unit_X;
                    if (start_to_right > this.Width)
                        break;

                    if (this.Center_Y.XY >= (this.Height - 15))
                        this.axis_unit_points_x.Add(new FPointUint(start_to_right - 5, this.Center_Y.XY - 15, start_to_right, this.Center_Y.XY, unit_to_right));
                    else
                        this.axis_unit_points_x.Add(new FPointUint(start_to_right - 5, this.Center_Y.XY, start_to_right, this.Center_Y.XY, unit_to_right));
                }

                double start_to_left = this.Center_X.XY;
                double unit_to_left = 0;
                while (true)
                {
                    start_to_left -= number_step_x;
                    unit_to_left -= this.axis_unit_X;
                    if (start_to_left < 0)
                        break;

                    if (this.Center_Y.XY >= (this.Height - 15))
                        this.axis_unit_points_x.Add(new FPointUint(start_to_left - 5, this.Center_Y.XY - 15, start_to_left, this.Center_Y.XY, unit_to_left));
                    else
                        this.axis_unit_points_x.Add(new FPointUint(start_to_left - 5, this.Center_Y.XY, start_to_left, this.Center_Y.XY, unit_to_left));
                }
            }

            if (this.Ymax >= 0 && this.Ymin >= 0)
            {
                double start_to_up = this.Center_Y.XY;
                double unit_to_up = this.Ymin;
                while (true)
                {
                    start_to_up -= number_step_y;
                    unit_to_up += this.axis_unit_Y;
                    if (start_to_up < 0)
                        break;

                    if (this.Center_X.XY >= (this.Width - 15))
                        this.axis_unit_points_y.Add(new FPointUint(this.Center_X.XY - 15, start_to_up - 5, this.Center_X.XY, start_to_up, unit_to_up));
                    else
                        this.axis_unit_points_y.Add(new FPointUint(this.Center_X.XY, start_to_up - 5, this.Center_X.XY, start_to_up, unit_to_up));
                }
            }
            else if (this.Ymax <= 0 && this.Ymin <= 0)
            {
                double start_to_down = this.Center_Y.XY;
                double unit_to_down = this.Ymax;
                while (true)
                {
                    start_to_down += number_step_y;
                    unit_to_down -= this.axis_unit_Y;
                    if (start_to_down > this.Height)
                        break;

                    if (this.Center_X.XY >= (this.Width - 15))
                        this.axis_unit_points_y.Add(new FPointUint(this.Center_X.XY - 15, start_to_down - 5, this.Center_X.XY, start_to_down, unit_to_down));
                    else
                        this.axis_unit_points_y.Add(new FPointUint(this.Center_X.XY, start_to_down - 5, this.Center_X.XY, start_to_down, unit_to_down));
                }
            }
            else if (this.Ymin <= 0 && this.Ymax >= 0)
            {
                double start_to_up = this.Center_Y.XY;
                double unit_to_up = 0;
                while (true)
                {
                    start_to_up -= number_step_y;
                    unit_to_up += this.axis_unit_Y;
                    if (start_to_up < 0)
                        break;

                    if (this.Center_X.XY >= (this.Width - 15))
                        this.axis_unit_points_y.Add(new FPointUint(this.Center_X.XY - 15, start_to_up - 5, this.Center_X.XY, start_to_up, unit_to_up));
                    else
                        this.axis_unit_points_y.Add(new FPointUint(this.Center_X.XY, start_to_up - 5, this.Center_X.XY, start_to_up, unit_to_up));
                }

                double start_to_down = this.Center_Y.XY;
                double unit_to_down = 0;
                while (true)
                {
                    start_to_down += number_step_y;
                    unit_to_down -= this.axis_unit_Y;
                    if (start_to_down > this.Height)
                        break;

                    if (this.Center_X.XY >= (this.Width - 15))
                        this.axis_unit_points_y.Add(new FPointUint(this.Center_X.XY - 15, start_to_down - 5, this.Center_X.XY, start_to_down, unit_to_down));
                    else
                        this.axis_unit_points_y.Add(new FPointUint(this.Center_X.XY, start_to_down - 5, this.Center_X.XY, start_to_down, unit_to_down));
                }
            }
        }

        private void PrepereBackground()
        {
            this.color_background_axis_size_x = this.Width / this.color_background_axis_step;
            this.color_background_axis_size_y = this.Height / this.color_background_axis_step;
            this.background_colour_points = new FPointColor[this.color_background_axis_size_x, this.color_background_axis_size_y];
            Color[] colors = new Color[10] { Color.Blue, Color.SkyBlue, Color.Aquamarine, Color.LightGreen, Color.Green, Color.YellowGreen, Color.Yellow, Color.Orange, Color.OrangeRed, Color.Red };
            double max_background_value = double.MinValue;
            double min_background_value = double.MaxValue;
            double jump_step_x, jump_step_y, step_x, step_y;

            step_x = this.MAX_X / this.color_background_axis_size_x;
            step_y = this.MAX_Y / this.color_background_axis_size_y;
            jump_step_x = this.Xmin - step_x;
            jump_step_y = this.Ymax + step_y;

            for (int i = 0; i < this.color_background_axis_size_x; i++)
            {
                jump_step_x += step_x;
                Parallel.For(0, this.color_background_axis_size_y, j =>
                {
                    double jump_step_y_parallel = jump_step_y - ((j + 1) * step_y);
                    double function_value = this.function.Evaluate(new List<double>() { jump_step_x + (step_x / 2), jump_step_y_parallel + (step_y / 2) });
                    this.background_colour_points[i, j] = new FPointColor(i * this.color_background_axis_step,
                        j * this.color_background_axis_step,
                        function_value, Color.Red);

                    if (this.background_colour_points[i, j].Value > max_background_value)
                        max_background_value = this.background_colour_points[i, j].Value;
                    if (this.background_colour_points[i, j].Value < min_background_value)
                        min_background_value = this.background_colour_points[i, j].Value;
                });
            }

            double jump_value = 0;
            if (max_background_value >= 0 && min_background_value >= 0)
            {
                jump_value = max_background_value - min_background_value;
            }
            else if (max_background_value <= 0 && min_background_value <= 0)
            {
                jump_value = Math.Abs(min_background_value) - Math.Abs(max_background_value);
            }
            else if (min_background_value <= 0 && max_background_value >= 0)
            {
                jump_value = Math.Abs(min_background_value) + Math.Abs(max_background_value);
            }
            jump_value /= 10;

            for (int i = 0; i < this.color_background_axis_size_x; i++)
            {
                for (int j = 0; j < this.color_background_axis_size_y; j++)
                {
                    for (int k = 0; k < 10; k++)
                    {
                        if (this.background_colour_points[i, j].Value >= (min_background_value + (jump_value * k))
                            && this.background_colour_points[i, j].Value <= (min_background_value + (jump_value * (k + 1))))
                        {
                            this.background_colour_points[i, j].Color = colors[k];
                            break;
                        }
                    }
                }
            }
        }
    }
}
