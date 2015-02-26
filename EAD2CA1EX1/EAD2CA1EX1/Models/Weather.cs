using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EAD2CA1EX1.Models
{
    public class Weather
    {
        [Required(ErrorMessage="You must include a city")]
        public string City { get; set; }

        [Required(ErrorMessage="You must include a temperature(in Celcius)")]
        [Range(-50, 50, ErrorMessage="Temperature(Celcius) between -50 and 50")]
        public double Temp { get; set; }

        [Required(ErrorMessage="You must include windspeed")]
        [Range(0, 200, ErrorMessage="Range must be between 0 and 200 Km/h")]
        public int WindSpeed { get; set; }

        [Required(ErrorMessage="You must have a weather condition")]
        public String Condition { get; set; }

        [Required(ErrorMessage = "You must include wether there is a weather warning")]
        public bool Warning { get; set; }
    }
}