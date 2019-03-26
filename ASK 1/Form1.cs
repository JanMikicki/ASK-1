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
        private Register regAX;
        private Register regBX;
        private Register regCX;
        private Register regDX;

        private int view = 10; // 10 - base10, 2 - base2, 16 - base16

        public Form1()
        {
            InitializeComponent();
            initializeRegisters();
        }

        private void initializeRegisters() {
            this.regAX = new Register("AX");
            this.regBX = new Register("BX");
            this.regCX = new Register("CX");
            this.regDX = new Register("DX");
            this.label4.Text = Convert.ToString(regAX.getValue());
            this.label11.Text = Convert.ToString(regBX.getValue());
            this.label6.Text = Convert.ToString(regCX.getValue());
            this.label9.Text = Convert.ToString(regDX.getValue());
        }

        private void makeOperation(String operation, Register regDest, int sourceVal) {
            switch (operation) {
                case "MOV": {
                        regDest.writeInto(sourceVal);
                        break;
                    }
                case "ADD": {
                        int a = regDest.getValue();
                        int b = sourceVal;                      
                        int c = a + b;
                        regDest.writeInto(c);
                        break;
                    }
                case "SUB": {
                        int a = regDest.getValue();
                        int b = sourceVal;
                        int c = a - b;
                        regDest.writeInto(c);                       
                        break;
                    }
                default:
                    break;
            }

        }

        private Register chooseDestinationRegister(String regName) {
            switch (regName) {
                case "AX":
                    return regAX;
                case "BX":
                    return regBX;
                case "CX":
                    return regCX;
                case "DX":
                    return regDX;
                default:
                    return null;
            }
        }

        private void processSourceValue(String value) {
            String mode = value.Substring(value.Length);


        }

        private int getSourceValue(String regName) {
            switch (regName) {
                case "AX":
                    return regAX.getValue();
                case "BX":
                    return regBX.getValue();
                case "CX":
                    return regCX.getValue();
                case "DX":
                    return regDX.getValue();
                default:
                    if (regName[regName.Length - 1] == 'H') {
                        regName = regName.Replace("H", "");
                        return int.Parse(regName, System.Globalization.NumberStyles.HexNumber);
                    }
                    return Convert.ToInt32(regName);
            }
        }

        private void updateRegisterLabel(Register reg, int view) {
            //String value = Convert.ToString(reg.getValue());
            switch (reg.getName()) {
                case "AX":
                    //String tmp = Convert.ToString(Convert.ToInt32(value, view)); 
                    this.label4.Text = Convert.ToString(reg.getValue(), view);
                    break;
                case "BX":
                    this.label11.Text = Convert.ToString(reg.getValue(), view);
                    break;
                case "CX":
                    this.label6.Text = Convert.ToString(reg.getValue(), view);
                    break;
                case "DX":
                    this.label9.Text = Convert.ToString(reg.getValue(), view);
                    break;
                default:
                    break;
            }
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
                    this.textBox1.Text = line;
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

        private void button2_Click(object sender, EventArgs e) {
            String command;
            String firstArg;
            String secondArg;
            for (int i = 0; i < textBox1.Lines.Length; i++) {
                String line = textBox1.Lines[i];
                if (!String.IsNullOrEmpty(line)) //sprawdzenie czy linia nie pusta
                {
                    command = line.Substring(0, 3).ToUpper();                           //pierwsze 3 znaki to rozkaz
                    String[] args = line.Substring(line.IndexOf(' ') + 1).Split(',');   //dzieli argumenty przecinkiem
                    firstArg = args[0].Trim().ToUpper();                                //usunięcie spacjii, konwersja ToUpper
                    secondArg = args[1].Trim().ToUpper();

                    makeOperation(command, chooseDestinationRegister(firstArg), getSourceValue(secondArg));
                    updateRegisterLabel(chooseDestinationRegister(firstArg), view);
                }   
            }
        }

        private void pomocToolStripMenuItem_Click(object sender, EventArgs e) {
            MessageBox.Show("KOMENDY: MOV, ADD, SUB.\n Przykład:\n Dziesietnie MOV AX,123 \n Hexadecymentalnie MOV AX,123h");
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e) {
            view = 10;
            updateRegisterLabel(regAX, view);
            updateRegisterLabel(regBX, view);
            updateRegisterLabel(regCX, view);
            updateRegisterLabel(regDX, view);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e) {
            view = 2;
            updateRegisterLabel(regAX, view);
            updateRegisterLabel(regBX, view);
            updateRegisterLabel(regCX, view);
            updateRegisterLabel(regDX, view);
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e) {
            view = 16;
            updateRegisterLabel(regAX, view);
            updateRegisterLabel(regBX, view);
            updateRegisterLabel(regCX, view);
            updateRegisterLabel(regDX, view);
        }
    }
}
