using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASK_1 {
    class Register {

        private String name;

        private String H;
        private String L;
        private String HL;

        public Register(String name) {
            this.name = name;
            this.H = "00";
            this.L = "00";
            this.HL = this.H + this.L;
        }

        public String getValue() {
            return this.HL;
        }

        public String getName() {
            return this.name;
        }

        public void writeInto(String value) {
            this.H = value.Substring(0, 2);
            this.L = value.Substring(2);
            this.HL = this.H + this.L;
        }
    }
}
