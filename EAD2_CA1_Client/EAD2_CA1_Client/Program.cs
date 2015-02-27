using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EAD2_CA1_Server.Models;
using System.Net.Http;
using System.Net.Http.Headers;

namespace EAD2_CA1_Client
{
    class Program
    {
        // get all stock listings Controller Method name GetAllCities()
        static async Task GetAllAsync()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:3182/"); // This is your local Host

                    // add an Accept header for JSON
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Your Route must always contain the Controller name as the first item (Weather)
                    // RoutePrefix = "" ; i.e. Weather Controller                  
                    HttpResponseMessage response = await client.GetAsync("Weather");
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("City Details:\n ");
                        // read result 
                        var listings = await response.Content.ReadAsAsync<IEnumerable<Weather>>();
                        foreach (var listing in listings)
                        {
                            Console.WriteLine(  "\tCity: " + listing.City +
                                                "\tTemperature: " + listing.Temperature +
                                                "\tWindspeed: " + listing.WindSpeed);
                        }
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        static async Task GetOneAsync()   // Controller Method name: GetCity(String cityName)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:3182/");  // This is your local Host

                    // add an Accept header for JSON
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

					// RoutePrefix (in Controller): [Route("City/{cityName:alpha}")]                  
                    HttpResponseMessage response = await client.GetAsync("Weather/City/Dublin");

                    if (response.IsSuccessStatusCode)
                    {
                        // read result 
                        Weather listing = await response.Content.ReadAsAsync<Weather>();
                        // if returning a collection - use 2 commented out lines below
                        // var listings = await response.Content.ReadAsAsync<IEnumerable<Weather>>(); 
                        // foreach (var listing in listings)
                        // {
                        Console.WriteLine("City detail\n " +
                                                "\tCity: " + listing.City +
                                                ",\tTemperature: " + listing.Temperature +
                                                ",\tWindspeed: " + listing.WindSpeed);
                        // }
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        static async Task GetCityWarnAsync() // Controller Method name:  GetWarning(bool warning)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // your own local host
                    client.BaseAddress = new Uri("http://localhost:3182/");

                    // add an Accept header for JSON
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // get all weather listings based on warning     
                    // RoutePrefix(in Controller):  [Route("Warning/{warning:bool}")]                   
                    HttpResponseMessage response = await client.GetAsync("Weather/Warning/true");

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("City Weather Warnings: ");
                        // read result 
                        var listings = await response.Content.ReadAsAsync<IEnumerable<Weather>>();
                        foreach (var listing in listings)
                        {
                            Console.WriteLine(",\tCity: " + listing.City +
                                                ",\tTemperature: " + listing.Temperature +
                                                ",\tWindspeed: " + listing.WindSpeed);
                        }
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        // ***********ADD A CITY TO THE COLLECTION***********
        static async Task AddAsync() // Method name in Controller: PostWeatherObject(String cityName, Weather w)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {                                           //Your local host
                    client.BaseAddress = new Uri("http://localhost:3182/");

                    // add an Accept header for JSON - preference for response 
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // ***********CREATE A NEW CITY TO BE ADDED TO THE COLLECTION***********
                    Weather newListing = new Weather() { City = "Madrid", Temperature = 35, WindSpeed = 2, Condition = "sunny ", Warning = false };

                    // Route Prefix(in Controller):  [Route("AddCity/{cityName:alpha}")]
                    HttpResponseMessage response = await client.PostAsJsonAsync("Weather/AddCity/Madrid", newListing);//pass in object also
                                                                                                            
                    if (response.IsSuccessStatusCode) // if your getting a false response then check the URi being passed back in the controller                                                    
                    {
                        // print out
                        Uri newStockUri = response.Headers.Location;
                        var listing = await response.Content.ReadAsAsync<Weather>();
                        Console.WriteLine("URI for new resource: " + newStockUri.ToString());
                        Console.WriteLine("City Added " +
                                                ",City: " + listing.City +
                                                ",\tTemperature: " + listing.Temperature +
                                                ",\tWindspeed: " + listing.WindSpeed);
                    }
                    else
                    {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        // ***********UPDATE THE WEATHER COLLECTION***********
        static async Task UpdateAsync()  // Method name in Controller: PutUpdateCity(String cityName, Weather w)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:3182/");//Your local host

                    // ***********CREATE NEW WEATHER OBJECT WITH UPDATED DETAILS*********** 
                    Weather listing = new Weather() { City = "Dublin", Temperature = 44, WindSpeed = 111, Condition = "Snow", Warning = false };

                    // Route Prefix(in Controller):   [Route("Update/{cityName:alpha}")]
                    HttpResponseMessage response = await client.PutAsJsonAsync("Weather/Update/Dublin", listing); // pass in object also
                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }
                    else
                    {
                        Console.WriteLine("City Updated - Dublin");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        // ***********DELETE A CITY FROM THE COLLECTION***********
        static async Task DeleteAsync() // Controller Method name:  DeleteCityObject(String cityName)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:3182/"); // your local host

                    // Route Prefix(in Controller):  [Route("Delete/{cityName:alpha}")]                                                    
                    HttpResponseMessage response = await client.DeleteAsync("Weather/Delete/Madrid");
                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }
                    Console.WriteLine("City Deleted - Madrid" );
                    Console.ReadLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        // kick off
        static void Main(string[] args)
        {
            GetAllAsync().Wait(); // Get all cities

            DeleteAsync().Wait(); // Delete a city

            UpdateAsync().Wait(); // Update an existing city
            GetAllAsync().Wait();

            AddAsync().Wait(); // Add a city
           
            GetAllAsync().Wait();
            GetCityWarnAsync().Wait(); // Get warnings

            GetOneAsync().Wait(); // Get one City details
            GetAllAsync().Wait();
        }
    }
}