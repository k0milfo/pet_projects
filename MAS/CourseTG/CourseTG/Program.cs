using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseTG
{
    partial class Program : Form
    {

        private Button buttonFormUsdtKzt;
        private Button buttonFormRubUsdt;



        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            
        }

        private void InitializeComponent()
        {
            this.buttonFormRubUsdt = new System.Windows.Forms.Button();
            this.buttonFormUsdtKzt = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonFormRubUsdt
            // 
            this.buttonFormRubUsdt.Location = new System.Drawing.Point(12, 68);
            this.buttonFormRubUsdt.Name = "buttonFormRubUsdt";
            this.buttonFormRubUsdt.Size = new System.Drawing.Size(260, 23);
            this.buttonFormRubUsdt.TabIndex = 0;
            this.buttonFormRubUsdt.Text = "Обмен RUB/USDT";
            this.buttonFormRubUsdt.UseVisualStyleBackColor = true;
            this.buttonFormRubUsdt.Click += new System.EventHandler(this.button_Form_Rub_Usdt);
            // 
            // buttonFormUsdtKzt
            // 
            this.buttonFormUsdtKzt.Location = new System.Drawing.Point(12, 123);
            this.buttonFormUsdtKzt.Name = "buttonFormUsdtKzt";
            this.buttonFormUsdtKzt.Size = new System.Drawing.Size(260, 23);
            this.buttonFormUsdtKzt.TabIndex = 1;
            this.buttonFormUsdtKzt.Text = "Обмен USDT/KZT";
            this.buttonFormUsdtKzt.UseVisualStyleBackColor = true;
            this.buttonFormUsdtKzt.Click += new System.EventHandler(this.button_Form_Usdt_Kzt);
            // 
            // Program
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.buttonFormUsdtKzt);
            this.Controls.Add(this.buttonFormRubUsdt);
            this.Name = "Program";
            this.ResumeLayout(false);

        }

        private void button_Form_Rub_Usdt(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void button_Form_Usdt_Kzt(object sender, EventArgs e)
        {

        }
    }
}
