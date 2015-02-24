// Cliff Browne - X00014810
/*
   •	The service provides one operation to send an SMS. 
   •	It should accept an SMS as input i.e. the message content (max 140 chars), 
 *      the mobile phone number of the sender, and the mobile number of the receiver.  
   •	The service should validate all inputs; the length of the message content should 
 *      not exceed 140 chars. 
   •	Instead of actually sending the text message, simulate the sending by writing to 
 *      a log file the details of the message and the time sent. 
        Design and develop a simple C# console application which acts as a client for the text 
 *      message service. 
 */
using EAD2lab1Task2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EAD2lab1Task2.Controllers
{
    public class SMSController : ApiController
    {
        public void Post()
        {
            // HTTP POST
            var message = new SMS() { SMSMessage = "", SenderMobileNumber = "", ReceiverMobileNumber = "" };
            response = await client.PostAsJsonAsync("api/sms", message);
            if (response.IsSuccessStatusCode)
            {
                // Get the URI of the created resource.
                Uri messageUrl = response.Headers.Location;
            }
        }
    }
}
