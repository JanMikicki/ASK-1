using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ASK_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void otwórzToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.ShowDialog();
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(this.openFileDialog1.FileName))
                {
                    // Read the stream to a string, and write the string to the console.
                    String line = sr.ReadToEnd();
                    //this.label1.Text = line;
                    this.textBox1.Text = line; ;
                }
            }
            catch (IOException ne)
            {
                MessageBox.Show(ne.Message);              
            }
        }

        private void zapiszToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.saveFileDialog1.ShowDialog();
            // Create a string array with the lines of text
            string[] lines = this.textBox1.Lines;

            // Set a variable to the Documents path.
            string docPath =
              Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            // Write the string array to a new file named "WriteLines.txt".
            using (StreamWriter outputFile = new StreamWriter(this.saveFileDialog1.FileName))
            {
                foreach (string line in lines)
                    outputFile.WriteLine(line);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int number_one = Convert.ToInt32(this.label1.Text, 2); //zamiana stringa na liczbę binarną
            int number_two = Convert.ToInt32(this.label2.Text, 2); //też nie wiedziałem że się da

            // wypisanie w labelu 3 wyniku jako string dopełniony z lewj zerami
            this.label3.Text = Convert.ToString(number_one + number_two, 2).PadLeft(8, '0'); 
        }
    }
}
