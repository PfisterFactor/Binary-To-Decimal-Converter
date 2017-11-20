#define DEBUG
#undef DEBUG
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BinaryToDecimalConverter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void decimaltext_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        } // Event fired on keypress while typing in decimal input box.

        private void binarytext_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((TextBox)sender).Text.Length >= 31 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            if (!(e.KeyChar == '0' || e.KeyChar == '1') && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            
        } // Event fired on keypress while typing in binary input box.

        private void ClearButton_Click(object sender, EventArgs e) // Event fired on Clear button click.
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }
        Int64 binaryToDecimal(string binaryAsString) // Converts a string of binary to a 32 bit integer, 64 bit int is used to prevent overflow.
        {
            Int64 convertedBinary = 0;
            if (binaryAsString.Length > 63)
            {
                return -1;
            }
            int count = 0;
            for (int i = binaryAsString.Length-1; i >= 0; i--)
            {
                
                if (binaryAsString[i] == '1')
                {
                    convertedBinary += (Int64)Math.Pow((double)2, (double)count);
                }
                count++;
            }
                return convertedBinary;
        }
        string decimalToBinary(Int32 Decimal) // Converts an Int32 to binary.
        {
            string convertedDecimal = "";
            Int64 editDecimal = Decimal; // We perform our operations on this to not screw up the original data.
            for (int i = 31; i >= 0; i--)
            {
                Int64 TwoToTheithPower = (Int64)Math.Pow((double)2, (double)i);
#if (DEBUG)
                Console.WriteLine(TwoToTheithPower);
                Console.WriteLine(editDecimal - TwoToTheithPower >= 0);
#endif
                if (editDecimal - TwoToTheithPower >=0)
                {
                    editDecimal -= TwoToTheithPower;
                    convertedDecimal += "1";
                }
                else
                {
                    convertedDecimal += "0";
                }
            }

            for (int i = 0; i <= convertedDecimal.Length-1; i++) // Trims the trailing zeroes to get a much cleaner (and shorter) result.
            {
                if (convertedDecimal[i] == '1')
                {
#if (DEBUG)
                    Console.WriteLine(i);
#endif
                    convertedDecimal = convertedDecimal.Remove(0, i);
                    break;
                }
            }
                return convertedDecimal;
        }

        private void ConvertButton_Click(object sender, EventArgs e) // Event fired on Convert button click.
        {

            if (textBox1.Text != "")
            {
                Int64 convertedBinary = binaryToDecimal(textBox1.Text);
                textBox2.Text = Convert.ToString(convertedBinary);
                textBox1.Text = "";
            }
            else if (textBox2.Text != "")
            {
                string convertedDecimal = decimalToBinary(Convert.ToInt32(textBox2.Text));
                textBox1.Text = convertedDecimal;
                textBox2.Text = "";
            }
            
        }
    


    }
}
