using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EAD2lab1Task2.Models
{
    public class SMS
    {
        [Key]
        public int Id { get; set; }
        [Display(Name="SMS Message (max 140 Chars.)")]
        [Range(0, 140, ErrorMessage="SMS Message must be between 0 and 140 Characters.")]
        public string SMSMessage { get; set; }
        [Display(Name="Sender Number:")]
        [DataType(DataType.PhoneNumber)]
        public string SenderMobileNumber { get; set; }
        [Display(Name="Receiver Number:")]
        [DataType(DataType.PhoneNumber)]
        public string ReceiverMobileNumber { get; set; }
    }
}