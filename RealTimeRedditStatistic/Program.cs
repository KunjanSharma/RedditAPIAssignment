using Newtonsoft.Json.Linq;
using RedditPostAssignment.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace RealTimeRedditStatistic
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var apiUrl = "http://localhost:5298"; // Replace with your Web API's URL

            using (var httpClient = new HttpClient())
            {
                var tokenResponse = await httpClient.GetAsync($"{apiUrl}/api/reddit/getAccessToken");

                if (!tokenResponse.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Failed to fetch access token. HTTP Status: {tokenResponse.StatusCode}");
                    return;
                }

                var responseToken = await tokenResponse.Content.ReadAsStringAsync();
                var jsonTokenObject = JObject.Parse(responseToken);
                if (string.IsNullOrEmpty(jsonTokenObject["accessToken"]!.ToString()))
                {
                    Console.WriteLine("Access token is empty or null.");
                    return;
                }

                var accessToken = jsonTokenObject["accessToken"]!.ToString();

                // Setting the access token in headers or pass it as a parameter based on the Web API implementation
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

                // Fetch latest Reddit posts from your Web API        

                while (true)
                {
                    var posts = await httpClient.GetAsync($"{apiUrl}/api/reddit/getLatestPosts");
                    //var posts = jObject;
                    if (posts.IsSuccessStatusCode)
                    {
                        var content = await posts.Content.ReadAsStringAsync();

                        var jsonPostObject = JObject.Parse(content);

                        // Add null checks before accessing properties
                        var topUser = jsonPostObject["topUser"] as JObject;
                        if (topUser != null)
                        {
                            var topUserKey = topUser["key"]?.ToString();
                            var topUserValue = topUser["value"]?.ToString();

                            if (!string.IsNullOrEmpty(topUserKey) && !string.IsNullOrEmpty(topUserValue))
                            {
                                Console.WriteLine($"Top User:");
                                Console.WriteLine($"Username: {topUserKey}");
                                Console.WriteLine($"Post Count: {topUserValue}");
                            }
                            else
                            {
                                Console.WriteLine("The TopUser object does not have the expected structure.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No TopUser found in the response.");
                        }


                        var topPost = jsonPostObject["topPost"] as JObject;

                        if (topPost != null)
                        {
                            var topPostKey = topPost["key"]?.ToString();
                            var topPostValue = topPost["value"]?.ToString();

                            if (!string.IsNullOrEmpty(topPostKey) && !string.IsNullOrEmpty(topPostValue))
                            {
                                Console.WriteLine($"Top Post:");
                                Console.WriteLine($"Username: {topPostKey}");
                                Console.WriteLine($"Post Count: {topPostValue}");
                            }
                            else
                            {
                                Console.WriteLine("The TopPost object does not have the expected structure.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No TopPost found in the response.");
                        }

                        await Task.Delay(10000); // Sleep for 10 seconds. Adjust this to your needs and also add rate-limiting logic.
                    }
                    else
                    {
                        Console.WriteLine("Failed to fetch latest posts.");
                    }
                }
            }
        }
    }
}
