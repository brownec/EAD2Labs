using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAD2_CA1_Server.Models
{
    class Weather
    {
        // [Required(ErrorMessage = "Must include a city")]
        public String City { get; set; }

        // [Required(ErrorMessage = "Must include a Temperature")]
        // [Range(-50, 50, ErrorMessage = "Temperature must be between -50 and 50 Degree Celsius")]
        public double Temperature { get; set; }

        // [Required(ErrorMessage = "Must include current Windspeed")]
        // [Range(0, 200, ErrorMessage = "Windspeed measured between 0 and 200 Km/h")]
        public int WindSpeed { get; set; }

        // [Required(ErrorMessage = "Must include weather condition(Sunny, Sno, Cloudy...")]
        public String Condition { get; set; }

        // [Required(ErrorMessage = "Weather warning in place must be True or False")]
        public bool Warning { get; set; }
    }
}
