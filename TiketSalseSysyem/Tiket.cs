using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiketSalseSysyem {
    class Tiket {
        //Name of the tiket
        private String name;
        //Price of the tiket
        private float price;
        //Amount of them
        private float amount;

        //Constructor
        public Tiket (string name, float price) {
            //Sets the valuse of the tikets
            this.name = name;
            this.price = price;
            amount = 0;
        }

        //Getter and Setter for 
        //The tiket name
        public string Name {
            get { return name; }
            set { this.name = value; }
        }

        //Getter and Setter for 
        //The tiket price
        public float Price {
            get { return price; }
            set { this.price = value; }
        }

        //Getter and Setter for 
        //The amount of tikets
        public float Amount {
            get { return amount; }
            set { this.amount = value; }
        }

    }
}