using System;
using System.Collections.Generic;
using System.Text;

namespace Console_HW
{
    class Customers
    {
        //constructor
        public Customers(string name)
        {
            Name = name;
        }

        //auto properties
        public string Name { get; set; }
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Model { get; set; } = "";
    }
}
