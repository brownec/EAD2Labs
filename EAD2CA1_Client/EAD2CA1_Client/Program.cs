using EAD2CA1_Server.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Linq;
using System.Text;

namespace EAD2CA1_Client
{
    class Program
    {
        // get all stock listings
        static async Task GetAllAsync()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:40203/"); // base URL for API Controller i.e. RESTFul service

                    // add an Accept header for JSON
                    client.DefaultRequestHeaders.
                        Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // GET ../api/weather
                    // get all weather
                    HttpResponseMessage response = await client.GetAsync("Weather"); // async call, await suspends until result available            
                    if (response.IsSuccessStatusCode)                                  
                    {
                        // read result 
                        var listings = await response.Content.ReadAsAsync<IEnumerable<Weather>>();
                        foreach (var listing in listings)
                        {
                            Console.WriteLine(listing.City + " \t| " + listing.CityTemp + " \t| " + listing.WindSpeed + " \t| " + listing.Conditions + " \t| " + listing.Warning);
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

        // add a weather listing
        static async Task AddAsync()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:40203/");                             // base URL for API Controller i.e. RESTFul service

                    // add an Accept header for JSON - preference for response 
                    client.DefaultRequestHeaders.
                        Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // POST /api/weather with a listing serialised in request body
                    // create a new weather listing
                    Weather newListing = new Weather() { City = "Oslo", CityTemp = "15", WindSpeed = 45, Conditions = "Sunny", Warning = false  };
                    HttpResponseMessage response = await client.PostAsJsonAsync("Weather", newListing);   // or PostAsXmlAsync
                    if (response.IsSuccessStatusCode)                                                       // 200 .. 299
                    {
                        Uri newStockUri = response.Headers.Location;
                        var listing = await response.Content.ReadAsAsync<Weather>();
                        Console.WriteLine("URI for new resource: " + newStockUri.ToString());
                        Console.WriteLine("resource " + listing.City + " " + listing.CityTemp + " " + listing.WindSpeed + " " + listing.Conditions + " " + listing.Warning);
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

        // update a weather listing
        static async Task UpdateAsync()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:40203/");

                    Weather listing = new Weather() { City = "Dublin" };
                    listing.Conditions = "Snowy"; // condition has changed

                    // update by Put to /api/stock/FB a listing serialised in request body
                    HttpResponseMessage response = await client.PutAsJsonAsync("Weather/Dublin", listing);
                    if (!response.IsSuccessStatusCode)
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

        // delete a stock listing
        static async Task DeleteAsync()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:40203/");

                    HttpResponseMessage response = await client.DeleteAsync("Weather/Paris");
                    if (!response.IsSuccessStatusCode)
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

        // kick off
        static void Main(string[] args)
        {
            AddAsync().Wait();
            UpdateAsync().Wait();
            //DeleteAsync().Wait();
            GetAllAsync().Wait();
            
        }
    }
}
