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
            return this.HL;
        }

        public String getName() {
            return this.name;
        }

        public void writeInto(int value) {
            //this.H = value;
            //this.L = value;
            if (value >= 0 && value <=65536) {
                this.HL = value;
            } else {
                this.HL = 0;
            }
            
        }
    }
}
