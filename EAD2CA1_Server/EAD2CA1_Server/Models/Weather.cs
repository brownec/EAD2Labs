using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EAD2CA1_Server.Models
{
    public class Weather
    {
        [Required(ErrorMessage="Must have a City")]
        public string City { get; set; }

        [Required(ErrorMessage="Must have a City Temperature")]
        [Range(-50, 50, ErrorMessage="Range between -50 and 50 Degrees Celcius")]
        public string CityTemp { get; set; }
        
        [Required(ErrorMessage="Must have a Windspeed")]
        [Range(0, 200, ErrorMessage="Range must be between 0 and 200 km/h")]
        public int WindSpeed { get; set; }
        
        [Required]
        public string Conditions { get; set; }
        
        public Boolean Warning { get; set; }
    }
}