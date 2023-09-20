namespace Siammod1
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart_graphic = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.RMS = new System.Windows.Forms.Label();
            this.variance = new System.Windows.Forms.Label();
            this.math_average = new System.Windows.Forms.Label();
            this.button_start = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_R = new System.Windows.Forms.TextBox();
            this.textBox_m = new System.Windows.Forms.TextBox();
            this.textBox_a = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.indirect_1 = new System.Windows.Forms.Label();
            this.indirect_2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chart_graphic)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // chart_graphic
            // 
            chartArea2.Name = "ChartArea1";
            this.chart_graphic.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chart_graphic.Legends.Add(legend2);
            this.chart_graphic.Location = new System.Drawing.Point(6, 21);
            this.chart_graphic.Name = "chart_graphic";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "bar chart";
            this.chart_graphic.Series.Add(series2);
            this.chart_graphic.Size = new System.Drawing.Size(501, 417);
            this.chart_graphic.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chart_graphic);
            this.groupBox1.Location = new System.Drawing.Point(1, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(546, 451);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button_start);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.textBox_R);
            this.groupBox2.Controls.Add(this.textBox_m);
            this.groupBox2.Controls.Add(this.textBox_a);
            this.groupBox2.Location = new System.Drawing.Point(553, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(235, 172);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Parametrs";
            // 
            // RMS
            // 
            this.RMS.AutoSize = true;
            this.RMS.Location = new System.Drawing.Point(164, 110);
            this.RMS.Name = "RMS";
            this.RMS.Size = new System.Drawing.Size(0, 16);
            this.RMS.TabIndex = 9;
            // 
            // variance
            // 
            this.variance.AutoSize = true;
            this.variance.Location = new System.Drawing.Point(164, 75);
            this.variance.Name = "variance";
            this.variance.Size = new System.Drawing.Size(0, 16);
            this.variance.TabIndex = 8;
            // 
            // math_average
            // 
            this.math_average.AutoSize = true;
            this.math_average.Location = new System.Drawing.Point(164, 37);
            this.math_average.Name = "math_average";
            this.math_average.Size = new System.Drawing.Size(0, 16);
            this.math_average.TabIndex = 7;
            // 
            // button_start
            // 
            this.button_start.Location = new System.Drawing.Point(145, 139);
            this.button_start.Name = "button_start";
            this.button_start.Size = new System.Drawing.Size(75, 23);
            this.button_start.TabIndex = 6;
            this.button_start.Text = "calculate";
            this.button_start.UseVisualStyleBackColor = true;
            this.button_start.Click += new System.EventHandler(this.button_start_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(24, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "R=";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "m=";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "a=";
            // 
            // textBox_R
            // 
            this.textBox_R.Location = new System.Drawing.Point(37, 59);
            this.textBox_R.Name = "textBox_R";
            this.textBox_R.Size = new System.Drawing.Size(133, 22);
            this.textBox_R.TabIndex = 2;
            this.textBox_R.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_R_KeyPress);
            this.textBox_R.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox_R_KeyUp);
            // 
            // textBox_m
            // 
            this.textBox_m.Location = new System.Drawing.Point(37, 99);
            this.textBox_m.Name = "textBox_m";
            this.textBox_m.Size = new System.Drawing.Size(133, 22);
            this.textBox_m.TabIndex = 1;
            this.textBox_m.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_m_KeyPress);
            this.textBox_m.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox_m_KeyUp);
            // 
            // textBox_a
            // 
            this.textBox_a.Location = new System.Drawing.Point(37, 21);
            this.textBox_a.Name = "textBox_a";
            this.textBox_a.Size = new System.Drawing.Size(133, 22);
            this.textBox_a.TabIndex = 0;
            this.textBox_a.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_a_KeyPress);
            this.textBox_a.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox_a_KeyUp);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.math_average);
            this.groupBox3.Controls.Add(this.variance);
            this.groupBox3.Controls.Add(this.RMS);
            this.groupBox3.Controls.Add(this.indirect_1);
            this.groupBox3.Controls.Add(this.indirect_2);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Location = new System.Drawing.Point(553, 180);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(235, 258);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "the required coefficients";
            // 
            // indirect_1
            // 
            this.indirect_1.AutoSize = true;
            this.indirect_1.Location = new System.Drawing.Point(164, 146);
            this.indirect_1.Name = "indirect_1";
            this.indirect_1.Size = new System.Drawing.Size(0, 16);
            this.indirect_1.TabIndex = 10;
            // 
            // indirect_2
            // 
            this.indirect_2.AutoSize = true;
            this.indirect_2.Location = new System.Drawing.Point(164, 185);
            this.indirect_2.Name = "indirect_2";
            this.indirect_2.Size = new System.Drawing.Size(0, 16);
            this.indirect_2.TabIndex = 11;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(22, 75);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 16);
            this.label8.TabIndex = 4;
            this.label8.Text = "variance=";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(22, 110);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 16);
            this.label7.TabIndex = 3;
            this.label7.Text = "RMS=";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(22, 146);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 16);
            this.label6.TabIndex = 2;
            this.label6.Text = "indirect signs";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 185);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 16);
            this.label5.TabIndex = 1;
            this.label5.Text = "indirect signs";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(118, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "math. expectation=";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.chart_graphic)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart_graphic;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBox_R;
        private System.Windows.Forms.TextBox textBox_m;
        private System.Windows.Forms.TextBox textBox_a;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button_start;
        private System.Windows.Forms.Label RMS;
        private System.Windows.Forms.Label variance;
        private System.Windows.Forms.Label math_average;
        private System.Windows.Forms.Label indirect_1;
        private System.Windows.Forms.Label indirect_2;
    }
}

