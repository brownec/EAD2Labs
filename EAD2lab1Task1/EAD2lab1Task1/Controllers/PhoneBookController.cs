// Cliff Browne - X00014810
// EAD2 Lab1 Phonebook Service
using EAD2lab1Task1.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EAD2lab1Task1.Controllers
{
    public class PhoneBookController : ApiController
    {
        Contact[] contacts = new Contact[]
        {
            new Contact{Id = 1, Name = "Cliff Browne", Address = "1 Main Street", PhoneNumber = "0881234567"},
            new Contact{Id = 2, Name = "Gary Curran", Address = "11 Park Vale", PhoneNumber = "0889876543"},
            new Contact{Id = 3, Name = "Thomas Jones", Address = "Two Teachs", PhoneNumber = "0883216549"},
            new Contact{Id = 4, Name = "John Diez Daly", Address = "Kildare Way", PhoneNumber = "0881472583"},
            new Contact{Id = 5, Name = "John Diez Daly", Address = "Kildare Road", PhoneNumber = "0881472987"},
        };
        public IHttpActionResult GetName(string name)
        {
            List<Contact> myAL = new List<Contact>();

            //DB call using LINQ
            //Var = Generic Type
            var contact = contacts.Where(c => c.Name == name);

            foreach (var item in contact)
            {
                myAL.Add(item);
            }

            if (myAL != null)
            {
                foreach (var item in myAL)
                {
                    return Ok(ToString() + "Address " + item.Name + "PhoneNumber " + item.Address);
                }
            }
            return NotFound();
        }
        public IHttpActionResult GetPhoneNumber(string n)
        {
            // DB call using LINQ
            // Var = Generic Type
            var contact = contacts.Where(c => c.PhoneNumber == n).SingleOrDefault();
            if (contact != null)
            {
                return Ok(ToString() + "Name " + contact.Name + "Address " + contact.Address);
            }
            return NotFound();
        }
    }
}
