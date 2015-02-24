using EAD2CA1_Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EAD2CA1_Server.Controllers
{
    [RoutePrefix("Weather")] // navigate straight to the weather controller
    public class WeatherController : ApiController
    {
        // create a static list of objects
        // creates variable based on the class not on the object
        public static List<Weather> weather = new List<Weather>()
        {
            new Weather{City = "Dublin", CityTemp = "14", WindSpeed = 50, Conditions = "Cloudy", Warning = false },
            new Weather{City = "Rome", CityTemp = "7", WindSpeed = 120, Conditions = "Stormy", Warning = true },
            new Weather{City = "Paris", CityTemp = "22", WindSpeed = 30, Conditions = "Sunny", Warning = false },
            new Weather{City = "Madrid", CityTemp = "-25", WindSpeed = 75, Conditions = "Snowy", Warning = true }
        };

        // Retrieve the weather for all the cities
        // GET /weather - get weather information for all cities
        // RetrieveAllWeatherInformation()
        [Route("")]
        [HttpGet]
        public IHttpActionResult RetrieveAllWeatherInformation()
        {
            return Ok(weather);
        }

        // Retrieve the weather for a specified city
        // GET /weather/city/Dublin - get weather information for Dublin
        // GetWeatherInformationForCity(city) 
        [Route("city/{cityName:alpha}")]
        public IHttpActionResult GetWeatherInformationForCity(String cityName)
        {
            // LINQ query, find the matching city (case insensitive) or default
            // value null if none matching
            Weather cityWeather = weather.FirstOrDefault(w => w.City.ToUpper() == cityName.ToUpper());
            if (cityWeather == null)
            {
                return NotFound(); // 404 error not found
            }
            return Ok(cityWeather); // 200 OK
        }

        // Retrieve cities that have a weather warning
        // GET api/weather/warning/true or false
        // GET /weather/cities/warning/true - get cities which have a weather warning
        // GetCityNameForWarningStatus(true)
        [Route("cities/warning/{warning:bool}")]
        public IEnumerable<String> GetCityNameForWarningStatus(bool warning)
        {
            // LINQ Query to find all cities whose weather warning status
            // matches the specified warning parameter
            var cities = weather.Where(w => w.Warning == warning).Select(w => w.City);
            return cities;
        }

        // 4. the weather information for a specified city to be updated
        [Route("update/{cityName:alpha}")]
        public IHttpActionResult PutUpdateCity(Weather w)
        {
            // LINQ query to find specified city in list
            // SingleOrDefault = finds 1 or null records
            var cities = weather.SingleOrDefault(c => c.City.ToUpper() == w.City.ToUpper());
            if (cities != null)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        weather.Remove(cities); // remove city that was found in list(query)
                        weather.Add(w); // adds in the updated record
                        return Ok(); // 200 OK
                    }
                }
                catch (Exception e)
                {
                    e.Message.ToString();
                    throw;
                }
            }
            else
            {
                return BadRequest(); // Bad Request         
            }
            return NotFound(); // 404 error not found
        }

        [Route("addCity/{cityName:alpha}")]
        public IHttpActionResult Post(Weather w)
        {
            if (w != null)
            {
                var result = weather.SingleOrDefault(c => c.City.ToUpper() == w.City.ToUpper());
                if (result == null)
                {
                    try
                    {
                        if (ModelState.IsValid)
                        {
                            weather.Add(w);
                            // id variable must match the id in the WebApiConfig.cs file (AppStart)
                            string uri = Url.Link("DefaultApi", new { City = w.City });
                            return Created(uri, w);
                        }
                    }
                    catch (Exception e)
                    {
                        e.Message.ToString();
                        throw;
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }
    }
}
