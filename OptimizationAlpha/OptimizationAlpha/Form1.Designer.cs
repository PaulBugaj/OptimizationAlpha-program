namespace OptimizationAlpha
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.textBox_function = new System.Windows.Forms.TextBox();
            this.label_results = new System.Windows.Forms.Label();
            this.tabControl_chart = new System.Windows.Forms.TabControl();
            this.tabPage_2d = new System.Windows.Forms.TabPage();
            this.myChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabPage_3d = new System.Windows.Forms.TabPage();
            this.panel_3D = new DisplayFunction.FPanel();
            this.groupBox_functions = new System.Windows.Forms.GroupBox();
            this.numericUpDown_Z_range_to = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_Y_range_to = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_Z_range_from = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_Y_range_from = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_X_range_to = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_X_range_from = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.radioButton_two_var = new System.Windows.Forms.RadioButton();
            this.button_help = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.listBox_variables = new System.Windows.Forms.ListBox();
            this.radioButton_one_var = new System.Windows.Forms.RadioButton();
            this.button_search = new System.Windows.Forms.Button();
            this.button_read_from_file = new System.Windows.Forms.Button();
            this.listView_results = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.numericUpDown_precison = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.tabControl_chart.SuspendLayout();
            this.tabPage_2d.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.myChart)).BeginInit();
            this.tabPage_3d.SuspendLayout();
            this.groupBox_functions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Z_range_to)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Y_range_to)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Z_range_from)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Y_range_from)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_X_range_to)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_X_range_from)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_precison)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox_function
            // 
            this.textBox_function.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_function.Enabled = false;
            this.textBox_function.Location = new System.Drawing.Point(887, 195);
            this.textBox_function.Name = "textBox_function";
            this.textBox_function.Size = new System.Drawing.Size(276, 20);
            this.textBox_function.TabIndex = 0;
            this.textBox_function.TabStop = false;
            this.textBox_function.Text = "Write a function...";
            this.textBox_function.Click += new System.EventHandler(this.textBox_function_Text_Click);
            this.textBox_function.Leave += new System.EventHandler(this.textBox_function_Leave);
            // 
            // label_results
            // 
            this.label_results.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_results.AutoSize = true;
            this.label_results.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label_results.Location = new System.Drawing.Point(887, 247);
            this.label_results.Name = "label_results";
            this.label_results.Size = new System.Drawing.Size(82, 25);
            this.label_results.TabIndex = 5;
            this.label_results.Text = "Results:";
            this.label_results.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabControl_chart
            // 
            this.tabControl_chart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl_chart.Controls.Add(this.tabPage_2d);
            this.tabControl_chart.Controls.Add(this.tabPage_3d);
            this.tabControl_chart.Location = new System.Drawing.Point(0, 0);
            this.tabControl_chart.Name = "tabControl_chart";
            this.tabControl_chart.SelectedIndex = 0;
            this.tabControl_chart.Size = new System.Drawing.Size(881, 665);
            this.tabControl_chart.TabIndex = 6;
            // 
            // tabPage_2d
            // 
            this.tabPage_2d.Controls.Add(this.myChart);
            this.tabPage_2d.Location = new System.Drawing.Point(4, 22);
            this.tabPage_2d.Name = "tabPage_2d";
            this.tabPage_2d.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_2d.Size = new System.Drawing.Size(873, 639);
            this.tabPage_2d.TabIndex = 0;
            this.tabPage_2d.Text = "2D";
            this.tabPage_2d.UseVisualStyleBackColor = true;
            // 
            // myChart
            // 
            chartArea1.Name = "ChartArea1";
            this.myChart.ChartAreas.Add(chartArea1);
            this.myChart.Enabled = false;
            legend1.Name = "Legend1";
            this.myChart.Legends.Add(legend1);
            this.myChart.Location = new System.Drawing.Point(0, 0);
            this.myChart.Name = "myChart";
            series1.ChartArea = "ChartArea1";
            series1.IsVisibleInLegend = false;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            series2.ChartArea = "ChartArea1";
            series2.IsVisibleInLegend = false;
            series2.Legend = "Legend1";
            series2.Name = "Series2";
            this.myChart.Series.Add(series1);
            this.myChart.Series.Add(series2);
            this.myChart.Size = new System.Drawing.Size(855, 599);
            this.myChart.TabIndex = 0;
            this.myChart.Text = "chart1";
            // 
            // tabPage_3d
            // 
            this.tabPage_3d.Controls.Add(this.panel_3D);
            this.tabPage_3d.Location = new System.Drawing.Point(4, 22);
            this.tabPage_3d.Name = "tabPage_3d";
            this.tabPage_3d.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_3d.Size = new System.Drawing.Size(873, 639);
            this.tabPage_3d.TabIndex = 1;
            this.tabPage_3d.Text = "3D";
            this.tabPage_3d.UseVisualStyleBackColor = true;
            // 
            // panel_3D
            // 
            this.panel_3D.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_3D.ColorBackgroundAxisStep = 3;
            this.panel_3D.Location = new System.Drawing.Point(8, 9);
            this.panel_3D.Name = "panel_3D";
            this.panel_3D.Size = new System.Drawing.Size(811, 622);
            this.panel_3D.TabIndex = 0;
            // 
            // groupBox_functions
            // 
            this.groupBox_functions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_functions.Controls.Add(this.numericUpDown_Z_range_to);
            this.groupBox_functions.Controls.Add(this.numericUpDown_Y_range_to);
            this.groupBox_functions.Controls.Add(this.numericUpDown_Z_range_from);
            this.groupBox_functions.Controls.Add(this.numericUpDown_Y_range_from);
            this.groupBox_functions.Controls.Add(this.numericUpDown_X_range_to);
            this.groupBox_functions.Controls.Add(this.numericUpDown_X_range_from);
            this.groupBox_functions.Controls.Add(this.label6);
            this.groupBox_functions.Controls.Add(this.label7);
            this.groupBox_functions.Controls.Add(this.label4);
            this.groupBox_functions.Controls.Add(this.label5);
            this.groupBox_functions.Controls.Add(this.label2);
            this.groupBox_functions.Controls.Add(this.radioButton_two_var);
            this.groupBox_functions.Controls.Add(this.button_help);
            this.groupBox_functions.Controls.Add(this.label1);
            this.groupBox_functions.Controls.Add(this.listBox_variables);
            this.groupBox_functions.Controls.Add(this.radioButton_one_var);
            this.groupBox_functions.Location = new System.Drawing.Point(887, 12);
            this.groupBox_functions.Name = "groupBox_functions";
            this.groupBox_functions.Size = new System.Drawing.Size(382, 176);
            this.groupBox_functions.TabIndex = 0;
            this.groupBox_functions.TabStop = false;
            this.groupBox_functions.Text = "Search function";
            // 
            // numericUpDown_Z_range_to
            // 
            this.numericUpDown_Z_range_to.DecimalPlaces = 2;
            this.numericUpDown_Z_range_to.Location = new System.Drawing.Point(267, 140);
            this.numericUpDown_Z_range_to.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericUpDown_Z_range_to.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            -2147483648});
            this.numericUpDown_Z_range_to.Name = "numericUpDown_Z_range_to";
            this.numericUpDown_Z_range_to.Size = new System.Drawing.Size(99, 20);
            this.numericUpDown_Z_range_to.TabIndex = 28;
            // 
            // numericUpDown_Y_range_to
            // 
            this.numericUpDown_Y_range_to.DecimalPlaces = 2;
            this.numericUpDown_Y_range_to.Location = new System.Drawing.Point(267, 104);
            this.numericUpDown_Y_range_to.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericUpDown_Y_range_to.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            -2147483648});
            this.numericUpDown_Y_range_to.Name = "numericUpDown_Y_range_to";
            this.numericUpDown_Y_range_to.Size = new System.Drawing.Size(99, 20);
            this.numericUpDown_Y_range_to.TabIndex = 27;
            // 
            // numericUpDown_Z_range_from
            // 
            this.numericUpDown_Z_range_from.DecimalPlaces = 2;
            this.numericUpDown_Z_range_from.Location = new System.Drawing.Point(136, 140);
            this.numericUpDown_Z_range_from.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericUpDown_Z_range_from.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            -2147483648});
            this.numericUpDown_Z_range_from.Name = "numericUpDown_Z_range_from";
            this.numericUpDown_Z_range_from.Size = new System.Drawing.Size(99, 20);
            this.numericUpDown_Z_range_from.TabIndex = 26;
            // 
            // numericUpDown_Y_range_from
            // 
            this.numericUpDown_Y_range_from.DecimalPlaces = 2;
            this.numericUpDown_Y_range_from.Location = new System.Drawing.Point(136, 102);
            this.numericUpDown_Y_range_from.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericUpDown_Y_range_from.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            -2147483648});
            this.numericUpDown_Y_range_from.Name = "numericUpDown_Y_range_from";
            this.numericUpDown_Y_range_from.Size = new System.Drawing.Size(99, 20);
            this.numericUpDown_Y_range_from.TabIndex = 25;
            // 
            // numericUpDown_X_range_to
            // 
            this.numericUpDown_X_range_to.DecimalPlaces = 2;
            this.numericUpDown_X_range_to.Location = new System.Drawing.Point(267, 68);
            this.numericUpDown_X_range_to.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericUpDown_X_range_to.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            -2147483648});
            this.numericUpDown_X_range_to.Name = "numericUpDown_X_range_to";
            this.numericUpDown_X_range_to.Size = new System.Drawing.Size(99, 20);
            this.numericUpDown_X_range_to.TabIndex = 24;
            // 
            // numericUpDown_X_range_from
            // 
            this.numericUpDown_X_range_from.DecimalPlaces = 2;
            this.numericUpDown_X_range_from.Location = new System.Drawing.Point(136, 68);
            this.numericUpDown_X_range_from.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericUpDown_X_range_from.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            -2147483648});
            this.numericUpDown_X_range_from.Name = "numericUpDown_X_range_from";
            this.numericUpDown_X_range_from.Size = new System.Drawing.Size(99, 20);
            this.numericUpDown_X_range_from.TabIndex = 23;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(241, 142);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 18);
            this.label6.TabIndex = 22;
            this.label6.Text = "to: ";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(6, 140);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(129, 18);
            this.label7.TabIndex = 20;
            this.label7.Text = "Range for Z from: ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(241, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 18);
            this.label4.TabIndex = 18;
            this.label4.Text = "to: ";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 102);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(129, 18);
            this.label5.TabIndex = 16;
            this.label5.Text = "Range for Y from: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(241, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 18);
            this.label2.TabIndex = 14;
            this.label2.Text = "to: ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // radioButton_two_var
            // 
            this.radioButton_two_var.AutoSize = true;
            this.radioButton_two_var.Location = new System.Drawing.Point(7, 42);
            this.radioButton_two_var.Name = "radioButton_two_var";
            this.radioButton_two_var.Size = new System.Drawing.Size(68, 17);
            this.radioButton_two_var.TabIndex = 2;
            this.radioButton_two_var.Text = "maximum";
            this.radioButton_two_var.UseVisualStyleBackColor = true;
            this.radioButton_two_var.CheckedChanged += new System.EventHandler(this.radioButton_two_var_CheckedChanged);
            // 
            // button_help
            // 
            this.button_help.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_help.Location = new System.Drawing.Point(307, 10);
            this.button_help.Name = "button_help";
            this.button_help.Size = new System.Drawing.Size(69, 43);
            this.button_help.TabIndex = 8;
            this.button_help.TabStop = false;
            this.button_help.Text = "Help";
            this.button_help.UseVisualStyleBackColor = true;
            this.button_help.Click += new System.EventHandler(this.button_help_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 18);
            this.label1.TabIndex = 12;
            this.label1.Text = "Range for X from: ";
            // 
            // listBox_variables
            // 
            this.listBox_variables.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox_variables.Enabled = false;
            this.listBox_variables.FormattingEnabled = true;
            this.listBox_variables.Items.AddRange(new object[] {
            "f(x)=",
            "f(x, y)=",
            "f(x, y, z)="});
            this.listBox_variables.Location = new System.Drawing.Point(136, 10);
            this.listBox_variables.Name = "listBox_variables";
            this.listBox_variables.Size = new System.Drawing.Size(165, 43);
            this.listBox_variables.TabIndex = 3;
            this.listBox_variables.SelectedIndexChanged += new System.EventHandler(this.listBox_variables_SelectedIndexChanged);
            // 
            // radioButton_one_var
            // 
            this.radioButton_one_var.AutoSize = true;
            this.radioButton_one_var.Location = new System.Drawing.Point(7, 19);
            this.radioButton_one_var.Name = "radioButton_one_var";
            this.radioButton_one_var.Size = new System.Drawing.Size(65, 17);
            this.radioButton_one_var.TabIndex = 0;
            this.radioButton_one_var.Text = "minimum";
            this.radioButton_one_var.UseVisualStyleBackColor = true;
            this.radioButton_one_var.CheckedChanged += new System.EventHandler(this.radioButton_one_var_CheckedChanged);
            // 
            // button_search
            // 
            this.button_search.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_search.Enabled = false;
            this.button_search.Location = new System.Drawing.Point(1169, 194);
            this.button_search.Name = "button_search";
            this.button_search.Size = new System.Drawing.Size(100, 20);
            this.button_search.TabIndex = 9;
            this.button_search.Text = "Search";
            this.button_search.UseVisualStyleBackColor = true;
            this.button_search.Click += new System.EventHandler(this.button_search_Click);
            // 
            // button_read_from_file
            // 
            this.button_read_from_file.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_read_from_file.Enabled = false;
            this.button_read_from_file.Location = new System.Drawing.Point(887, 221);
            this.button_read_from_file.Name = "button_read_from_file";
            this.button_read_from_file.Size = new System.Drawing.Size(382, 23);
            this.button_read_from_file.TabIndex = 10;
            this.button_read_from_file.Text = "Read from file";
            this.button_read_from_file.UseVisualStyleBackColor = true;
            this.button_read_from_file.Click += new System.EventHandler(this.button_read_from_file_Click);
            // 
            // listView_results
            // 
            this.listView_results.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView_results.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.listView_results.FullRowSelect = true;
            this.listView_results.Location = new System.Drawing.Point(892, 275);
            this.listView_results.MultiSelect = false;
            this.listView_results.Name = "listView_results";
            this.listView_results.Size = new System.Drawing.Size(377, 386);
            this.listView_results.TabIndex = 17;
            this.listView_results.UseCompatibleStateImageBehavior = false;
            this.listView_results.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Result";
            this.columnHeader1.Width = 90;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "X";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 90;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Y";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader3.Width = 90;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Z";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader4.Width = 90;
            // 
            // numericUpDown_precison
            // 
            this.numericUpDown_precison.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDown_precison.Location = new System.Drawing.Point(1222, 249);
            this.numericUpDown_precison.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDown_precison.Name = "numericUpDown_precison";
            this.numericUpDown_precison.Size = new System.Drawing.Size(41, 20);
            this.numericUpDown_precison.TabIndex = 19;
            this.numericUpDown_precison.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(1139, 251);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 16);
            this.label3.TabIndex = 20;
            this.label3.Text = "Precision:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1281, 665);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numericUpDown_precison);
            this.Controls.Add(this.listView_results);
            this.Controls.Add(this.button_read_from_file);
            this.Controls.Add(this.button_search);
            this.Controls.Add(this.groupBox_functions);
            this.Controls.Add(this.tabControl_chart);
            this.Controls.Add(this.label_results);
            this.Controls.Add(this.textBox_function);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(800, 480);
            this.Name = "Form1";
            this.Text = "Extrema System Search";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tabControl_chart.ResumeLayout(false);
            this.tabPage_2d.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.myChart)).EndInit();
            this.tabPage_3d.ResumeLayout(false);
            this.groupBox_functions.ResumeLayout(false);
            this.groupBox_functions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Z_range_to)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Y_range_to)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Z_range_from)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Y_range_from)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_X_range_to)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_X_range_from)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_precison)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_function;
        private System.Windows.Forms.Label label_results;
        private System.Windows.Forms.TabControl tabControl_chart;
        private System.Windows.Forms.TabPage tabPage_2d;
        private System.Windows.Forms.TabPage tabPage_3d;
        private System.Windows.Forms.GroupBox groupBox_functions;
        private System.Windows.Forms.RadioButton radioButton_two_var;
        private System.Windows.Forms.RadioButton radioButton_one_var;
        private System.Windows.Forms.ListBox listBox_variables;
        private System.Windows.Forms.Button button_help;
        private System.Windows.Forms.Button button_search;
        private System.Windows.Forms.Button button_read_from_file;
        private System.Windows.Forms.DataVisualization.Charting.Chart myChart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private DisplayFunction.FPanel panel_3D;
        private System.Windows.Forms.NumericUpDown numericUpDown_Z_range_to;
        private System.Windows.Forms.NumericUpDown numericUpDown_Y_range_to;
        private System.Windows.Forms.NumericUpDown numericUpDown_Z_range_from;
        private System.Windows.Forms.NumericUpDown numericUpDown_Y_range_from;
        private System.Windows.Forms.NumericUpDown numericUpDown_X_range_to;
        private System.Windows.Forms.NumericUpDown numericUpDown_X_range_from;
        private System.Windows.Forms.ListView listView_results;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.NumericUpDown numericUpDown_precison;
        private System.Windows.Forms.Label label3;
    }
}

