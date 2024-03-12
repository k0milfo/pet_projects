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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(167)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(377, 297);
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
    }
}

