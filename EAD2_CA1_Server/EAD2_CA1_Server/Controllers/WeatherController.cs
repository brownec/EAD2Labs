using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EAD2_CA1_Server.Models;
using System.IO;

namespace EAD2_CA1_Server.Controllers
{
    [RoutePrefix("Weather")] // RoutePrefix set to go straight to the WEATHER controller
    public class WeatherController : ApiController
    {
        // create a static list of objects (static to remain on memory)
        public static List<Weather> weather = new List<Weather>()
        {
            new Weather{City = "Dublin", Temperature = 17, WindSpeed = 15, Condition = "Cloudy", Warning = false},
            new Weather{City = "Derry", Temperature = 12, WindSpeed = 26, Condition = "Windy", Warning = false},
            new Weather{City = "London", Temperature = 7, WindSpeed = 88, Condition = "Stormy", Warning = true},
            new Weather{City = "Athens", Temperature = 2, WindSpeed = 51, Condition = "Floods", Warning = true}
        };

        // GET | GET WEATHER INFORMATION FOR ALL CITIES
        [Route("")]
        [HttpGet]
        public IHttpActionResult GetAllCities()
        {
            // retrieve weather information for all cities
            return Ok(weather);
        }

        // GET | GET WEATHER FOR A SPECIFIC CITY
        [Route("City/{cityName:alpha}")]
        public IHttpActionResult GetCity(String cityName)
        {
            if (cityName != null) // make sure city not null
            {
                // GET city based on parameter being passed in
                Weather result = weather.SingleOrDefault(c => c.City.ToLower().Equals(cityName.ToLower()));
                if (result != null) // if match found in DB
                {
                    return Ok(result); // 200 OK message
                }
                else
                {
                    Console.WriteLine("Bad Request Specific City");
                    return BadRequest(); // no match found
                }
            }
            return NotFound(); // parameter being assed in is empty
        }
        
        // GET | GET WEATHER FOR A CITY THAT HAS A WARNING (TRUE/FALSE)
        [Route("Warning/{warning:bool}")]
        public IHttpActionResult GetWarning(bool warning)
        {
            // Return all the cities that have a weather warning based on parameter
            var result = weather.Where(c => c.Warning == warning);
            return Ok(result);
        }

        // PUT | UPDATE WEATHER INFORMATION FOR A CITY
        [Route("Update/{cityName:alpha}")]
        public IHttpActionResult PutUpdateCity(String cityName, Weather w)
        {
            if (cityName != null)
            {
                // find existing city in DB
                Weather wea = weather.SingleOrDefault(c => c.City.ToLower() == cityName.ToLower());
                if (wea != null)
                {
                    try
                    {
                        if (ModelState.IsValid)
                        {
                            weather.Remove(wea); // remove existing record being updated
                            weather.Add(w); // add new record
                            return Ok();
                        }
                    }
                    catch (Exception e)
                    {
                        // e.Message.ToString();
                        // throw;
                    }
                }
                else
                {
                    Console.WriteLine("Bad Request Update");
                    return BadRequest();
                }
            }
            return NotFound();
        }

        // DELETE | DELETE A CITY FROM THE COLLECTION
        [Route("Delete/{cityName:alpha}")]
        public IHttpActionResult DeleteCityObject(String cityName)
        {
            var result = weather.SingleOrDefault(c => c.City.ToLower().Equals(cityName.ToLower()));
            if (cityName != null)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        weather.Remove(result);
                        return Ok();
                    }
                }
                catch (Exception e)
                {
                    // e.Message.ToString();
                    // throw;
                }
            }
            return NotFound(); // parameter passed not found
        }

        // object on;y ma
        // POST | ADD A NEW CITY TO THE COLLECTION
        [Route("AddCity/{cityName:alpha}")]
        //public IHttpActionResult PostWeatherObject(String cityName, Weather w)
            public IHttpActionResult PostWeatherObject(Weather w)
        {
            //if (cityName != null)
            //{
            //    var result = weather.SingleOrDefault(c => c.City.ToLower().Equals(cityName.ToLower()));
            //    if (result == null) // if not in DB
            //    {
            //        try
            //        {
                        if (ModelState.IsValid)
                        {
                            var result = weather.FirstOrDefault(c => c.City.ToLower() ==(w.City.ToLower()));
                            // check if file exists
                            //if (!File.Exists("C:\\Test\\log.txt"))
                            //{
                            //    File.Create("C:\\Test\\log.txt");

                            //    // write message details to log file
                            //    using (StreamWriter s = new StreamWriter("C:\\Test\\log.txt", true))
                            //    {
                            //        s.WriteLine("City: " + w.City
                            //                             + "\nConditions: " + w.Condition
                            //                             + "\nOther Details: " + w.WindSpeed
                            //                             + "\n " + DateTime.Now.ToLongTimeString()
                            //                             + " " + DateTime.Now.ToLongDateString());
                            //    }
                            //    // Add to the existing collection
                            //    weather.Add(w); // add this to the collection
                            //    // create Http response with Created status code andlisting serialized as content and 
                            //    // location header set to uri for new resource
                            //    // "id" variable must match in the WebApiConfig.cs file
                            //    string uri = Url.Link("DefaultApi", new { controller = "Weather", id = w.WindSpeed });
                            //    return Created(uri, w);
                            //}

                            if(result == null)
                            {
                                weather.Add(w);
                                string uri = Url.Link("DefaultApi", new { controller = "Weather", id = w.WindSpeed });
                                return Created(uri, w);
                            }
                            else
	                        {
                                return NotFound();
	                        }
                        }
                    //}
                //    catch (Exception e)
                //    {
                //        // throw;
                //    }
                //}
                else
                {
                    return BadRequest();
                }
            }
            //Console.WriteLine("Bad Request Add City");
    //        return BadRequest();
    }
}
//}
