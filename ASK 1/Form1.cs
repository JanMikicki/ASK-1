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
        private int line_nr = 0;

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

        private void resetRegisters() {
            saveToReg("AX", 0);
            saveToReg("BX", 0);
            saveToReg("CX", 0);
            saveToReg("DX", 0);
            updateRegisterLabel("AX", view);
            updateRegisterLabel("BX", view);
            updateRegisterLabel("CX", view);
            updateRegisterLabel("DX", view);
        }

        private void saveToReg(String regname, int sourceVal)
        {
            switch (regname.Substring(1))
            {
                case "X":
                    {
                        chooseDestinationRegister(regname).writeInto(sourceVal);
                        break;
                    }
                case "H":
                    {
                        String dest = regname.Substring(0, 1) + "X";
                        chooseDestinationRegister(dest).writeInto(sourceVal, "H");
                        break;
                    }
                case "L":
                    {
                        String dest = regname.Substring(0, 1) + "X";
                        chooseDestinationRegister(dest).writeInto(sourceVal, "L");
                        break;
                    }
                default:
                    break;
            }
        }

        private int getFromReg(String regname)
        {
            switch (regname.Substring(1))
            {
                case "X":
                    {
                        return chooseDestinationRegister(regname).getValue();
                    }
                case "H":
                    {
                        String dest = regname.Substring(0, 1) + "X";
                        return chooseDestinationRegister(dest).getValue("H");
                    }
                case "L":
                    {
                        String dest = regname.Substring(0, 1) + "X";
                        return chooseDestinationRegister(dest).getValue("L");                       
                    }
                default:
                    return -1;
            }
        }

        private void makeOperation(String operation, String regname, int sourceVal) {
            Register regDest = chooseDestinationRegister(regname);
            switch (operation) {
                case "MOV": {
                        saveToReg(regname, sourceVal);
                        break;
                    }
                case "ADD": {
                        int a = getFromReg(regname);
                        int b = sourceVal;                      
                        int c = a + b;
                        saveToReg(regname, c);
                        break;
                    }
                case "SUB": {
                        int a = getFromReg(regname);
                        int b = sourceVal;
                        int c = a - b;
                        saveToReg(regname, c);
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

        private int getSourceValue(String arg) { //na razie nie mozna jeszcze podac jako drugiego argumentu sub-rejestru
            switch (arg[0]) {
                case 'A':
                    return getFromReg(arg);
                case 'B':
                    return getFromReg(arg);
                case 'C':
                    return getFromReg(arg);
                case 'D':
                    return getFromReg(arg);
                default:
                    if (arg.Length > 1 && arg.Substring(0, 2) == "0X") {
                        arg = arg.Replace("0X", "");
                        return int.Parse(arg, System.Globalization.NumberStyles.HexNumber);
                    }
                    else if (arg[arg.Length - 1] == 'H')
                    {
                        arg = arg.Replace("H", "");
                        return int.Parse(arg, System.Globalization.NumberStyles.HexNumber);
                    }
                    else if (arg[arg.Length - 1] == 'B')
                    {
                        arg = arg.Replace("B", "");
                        return Convert.ToInt32(arg, 2);
                    }
                    return Convert.ToInt32(arg);
            }
        }

        private void updateRegisterLabel(String regname, int view) {
            //String value = Convert.ToString(reg.getValue());
            switch (regname.Substring(0, 1)) {
                case "A":
                    if(view == 2)
                        this.label4.Text = Convert.ToString(regAX.getValue(), view).PadLeft(16, '0');
                    else
                        this.label4.Text = Convert.ToString(regAX.getValue(), view);
                    break;
                case "B":
                    this.label11.Text = Convert.ToString(regBX.getValue(), view);
                    break;
                case "C":
                    this.label6.Text = Convert.ToString(regCX.getValue(), view);
                    break;
                case "D":
                    this.label9.Text = Convert.ToString(regDX.getValue(), view);
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

                    makeOperation(command, firstArg, getSourceValue(secondArg));
                    updateRegisterLabel(firstArg, view);
                }   
            }
        }

        private void pomocToolStripMenuItem_Click(object sender, EventArgs e) {
            MessageBox.Show("KOMENDY: MOV, ADD, SUB.\n" +
                            "Przykład:\nDziesietnie MOV AX, 123 \n" +
                            "Hexadecymentalnie MOV AX, 0x123 lub MOV AX, 123h\n" +
                            "Binarnie MOV AX, 10011b");
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e) {
            view = 10;
            updateRegisterLabel("AX", view);
            updateRegisterLabel("BX", view);
            updateRegisterLabel("CX", view);
            updateRegisterLabel("DX", view);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e) {
            view = 2;
            updateRegisterLabel("AX", view);
            updateRegisterLabel("BX", view);
            updateRegisterLabel("CX", view);
            updateRegisterLabel("DX", view);
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e) {
            view = 16;
            updateRegisterLabel("AX", view);
            updateRegisterLabel("BX", view);
            updateRegisterLabel("CX", view);
            updateRegisterLabel("DX", view);
        }

        private void button1_Click(object sender, EventArgs e) {
            String command;
            String firstArg;
            String secondArg;
            String line = "";
            try {
                line = textBox1.Lines[line_nr];
            } catch (IndexOutOfRangeException) {
                this.label3.Text = "BRAK DALSZYCH LINII W KODZIE. KONIEC PRACY KROKOWEJ";
                line_nr = -1;
                resetRegisters();
            }
            
            if (!String.IsNullOrEmpty(line)) 
            {
                command = line.Substring(0, 3).ToUpper();                          
                String[] args = line.Substring(line.IndexOf(' ') + 1).Split(',');   
                firstArg = args[0].Trim().ToUpper();                                
                secondArg = args[1].Trim().ToUpper();

                makeOperation(command, firstArg, getSourceValue(secondArg));
                updateRegisterLabel(firstArg, view);
                this.label3.Text = (line_nr + 1) + ". " + line;
            }
            line_nr++;
            
        }

        private void button3_Click(object sender, EventArgs e) {
            line_nr = 0;
            this.label3.Text = "";
            resetRegisters();
        }
    }
}
