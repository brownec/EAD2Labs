// Cliff Browne - x00014810
/*
    Task 2 – SMS Service
•	A RESTful web service is required which allows SMS (text) messages to be sent. 
•	This text messaging web service wraps the functionality of a commercial SMS gateway 
 *  making it easier for clients to use the gateway.  
•	Clients can access the service over HTTP via a POST.
    The service is to be implemented in C# using the ASP.Net Web API, and hosted on IIS Express.
 */
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