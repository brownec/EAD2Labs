// ********************PROGRAM CLASS - CLIENT******************** 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using CA1_EAD2_Server.Models;

namespace CA1_EAD2_Client
{
    class Program
    { // get all stock listings Controller Method name GetAllCities()
        static async Task GetAllAsync()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:12051/"); // This is your local Host

                    // add an Accept header for JSON
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Your Route must always contain the Controller name as the first item (Weather)
                    // RoutePrefix = "" ; i.e. Weather Controller                  
                    HttpResponseMessage response = await client.GetAsync("Weather");
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Weather information for all cities:\n");
                        // read result 
                        var listings = await response.Content.ReadAsAsync<IEnumerable<Weather>>();
                        foreach (var listing in listings)
                        {
                            Console.WriteLine("\nCity:\t\t " + listing.City +
                                                "\nTemp:\t\t " + listing.Temp +
                                                "\nWindspeed:\t " + listing.WindSpeed +
                                                "\nCondition:\t " + listing.Conditions +
                                                "\nWarning:\t " + listing.Warning + "\n");
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

        static async Task GetOneAsync() // Controller Method name: GetCity(String cityName)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:12051/");  // This is your local Host

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
                        Console.WriteLine("Weather information for ONE city:\n" +
                                                "\nCity:\t\t " + listing.City +
                                                "\nTemp:\t\t " + listing.Temp +
                                                "\nWindspeed:\t " + listing.WindSpeed +
                                                "\nCondition:\t " + listing.Conditions +
                                                "\nWarning:\t " + listing.Warning + "\n");
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
                    client.BaseAddress = new Uri("http://localhost:12051/");

                    // add an Accept header for JSON
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // get all weather listings based on warning     
                    // RoutePrefix(in Controller):  [Route("Warning/{warning:bool}")]                   
                    HttpResponseMessage response = await client.GetAsync("Weather/Warning/true");

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Weather warning information:\n");
                        // read result 
                        var listings = await response.Content.ReadAsAsync<IEnumerable<Weather>>();
                        foreach (var listing in listings)
                        {
                            Console.WriteLine("\nCity:\t\t " + listing.City +
                                                "\nTemp:\t\t " + listing.Temp +
                                                "\nWindspeed:\t " + listing.WindSpeed +
                                                "\nCondition:\t " + listing.Conditions +
                                                "\nWarning:\t " + listing.Warning + "\n");
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
                    client.BaseAddress = new Uri("http://localhost:12051/");

                    // add an Accept header for JSON - preference for response 
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // ***********CREATE A NEW CITY TO BE ADDED TO THE COLLECTION***********
                    Weather newListing = new Weather() { City = "Madrid", WindSpeed = 2, Temp = 35, Conditions = "sunny ", Warning = false };

                    // Route Prefix(in Controller):  [Route("AddCity/{cityName:alpha}")]
                    HttpResponseMessage response = await client.PostAsJsonAsync("Weather/AddCity/Madrid", newListing);//pass in object also

                    if (response.IsSuccessStatusCode) // if your getting a false response then check the URi being passed back in the controller                                                    
                    {
                        // print out
                        Uri newStockUri = response.Headers.Location;
                        var listing = await response.Content.ReadAsAsync<Weather>();
                        Console.WriteLine("New resource has been added. URI: " + newStockUri.ToString() + "\n");
                        Console.WriteLine("New city has been added:\n" +
                                                "\nCity:\t\t " + listing.City +
                                                "\nTemp:\t\t " + listing.Temp +
                                                "\nWindspeed:\t " + listing.WindSpeed +
                                                "\nCondition:\t " + listing.Conditions +
                                                "\nWarning:\t " + listing.Warning + "\n");
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
                    client.BaseAddress = new Uri("http://localhost:12051/");//Your local host

                    // ***********CREATE NEW WEATHER OBJECT WITH UPDATED DETAILS*********** 
                    Weather listing = new Weather() { City = "Dublin", WindSpeed = 111, Temp = 44, Conditions = "Snow", Warning = false };

                    // Route Prefix(in Controller):   [Route("Update/{cityName:alpha}")]
                    HttpResponseMessage response = await client.PutAsJsonAsync("Weather/Update/Dublin", listing); // pass in object also
                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }
                    else
                    {
                        Console.WriteLine("Weather information update for Dublin\n");
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
                    client.BaseAddress = new Uri("http://localhost:12051/"); // your local host

                    // Route Prefix(in Controller):  [Route("Delete/{cityName:alpha}")]                                                    
                    HttpResponseMessage response = await client.DeleteAsync("Weather/Delete/Madrid");
                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }
                    Console.WriteLine("City successfully deleted: Madrid\n");
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
            // Print information for ALL cities
            GetAllAsync().Wait();
            Console.WriteLine("------------------------------");

            // Print information for ONE city (passed by parameter)
            GetOneAsync().Wait();
            Console.WriteLine("------------------------------");

            // Print information for ADD/POST city
            AddAsync().Wait();
            // Console.WriteLine("------------------------------");
            // Print information for ALL cities after ADD/POST
            GetAllAsync().Wait();
            Console.WriteLine("------------------------------");

            // Print information for UPDATED city
            UpdateAsync().Wait();
            //Console.WriteLine("------------------------------");
            // Print information for ALL cities after UPDATE/PUT
            GetAllAsync().Wait();
            Console.WriteLine("------------------------------");

            // Print information for cities with Weather Warnings (true/false)
            GetCityWarnAsync().Wait();
            Console.WriteLine("------------------------------");

            // Print information for DELETE city
            DeleteAsync().Wait();
            //
            Console.WriteLine("------------------------------");
            // Print information for ALL cities after city DELETED
            GetAllAsync().Wait();
        }
    }
}