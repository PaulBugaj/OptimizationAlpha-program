namespace OptimizationTests
{
    partial class TestForm
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
            this.comboBox_allTests = new System.Windows.Forms.ComboBox();
            this.listBox_testLogs = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button_beginTest = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBox_allTests
            // 
            this.comboBox_allTests.FormattingEnabled = true;
            this.comboBox_allTests.Location = new System.Drawing.Point(78, 12);
            this.comboBox_allTests.Name = "comboBox_allTests";
            this.comboBox_allTests.Size = new System.Drawing.Size(261, 21);
            this.comboBox_allTests.TabIndex = 0;
            // 
            // listBox_testLogs
            // 
            this.listBox_testLogs.FormattingEnabled = true;
            this.listBox_testLogs.Location = new System.Drawing.Point(12, 50);
            this.listBox_testLogs.Name = "listBox_testLogs";
            this.listBox_testLogs.Size = new System.Drawing.Size(776, 394);
            this.listBox_testLogs.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Select test:";
            // 
            // button_beginTest
            // 
            this.button_beginTest.Location = new System.Drawing.Point(668, 10);
            this.button_beginTest.Name = "button_beginTest";
            this.button_beginTest.Size = new System.Drawing.Size(120, 34);
            this.button_beginTest.TabIndex = 3;
            this.button_beginTest.Text = "Start";
            this.button_beginTest.UseVisualStyleBackColor = true;
            this.button_beginTest.Click += new System.EventHandler(this.button_beginTest_Click);
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button_beginTest);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBox_testLogs);
            this.Controls.Add(this.comboBox_allTests);
            this.Name = "TestForm";
            this.Text = "Test";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox_allTests;
        private System.Windows.Forms.ListBox listBox_testLogs;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_beginTest;
    }
}