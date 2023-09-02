using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Net.Http;

namespace RedditPostAssignment.Models
{
    public class RedditService : IRedditService
    {
        private const string BaseUrl = "https://oauth.reddit.com/r/";
        private const string _subreddit = "worldnews";
        private readonly string? _accessToken;
        private readonly IRateLimitedApiClient _apiClient;

        public RedditService(string accessToken, IRateLimitedApiClient apiClient)
        {
            _accessToken = accessToken;
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));

            // Set necessary headers on the RateLimitedApiClient's internal HttpClient.
            _apiClient.SetDefaultRequestHeader("User-Agent", "ScriptTest/1.0");
        }
        public async Task<JObject> GetLatestPosts()
        {
            try
            {
                if (_apiClient == null)
                {
                    throw new Exception("object is not set");
                }
                else
                {
                    _apiClient.SetAuthorizationHeader(new AuthenticationHeaderValue("Bearer", _accessToken));
                    var response = await _apiClient.GetAsync(BaseUrl + _subreddit + "/new.json");
                    var content = await response.Content.ReadAsStringAsync();
                    return JObject.Parse(content);
                }
            }
            catch (System.Exception ex)
            {
                throw;
            }
        }
    }
}
