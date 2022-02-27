
using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace WebAPIClient
{
    class DayInfo
    {
        [JsonProperty("results")]
        public JSON_Results json_results;
    }

    public class JSON_Results
    {
        [JsonProperty("sunrise")]
        public string Sunrise { get; set; }

        [JsonProperty("sunset")]
        public string Sunset { get; set; }

        [JsonProperty("day_length")]
        public string Day_Length { get; set; }

        [JsonProperty("solar_noon")]
        public string Solar_Noon { get; set; }
    }

    class Program
    {

        private static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            await ProcessRepositories();
        }

        private static async Task ProcessRepositories()
        {
            while (true)
            {

                try
                {
                    Console.WriteLine("Please Enter a Latitude. Press Enter without writing a name to quit the program");
                    var userLatitude = Console.ReadLine();

                    if (string.IsNullOrEmpty(userLatitude))
                    {
                        break;
                    }

                    Console.WriteLine("Please Enter a Longitude. Press Enter without writing a name to quit the program");
                    var userLongitude = Console.ReadLine();

                    if (string.IsNullOrEmpty(userLongitude))
                    {
                        break;
                    }

                    var result = await client.GetAsync("https://api.sunrise-sunset.org/json?lat=" + userLatitude + "&lng=" + userLongitude);
          
                    var resultRead = await result.Content.ReadAsStringAsync();

                    var dayinfo = JsonConvert.DeserializeObject<DayInfo>(resultRead);
                    
                    Console.WriteLine("\n--\n");
                    Console.WriteLine("Results are directly from the API in GMT");
                    Console.WriteLine("Sunrise: " + dayinfo.json_results.Sunrise);
                    Console.WriteLine("Sunset: " + dayinfo.json_results.Sunset);
                    Console.WriteLine("Day Length: " + dayinfo.json_results.Day_Length);
                    Console.WriteLine("Solar Noon: " + dayinfo.json_results.Solar_Noon);
                    Console.WriteLine("\n--");


                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}

