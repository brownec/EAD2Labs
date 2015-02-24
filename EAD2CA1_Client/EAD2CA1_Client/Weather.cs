using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAD2CA1_Server.Models
{
    class Weather
    {
        public string City { get; set; }

        public string CityTemp { get; set; }

        public int WindSpeed { get; set; }

        public string Conditions { get; set; }

        public Boolean Warning { get; set; }
    }
}
