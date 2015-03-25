// ********************WEATHER CLASS - CLIENT********************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA1_EAD2_Server.Models
{
    class Weather
    {
        // ***********REMOVE ALL THE REQUIRED AND VALIDATION FROM THE GLOBAL VARIABLES***********
        public String City { get; set; }

        public double Temp { get; set; }

        public int WindSpeed { get; set; }

        public String Conditions { get; set; }

        public bool Warning { get; set; }
    }
}