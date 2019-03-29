using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASK_1 {
    class Register {

        private String name;

        private int H;
        private int L;
        private int HL;

        public Register(String name) {
            this.name = name;
            this.H = 0;
            this.L = 0;
            this.HL = this.H + this.L;
        }

        public int getValue() {
            //this.HL = this.H + this.L;
            return this.HL;
        }

        public int getValue(String half)
        {
            if (half == "H") return this.H;
            else if (half == "L") return this.L;
            else return -1; // w sumie po nic
        }

        public String getName() {
            return this.name;
        }

        public void writeInto(int value)
        {           
            if (value >= 0 && value <=65536) {
                this.HL = value;

                String bin = Convert.ToString(value, 2).PadLeft(16, '0');
                this.H = Convert.ToInt32(bin.Substring(0, 8), 2);
                this.L = Convert.ToInt32(bin.Substring(8), 2);
            }
            else {
                String bin = Convert.ToString(value, 2);    // zbyt dużą liczbę obcina do 
                bin = bin.Substring(bin.Length - 16);       // ostatnich 16 bitów
                this.HL = Convert.ToInt32(bin, 2);
                this.H = Convert.ToInt32(bin.Substring(0, 8), 2);
                this.L = Convert.ToInt32(bin.Substring(8), 2);
            }
            
        }

        public void writeInto(int value, String half)
        {
            if (value >= 0 && value <= 255)
            {
                if (half == "H") this.H = value;             
                else if (half == "L") this.L = value;
                
                this.HL = (this.H << 8) + this.L;

            }
            else
            {
                String bin = Convert.ToString(value, 2);    // zbyt dużą liczbę obcina do 
                bin = bin.Substring(bin.Length - 8);        // ostatnich 8 bitów
               
                if (half == "H") this.H = Convert.ToInt32(bin, 2);
                else if (half == "L") this.L = Convert.ToInt32(bin, 2);

                this.HL = (this.H << 8) + this.L;
            }

        }
    }
}
