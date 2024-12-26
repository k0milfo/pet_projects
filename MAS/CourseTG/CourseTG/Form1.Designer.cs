using System.Windows.Forms;

namespace CourseTG
{
    partial class Form1 : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.textBox_CountRub = new System.Windows.Forms.TextBox();
            this.rub = new System.Windows.Forms.Label();
            this.tenge = new System.Windows.Forms.Label();
            this.textBox_AmountUSDTtoRub = new System.Windows.Forms.TextBox();
            this.textBox_CountUSDT = new System.Windows.Forms.TextBox();
            this.usdt = new System.Windows.Forms.Label();
            this.button = new System.Windows.Forms.Button();
            this.textBox_CountKZT = new System.Windows.Forms.TextBox();
            this.textBox_RubToTenge = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_AmountUSDTtoKZT = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.allClean = new System.Windows.Forms.Button();
            this.rub_box = new System.Windows.Forms.TextBox();
            this.procent_box = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.x = new System.Windows.Forms.Label();
            this.label_1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label_2 = new System.Windows.Forms.Label();
            this.label_3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox_CountRub
            // 
            this.textBox_CountRub.Location = new System.Drawing.Point(14, 44);
            this.textBox_CountRub.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox_CountRub.Name = "textBox_CountRub";
            this.textBox_CountRub.Size = new System.Drawing.Size(88, 22);
            this.textBox_CountRub.TabIndex = 0;
            // 
            // rub
            // 
            this.rub.AutoSize = true;
            this.rub.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rub.ForeColor = System.Drawing.Color.Black;
            this.rub.Location = new System.Drawing.Point(14, 20);
            this.rub.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.rub.Name = "rub";
            this.rub.Size = new System.Drawing.Size(36, 17);
            this.rub.TabIndex = 2;
            this.rub.Text = "RUB";
            // 
            // tenge
            // 
            this.tenge.AutoSize = true;
            this.tenge.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tenge.ForeColor = System.Drawing.Color.Black;
            this.tenge.Location = new System.Drawing.Point(272, 20);
            this.tenge.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.tenge.Name = "tenge";
            this.tenge.Size = new System.Drawing.Size(43, 17);
            this.tenge.TabIndex = 3;
            this.tenge.Text = "USDT";
            // 
            // textBox_AmountUSDTtoRub
            // 
            this.textBox_AmountUSDTtoRub.Location = new System.Drawing.Point(142, 44);
            this.textBox_AmountUSDTtoRub.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox_AmountUSDTtoRub.Name = "textBox_AmountUSDTtoRub";
            this.textBox_AmountUSDTtoRub.Size = new System.Drawing.Size(88, 22);
            this.textBox_AmountUSDTtoRub.TabIndex = 4;
            // 
            // textBox_CountUSDT
            // 
            this.textBox_CountUSDT.Location = new System.Drawing.Point(275, 44);
            this.textBox_CountUSDT.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox_CountUSDT.Name = "textBox_CountUSDT";
            this.textBox_CountUSDT.Size = new System.Drawing.Size(88, 22);
            this.textBox_CountUSDT.TabIndex = 5;
            // 
            // usdt
            // 
            this.usdt.AutoSize = true;
            this.usdt.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.usdt.ForeColor = System.Drawing.Color.Black;
            this.usdt.Location = new System.Drawing.Point(140, 1);
            this.usdt.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.usdt.Name = "usdt";
            this.usdt.Size = new System.Drawing.Size(102, 34);
            this.usdt.TabIndex = 6;
            this.usdt.Text = "Amount of sale\r\nRUB/USDT";
            // 
            // button
            // 
            this.button.Location = new System.Drawing.Point(144, 256);
            this.button.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button.Name = "button";
            this.button.Size = new System.Drawing.Size(88, 27);
            this.button.TabIndex = 7;
            this.button.Text = "Calculate";
            this.button.UseVisualStyleBackColor = true;
            this.button.Click += new System.EventHandler(this.button_Click);
            // 
            // textBox_CountKZT
            // 
            this.textBox_CountKZT.Location = new System.Drawing.Point(14, 118);
            this.textBox_CountKZT.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox_CountKZT.Name = "textBox_CountKZT";
            this.textBox_CountKZT.Size = new System.Drawing.Size(88, 22);
            this.textBox_CountKZT.TabIndex = 8;
            // 
            // textBox_RubToTenge
            // 
            this.textBox_RubToTenge.Location = new System.Drawing.Point(275, 118);
            this.textBox_RubToTenge.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox_RubToTenge.Name = "textBox_RubToTenge";
            this.textBox_RubToTenge.Size = new System.Drawing.Size(88, 22);
            this.textBox_RubToTenge.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(272, 75);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 34);
            this.label1.TabIndex = 10;
            this.label1.Text = "Course\r\nRUB to KZT";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(10, 94);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 17);
            this.label2.TabIndex = 11;
            this.label2.Text = "KZT";
            // 
            // textBox_AmountUSDTtoKZT
            // 
            this.textBox_AmountUSDTtoKZT.Location = new System.Drawing.Point(142, 118);
            this.textBox_AmountUSDTtoKZT.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox_AmountUSDTtoKZT.Name = "textBox_AmountUSDTtoKZT";
            this.textBox_AmountUSDTtoKZT.Size = new System.Drawing.Size(88, 22);
            this.textBox_AmountUSDTtoKZT.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(139, 75);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 34);
            this.label3.TabIndex = 13;
            this.label3.Text = "Amount of sale\r\nUSDT/KZT";
            // 
            // allClean
            // 
            this.allClean.Location = new System.Drawing.Point(287, 256);
            this.allClean.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.allClean.Name = "allClean";
            this.allClean.Size = new System.Drawing.Size(77, 27);
            this.allClean.TabIndex = 14;
            this.allClean.Text = "Clear";
            this.allClean.UseVisualStyleBackColor = true;
            this.allClean.Click += new System.EventHandler(this.button1_Click);
            // 
            // rub_box
            // 
            this.rub_box.Location = new System.Drawing.Point(13, 172);
            this.rub_box.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.rub_box.Name = "rub_box";
            this.rub_box.Size = new System.Drawing.Size(88, 22);
            this.rub_box.TabIndex = 15;
            this.rub_box.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // procent_box
            // 
            this.procent_box.Location = new System.Drawing.Point(142, 172);
            this.procent_box.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.procent_box.Name = "procent_box";
            this.procent_box.Size = new System.Drawing.Size(88, 22);
            this.procent_box.TabIndex = 16;
            this.procent_box.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(11, 152);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 17);
            this.label4.TabIndex = 18;
            this.label4.Text = "RUB";
            // 
            // x
            // 
            this.x.AutoSize = true;
            this.x.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.x.ForeColor = System.Drawing.Color.Black;
            this.x.Location = new System.Drawing.Point(141, 152);
            this.x.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.x.Name = "x";
            this.x.Size = new System.Drawing.Size(23, 17);
            this.x.TabIndex = 19;
            this.x.Text = "%";
            // 
            // label_1
            // 
            this.label_1.AutoSize = true;
            this.label_1.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_1.ForeColor = System.Drawing.Color.Black;
            this.label_1.Location = new System.Drawing.Point(320, 152);
            this.label_1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_1.Name = "label_1";
            this.label_1.Size = new System.Drawing.Size(29, 17);
            this.label_1.TabIndex = 20;
            this.label_1.Text = "___";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(238, 152);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 17);
            this.label7.TabIndex = 21;
            this.label7.Text = "Day";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(238, 177);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 17);
            this.label8.TabIndex = 22;
            this.label8.Text = "Month";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(238, 203);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(36, 17);
            this.label9.TabIndex = 23;
            this.label9.Text = "Year";
            // 
            // label_2
            // 
            this.label_2.AutoSize = true;
            this.label_2.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_2.ForeColor = System.Drawing.Color.Black;
            this.label_2.Location = new System.Drawing.Point(320, 177);
            this.label_2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_2.Name = "label_2";
            this.label_2.Size = new System.Drawing.Size(29, 17);
            this.label_2.TabIndex = 24;
            this.label_2.Text = "___";
            // 
            // label_3
            // 
            this.label_3.AutoSize = true;
            this.label_3.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_3.ForeColor = System.Drawing.Color.Black;
            this.label_3.Location = new System.Drawing.Point(320, 203);
            this.label_3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_3.Name = "label_3";
            this.label_3.Size = new System.Drawing.Size(29, 17);
            this.label_3.TabIndex = 25;
            this.label_3.Text = "___";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(167)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(377, 297);
            this.Controls.Add(this.label_3);
            this.Controls.Add(this.label_2);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label_1);
            this.Controls.Add(this.x);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.procent_box);
            this.Controls.Add(this.rub_box);
            this.Controls.Add(this.allClean);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_AmountUSDTtoKZT);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_RubToTenge);
            this.Controls.Add(this.textBox_CountKZT);
            this.Controls.Add(this.button);
            this.Controls.Add(this.usdt);
            this.Controls.Add(this.textBox_CountUSDT);
            this.Controls.Add(this.textBox_AmountUSDTtoRub);
            this.Controls.Add(this.tenge);
            this.Controls.Add(this.rub);
            this.Controls.Add(this.textBox_CountRub);
            this.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Exchange rate";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_CountRub;
        private System.Windows.Forms.Label rub;
        private System.Windows.Forms.Label tenge;
        private System.Windows.Forms.TextBox textBox_AmountUSDTtoRub;
        private System.Windows.Forms.TextBox textBox_CountUSDT;
        private System.Windows.Forms.Label usdt;
        private System.Windows.Forms.Button button;
        private System.Windows.Forms.TextBox textBox_CountKZT;
        private TextBox textBox_RubToTenge;
        private Label label1;
        private Label label2;
        private TextBox textBox_AmountUSDTtoKZT;
        private Label label3;
        private Button allClean;
        private TextBox rub_box;
        private TextBox procent_box;
        private Label label4;
        private Label x;
        private Label label_1;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label_2;
        private Label label_3;
    }
}

