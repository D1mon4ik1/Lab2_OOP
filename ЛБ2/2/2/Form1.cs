using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static _2.Lab_2;

namespace _2
{
    public partial class Lab_2 : Form
    {
        public Lab_2()
        {
            InitializeComponent();
        }

        private void Lab_2_Load(object sender, EventArgs e)
        {
            label1.Text = "Введіть значення: ";
            label2.Text = "Чисельник - ";
            label3.Text = "Знаменник - ";
            label4.Text = "Збережені дроби: ";
            label5.Text = " ";
            label6.Text = "Оберіть операцію над дробами: ";
            label7.Text = "Результат:";
            label8.Text = " ";
            button1.Text = "Зберегти";
            button2.Text = "Додати";
            button3.Text = "Змінити";
            button4.Text = "Видалити";
            button5.Text = "Зберегти зміну";
            button6.Text = "Обчислити";            
            comboBox1.Items.Add("1. Додати");
            comboBox1.Items.Add("2. Відняти");
            comboBox1.Items.Add("3. Помножити");
            comboBox1.Items.Add("4. Поділити");
            comboBox1.Items.Add("5. Скоротити");
            comboBox1.Items.Add("6. Піднести до ступеня");
            comboBox1.Items.Add("7. Операції відносин");
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

            textBox1.Enabled = false;
            textBox2.Enabled = false;
            button1.Enabled = false;
            button5.Visible = false;
        }
        public class Fraction
        {
            public int Numerator { get; set; }
            public int Denominator { get; set; }

            public Fraction(int numerator, int denominator)
            {
                Numerator = numerator;
                Denominator = denominator;
            }

            public override string ToString()
            {
                return $"{Numerator}/{Denominator}";
            }
        }

        private List<Fraction> fractions = new List<Fraction>();

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (!int.TryParse(textBox1.Text, out _) || !int.TryParse(textBox2.Text, out _))
            {
                MessageBox.Show("Будь ласка, введіть число!", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            int numerator = int.Parse(textBox1.Text);
            int denominator = int.Parse(textBox2.Text);
                        
            Fraction fraction = new Fraction(numerator, denominator);

            fractions.Add(fraction);

            label5.Text += $"{fractions.Count}: {fraction.ToString()}\n";

            textBox1.Enabled = false;
            textBox2.Enabled = false;
            button1.Enabled = false;
            button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox1.Text = "";
            textBox2.Text = "";

            button1.Enabled = true;
            button2.Enabled = false;
        }

        private Fraction selectedFraction;

        private void button3_Click(object sender, EventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox("Введіть номер дробу, який потрібно змінити:", "Зміна дробу", "");

            if (string.IsNullOrWhiteSpace(input))
            {
                MessageBox.Show("Будь ласка, введіть номер дробу.", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(input, out int fractionNumber))
            {
                MessageBox.Show("Будь ласка, введіть коректний номер дробу.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (fractionNumber > 0 && fractionNumber <= fractions.Count)
            {
                selectedFraction = fractions[fractionNumber - 1];

                textBox1.Text = selectedFraction.Numerator.ToString();
                textBox2.Text = selectedFraction.Denominator.ToString();

                button5.Visible = true;
                textBox1.Enabled = true;
                textBox2.Enabled = true;
            }
            else
            {
                MessageBox.Show("Будь ласка, введіть коректний номер дробу.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            fractions.Clear();

            label5.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int numerator = int.Parse(textBox1.Text);
            int denominator = int.Parse(textBox2.Text);

            selectedFraction.Numerator = numerator;
            selectedFraction.Denominator = denominator;

            label5.Text = "";
            for (int i = 0; i < fractions.Count; i++)
            {
                if (i == fractions.Count - 1)
                {
                    label5.Text += $"{i + 1}: {fractions[i].ToString()}\n";
                }
                else
                {
                    label5.Text += $"{i + 1}: {fractions[i].ToString()}\n";
                }
            }
            textBox1.Text = "";
            textBox2.Text = "";

            button5.Visible = false;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        
        private void button6_Click(object sender, EventArgs e)
        {
            string selectedOperation = comboBox1.SelectedItem.ToString();

            Fraction resultFraction = null;
            switch (selectedOperation)
            {
                case "1. Додати":
                    resultFraction = AddFractions();
                    break;
                case "2. Відняти":
                    resultFraction = SubtractFractions();
                    break;
                case "3. Помножити":
                    resultFraction = MultiplyFractions();
                    break;
                case "4. Поділити":
                    resultFraction = DivideFractions();
                    break;
                case "5. Скоротити":              
                    foreach (var fraction in fractions)
                    {
                        resultFraction = SimplifyFraction(fraction);
                        fraction.Numerator = resultFraction.Numerator;
                        fraction.Denominator = resultFraction.Denominator;
                    }
                    break;
                case "6. Піднести до ступеня":
                    resultFraction = PowerFraction();
                    break;
                case "7. Операції відносин":
                    if (fractions.Count < 2)
                    {
                        MessageBox.Show("Не вистачає дробів для порівняння.", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    Fraction fraction1 = fractions[0];
                    Fraction fraction2 = fractions[1];

                    string result = CompareFractions(fraction1, fraction2);

                    if (result != null)
                    {
                        label8.Text = $"{fraction1} {result} {fraction2}";
                    }
                    break;
                default:

                    break;
            }
            if (resultFraction != null)
            {
                label8.Text = $"{resultFraction}";
            }
        }
        private Fraction AddFractions()
        {
            if (fractions.Count < 2)
            {
                MessageBox.Show("Не вистачає дробів для додавання.", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            Fraction sum = fractions[0];

            for (int i = 1; i < fractions.Count; i++)
            {
                sum = AddTwoFractions(sum, fractions[i]);
            }

            sum = SimplifyFraction(sum);

            return sum;
        }

        private Fraction AddTwoFractions(Fraction fraction1, Fraction fraction2)
        {
            int newNumerator = (fraction1.Numerator * fraction2.Denominator) + (fraction2.Numerator * fraction1.Denominator);
            int newDenominator = fraction1.Denominator * fraction2.Denominator;

            return new Fraction(newNumerator, newDenominator);
        }

        private int FindLeastCommonMultiple(int a, int b)
        {
            return (a * b) / FindGreatestCommonDivisor(a, b);
        }

        private int FindGreatestCommonDivisor(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        private Fraction SubtractFractions()
        {
            if (fractions.Count < 2)
            {
                MessageBox.Show("Не вистачає дробів для віднімання.", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            Fraction difference = fractions[0];

            for (int i = 1; i < fractions.Count; i++)
            {
                difference = SubtractTwoFractions(difference, fractions[i]);
            }

            difference = SimplifyFraction(difference);

            return difference;
        }

        private Fraction SubtractTwoFractions(Fraction fraction1, Fraction fraction2)
        {
            int newNumerator = (fraction1.Numerator * fraction2.Denominator) - (fraction2.Numerator * fraction1.Denominator);
            int newDenominator = fraction1.Denominator * fraction2.Denominator;

            return new Fraction(newNumerator, newDenominator);
        }


        private Fraction MultiplyFractions()
        {
            if (fractions.Count < 2)
            {
                MessageBox.Show("Не вистачає дробів для множення.", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            int productNumerator = fractions[0].Numerator;
            int productDenominator = fractions[0].Denominator;

            for (int i = 1; i < fractions.Count; i++)
            {
                productNumerator *= fractions[i].Numerator;
                productDenominator *= fractions[i].Denominator;
            }

            Fraction productFraction = new Fraction(productNumerator, productDenominator);

            productFraction = SimplifyFraction(productFraction);

            return productFraction;
        }

        private Fraction DivideFractions()
        {
            if (fractions.Count < 2)
            {
                MessageBox.Show("Не вистачає дробів для ділення.", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            Fraction quotient = fractions[0];

            for (int i = 1; i < fractions.Count; i++)
            {
                quotient = DivideTwoFractions(quotient, fractions[i]);
            }

            quotient = SimplifyFraction(quotient);

            return quotient;
        }

        private Fraction DivideTwoFractions(Fraction fraction1, Fraction fraction2)
        {
            if (fraction2.Numerator == 0)
            {
                MessageBox.Show("Ділення на нуль неможливе.", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            int newNumerator = fraction1.Numerator * fraction2.Denominator;
            int newDenominator = fraction1.Denominator * fraction2.Numerator;

            return new Fraction(newNumerator, newDenominator);
        }

        private Fraction SimplifyFraction(Fraction fraction)
        {            
            int gcd = FindGreatestCommonDivisor(fraction.Numerator, fraction.Denominator);

            int simplifiedNumerator = fraction.Numerator / gcd;
            int simplifiedDenominator = fraction.Denominator / gcd;

            return new Fraction(simplifiedNumerator, simplifiedDenominator);
        }

        private Fraction PowerFraction()
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox("Введіть ступінь:", "Піднесення до ступеня", "", -1, -1);

            if (string.IsNullOrEmpty(input))
            {
                MessageBox.Show("Ступінь не вказано.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            if (!int.TryParse(input, out int power))
            {
                MessageBox.Show("Введено некоректне значення ступеня.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            return PowerFraction(power);
        }

        private Fraction PowerFraction(int power)
        {
            if (fractions.Count == 0)
            {
                MessageBox.Show("Немає дробів для використання.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            Fraction fraction = fractions[0];

            if (fraction == null)
            {
                MessageBox.Show("Дріб не був переданий для піднесення до ступеня.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            return PowerFraction(fraction, power);
        }

        private Fraction PowerFraction(Fraction fraction, int power)
        {
            if (power == 0)
            {
                return new Fraction(1, 1);
            }

            if (power == 1)
            {
                return fraction;
            }

            if (power < 0)
            {
                fraction = new Fraction(fraction.Denominator, fraction.Numerator);
                power = Math.Abs(power);
            }

            int poweredNumerator = (int)Math.Pow(fraction.Numerator, power);
            int poweredDenominator = (int)Math.Pow(fraction.Denominator, power);

            return new Fraction(poweredNumerator, poweredDenominator);
        }

        private Fraction GetFractionFromUser()
        {
            string numeratorInput = Microsoft.VisualBasic.Interaction.InputBox("Введіть чисельник:", "Введення чисельника", "", -1, -1);

            if (string.IsNullOrEmpty(numeratorInput))
            {
                return null;
            }

            if (!int.TryParse(numeratorInput, out int numerator))
            {
                MessageBox.Show("Введено некоректне значення чисельника.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            string denominatorInput = Microsoft.VisualBasic.Interaction.InputBox("Введіть знаменник:", "Введення знаменника", "", -1, -1);

            if (string.IsNullOrEmpty(denominatorInput))
            {
                return null;
            }

            if (!int.TryParse(denominatorInput, out int denominator) || denominator == 0)
            {
                MessageBox.Show("Введено некоректне значення знаменника.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            return new Fraction(numerator, denominator);
        }

        private string CompareFractions(Fraction fraction1, Fraction fraction2)
        {
            double value1 = (double)fraction1.Numerator / fraction1.Denominator;
            double value2 = (double)fraction2.Numerator / fraction2.Denominator;

            if (value1 == value2)
            {
                return " = ";
            } 
            else if (value1 > value2)
            {
                return " > ";
            }
            else if (value1 < value2)
            {
                return " < ";
            }
            else
            {
                return "невідомо";
            }
        }
    }
}