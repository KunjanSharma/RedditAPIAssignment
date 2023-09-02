using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace RedditPostAssignment.Models
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json.Linq;

    public class RedditOAuth : IRedditOAuth
    {
        private const string TokenUrl = "https://www.reddit.com/api/v1/access_token";

        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string _username;
        private readonly string _password;
        private readonly IRateLimitedApiClient _apiClient;

        public RedditOAuth(string clientId, string clientSecret, string username, string password, IRateLimitedApiClient apiClient)
        {
            _clientId = clientId ?? throw new ArgumentNullException(nameof(clientId));
            _clientSecret = clientSecret ?? throw new ArgumentNullException(nameof(clientSecret));
            _username = username ?? throw new ArgumentNullException(nameof(username));
            _password = password ?? throw new ArgumentNullException(nameof(password));
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));

            // Set necessary headers on the RateLimitedApiClient's internal HttpClient.
            _apiClient.SetDefaultRequestHeader("User-Agent", "ScriptTest/1.0");
        }

        public async Task<string?> GetAccessToken()
        {
            try
            {
                if (_apiClient == null)
                {
                    throw new Exception("object is not set");
                }
                else
                {                   

                    // Add Basic auth header for OAuth
                    var authHeader = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_clientId}:{_clientSecret}"));
                    _apiClient.SetAuthorizationHeader(new AuthenticationHeaderValue("Basic", authHeader));

                    // Form data for OAuth request
                    var requestData = new FormUrlEncodedContent(new[]
                    {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", _username),
                    new KeyValuePair<string, string>("password", _password)
                });

                    // Make the request and retrieve the access token
                    var response = await _apiClient.PostAsync(TokenUrl, requestData);

                    // Check if the request was successful
                    response.EnsureSuccessStatusCode();

                    var responseString = await response.Content.ReadAsStringAsync();
                    var jsonObject = JObject.Parse(responseString);

                    return jsonObject["access_token"]?.ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error obtaining access token: {ex.Message}");
                return null;
            }
        }
    }

}
