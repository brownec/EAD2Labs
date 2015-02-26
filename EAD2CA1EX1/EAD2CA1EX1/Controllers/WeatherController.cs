using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EAD2CA1EX1.Models;

namespace EAD2CA1EX1.Controllers
{
    [RoutePrefix("Weather")] // set the route prefix to go to the weather controller
    public class WeatherController : ApiController
    {
        // create a static collection of objects
        public static List<Weather> weather = new List<Weather>()
        {
            new Weather {City = "Dublin", Temp = 17, WindSpeed = 15, Condition = "Cloudy", Warning = false},
            new Weather {City = "London", Temp = 22, WindSpeed = 9, Condition = "Sunny", Warning = false},
            new Weather {City = "Madrid", Temp = 28, WindSpeed = 5, Condition = "Sunny", Warning = false},
            new Weather {City = "Moscow", Temp = -9, WindSpeed = 80, Condition = "Snowy", Warning = true},
        };

        // GET | retrieve weather for all cities
        [Route("")]
        [HttpGet]
        public IHttpActionResult GetAllCities()
        {
            // retrieve weather for all the cities
            return Ok(weather);
        }

        // GET | retrieve weather for a specific city
        [Route("City/{cityName:alpha}")]
        public IHttpActionResult GetCity(String cityName)
        {
            if (cityName != null)
            {
                // retrieve city based on parameter being passed in
                var result = weather.SingleOrDefault(c => c.City.ToLower().Equals(cityName.ToLower()));
                return Ok(result);
            }
            return NotFound();
        }

        // GET | retrieve cities that have a weather warning (ie true/false)
        [Route("Warning/{warning:bool}")]
        public IHttpActionResult GetWarning(bool warning)
        {
            // retrieve all cities that have a weather warning (ie true/false)
            var result = weather.Where(c => c.Warning == warning);
            return Ok(result);
        }

        // PUT | update weather information for a specified city
        [Route("Update/{cityName:alpha}")]
        public IHttpActionResult PutUpdateCity(String cityName, Weather w)
        {
            if(cityName != null)
            {
                Weather we = weather.SingleOrDefault(c => c.City.ToUpper() == cityName.ToUpper());
                var result = weather.SingleOrDefault(c => c.City.ToUpper() == cityName.ToUpper());
                if(result != null)
                {
                    try
                    {
                        if(ModelState.IsValid)
                        {
                            weather.Remove(result);
                            weather.Add(w);
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
                    return BadRequest();
                }
            }
            return NotFound();
        }

        // DELETE | delete a city from the collection
        [Route("Delete/{cityName:alpha}")]
        public IHttpActionResult DeleteCityObject(String cityName)
        {
            var result = weather.SingleOrDefault(c => c.City.ToUpper().Equals(cityName.ToUpper()));
            if(cityName != null)
            {
                try
                {
                    if(ModelState.IsValid)
                    {
                        weather.Remove(result);
                        return Ok();
                    }
                }
                catch (Exception e)
                {
                    // e.Message.ToString();
                    throw;
                }
            }
            return NotFound();
        }

        // POST | insert a new city to the collection
        [Route("AddCity/{cityName:alpha}")]
        public IHttpActionResult PostWeatherObject(String cityName, Weather w)
        {
            if (cityName != null) // check for null object
            {
                var result = weather.SingleOrDefault(c => c.City.ToUpper().Equals(cityName.ToUpper()));
                if(ModelState.IsValid)
                {
                    // check to see if file exists
                    if (!File.Exists("C:\\Test\\log.txt"))
                    {
                       File.Create("C:\\Test\\log.txt"));
                    }
                    // write message details to log file
                    using (StreamWriter s = new StreamWriter("C:\\Test\\log.txt", true))
                    {
                        s.WriteLine("City: " + w.City 
                                    + "\nTemperature: " + w.Temp 
                                    + "\nWindspeed: " + w.WindSpeed 
                                    + "\n " + DateTime.Now.ToLongTimeString()
                                    + " " + DateTime.Now.ToLongDateString());
                    }

                    weather.Add(w); // Add this to the Collection
                    // create HTTP response with Created status code and list....
                    string uri = Url.Link("DefaultApi", new { controller = "Weather", id = w.WindSpeed});
                    return Created(uri, w);
                }
            }
        }
    }
}
