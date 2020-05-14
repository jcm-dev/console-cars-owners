using System;
using System.Collections.Generic;
using System.Text;

namespace Console_HW
{
    class Cars
    {
        //constructor
        public Cars(string model)
        {
            Model = model;
        }

        //auto properties
        public string Model { get; set; }
        public string Manufacturer { get; set; } = "";
        public string Price{ get; set; } = "";
    }
}
