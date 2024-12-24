using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseTG
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            textBox_CountUSDT.TextChanged += TextChanget_box_CountUSDT;
            textBox_CountRub.TextChanged += TextChanget_box_CountRub;
            textBox_AmountUSDTtoRub.TextChanged += TextChanget_box_CountRub;
            rub_box.TextChanged += textBox1_TextChanged;
            procent_box.TextChanged += textBox2_TextChanged;
        }


        private void button_Click(object sender, EventArgs e)
        {

            if (textBox_CountRub.Text.Length != 0 && textBox_AmountUSDTtoRub.Text.Length != 0 && textBox_AmountUSDTtoKZT.Text.Length != 0)
            {
                getCountUSDT(textBox_CountRub, textBox_AmountUSDTtoRub);
                getCountKZT(textBox_CountUSDT, textBox_AmountUSDTtoKZT);
                getCourseRubToTenge(textBox_CountKZT, textBox_CountRub);
            }
            else if (textBox_CountRub.Text.Length == 0 && textBox_AmountUSDTtoKZT.Text.Length != 0 && textBox_CountUSDT.Text.Length != 0)
            {
                getCountKZT(textBox_AmountUSDTtoKZT, textBox_CountUSDT);
            }
        }
        private void TextChanget_box_CountRub(object sender, EventArgs e)
        {
            if (textBox_CountRub.Text.Length != 0 | textBox_AmountUSDTtoRub.Text.Length != 0)
            {
                textBox_CountUSDT.TextChanged -= TextChanget_box_CountUSDT;
                textBox_CountUSDT.Enabled = false;
                textBox_RubToTenge.Enabled = false;
                textBox_CountKZT.Enabled = false;
            }
            else
            {
                textBox_CountUSDT.TextChanged += TextChanget_box_CountUSDT;
                textBox_CountUSDT.Enabled = true;
                textBox_RubToTenge.Enabled = true;
                textBox_CountKZT.Enabled = true;
            }
        }
        private void TextChanget_box_CountUSDT(object sender, EventArgs e)
        {
            if (textBox_CountUSDT.Text.Length > 0)
            {
                textBox_CountRub.TextChanged -= TextChanget_box_CountRub;
                textBox_AmountUSDTtoRub.TextChanged -= TextChanget_box_CountRub;
                textBox_CountRub.Enabled = false;
                textBox_CountKZT.Enabled = false;
                textBox_AmountUSDTtoRub.Enabled = false;
                textBox_RubToTenge.Enabled = false;
            }
            else
            {
                textBox_CountRub.TextChanged += TextChanget_box_CountRub;
                textBox_AmountUSDTtoRub.TextChanged += TextChanget_box_CountRub;
                textBox_CountRub.Enabled = true;
                textBox_AmountUSDTtoRub.Enabled = true;
                textBox_RubToTenge.Enabled = true;
                textBox_CountKZT.Enabled = true;
            }
        }

        private string getCountUSDT(TextBox rub, TextBox AmountUSDTtoRub)
        {
            return
                textBox_CountUSDT.Text = Math.Round(double.Parse(textBox_CountRub.Text) /
                double.Parse(textBox_AmountUSDTtoRub.Text), 2).ToString();
        }

        private string getCountKZT(TextBox countUSDT, TextBox amountUSDTtoKZT)
        {
            return
                textBox_CountKZT.Text = Math.Round(double.Parse(textBox_CountUSDT.Text) *
                double.Parse(textBox_AmountUSDTtoKZT.Text), 2).ToString();
        }

        private string getCourseRubToTenge(TextBox countKZT, TextBox countRUB)
        {
            return
                textBox_RubToTenge.Text = Math.Round(double.Parse(textBox_CountKZT.Text) /
                double.Parse(textBox_CountRub.Text), 2).ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox_AmountUSDTtoKZT.Text = "";
            textBox_AmountUSDTtoRub.Text = "";
            textBox_CountKZT.Text = "";
            textBox_CountRub.Text = "";
            textBox_CountUSDT.Text = "";
            textBox_RubToTenge.Text = "";
            procent_box.Text = "";
            rub_box.Text = "";
            label_1.Text = "";
            label_2.Text = "";
            label_3.Text = "";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (procent_box.Text.Length != 0 && rub_box.Text.Length != 0)
            {
                double P = double.Parse(rub_box.Text); // начальная сумма
                double r = Math.Round(double.Parse(procent_box.Text) / 100, 2); // годовая процентная ставка (10%)
                int n = 1; // количество раз начисления процентов в году

                double t_day = 1.0 / 365; // Время в годах (1 день)
                double dailyInterest = P * Math.Pow(1 + r / n, n * t_day); // Вычисляем сумму за день
                label_1.Text = Math.Round(dailyInterest, 2).ToString();

                // Вычисление за месяц
                double t_month = 1.0 / 12; // Время в годах (1 месяц)
                double monthlyInterest = P * Math.Pow(1 + r / n, n * t_month); // Вычисляем сумму за месяц
                label_2.Text = Math.Round(monthlyInterest, 2).ToString();

                double yearlyInterest = P * Math.Pow(1 + r / n, n); // Вычисляем сумму за год
                label_3.Text = Math.Round(yearlyInterest, 2).ToString();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (rub_box.Text.Length != 0 && procent_box.Text.Length != 0)
            {
                double P = double.Parse(rub_box.Text); // начальная сумма
                double r = Math.Round(double.Parse(procent_box.Text) / 100, 2); // годовая процентная ставка (10%)
                int n = 1; // количество раз начисления процентов в году

                double t_day = 1.0 / 365; // Время в годах (1 день)
                double dailyInterest = P * Math.Pow(1 + r / n, n * t_day); // Вычисляем сумму за день
                label_1.Text = Math.Round(dailyInterest, 2).ToString();

                // Вычисление за месяц
                double t_month = 1.0 / 12; // Время в годах (1 месяц)
                double monthlyInterest = P * Math.Pow(1 + r / n, n * t_month); // Вычисляем сумму за месяц
                label_2.Text = Math.Round(monthlyInterest, 2).ToString();

                double yearlyInterest = P * Math.Pow(1 + r / n, n); // Вычисляем сумму за год
                label_3.Text = Math.Round(yearlyInterest, 2).ToString();
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
