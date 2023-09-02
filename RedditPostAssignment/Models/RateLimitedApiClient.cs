using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
namespace RedditPostAssignment.Models
{
    public interface IRateLimitedApiClient
    {
        Task<HttpResponseMessage> GetAsync(string url);
        Task<HttpResponseMessage> PostAsync(string url, HttpContent content);
        void SetDefaultRequestHeader(string headerName, string headerValue);

        void SetAuthorizationHeader(AuthenticationHeaderValue authHeaderValue);
    }
    public class RateLimitedApiClient: IRateLimitedApiClient
    {
        private readonly HttpClient _httpClient;

        public RateLimitedApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }
        public void SetDefaultRequestHeader(string headerName, string headerValue)
        {
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation(headerName, headerValue);
        }

        public void SetAuthorizationHeader(AuthenticationHeaderValue authHeaderValue)
        {
            _httpClient.DefaultRequestHeaders.Authorization = authHeaderValue;
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            await HandleRateLimitingAsync(response);
            return response;
        }

        public async Task<HttpResponseMessage> PostAsync(string url, HttpContent content)
        {
            HttpResponseMessage response = await _httpClient.PostAsync(url, content);
            await HandleRateLimitingAsync(response);
            return  response;
        }

        // You can add other HTTP methods (PUT, DELETE, etc.) in a similar fashion if required.

        private async Task HandleRateLimitingAsync(HttpResponseMessage response)
        {
            if (response.Headers.TryGetValues("X-Ratelimit-Remaining", out var rateLimitRemainingValues))
            {
                double remaining = double.Parse(rateLimitRemainingValues.First());

                if (remaining <= 0)
                {
                    if (response.Headers.TryGetValues("X-Ratelimit-Reset", out var rateLimitResetValues))
                    {
                        double resetInSeconds = double.Parse(rateLimitResetValues.First());
                        await Task.Delay(TimeSpan.FromSeconds(resetInSeconds + 1)); // +1 for safety
                    }
                }
            }
        }

        //private async Task<JObject> ParseResponseAsync(HttpResponseMessage response)
        //{
        //    if (!response.IsSuccessStatusCode)
        //    {
        //        throw new HttpRequestException($"API returned status code: {response.StatusCode}");
        //    }

        //    string responseBody = await response.Content.ReadAsStringAsync();
        //    return JObject.Parse(responseBody);
        //}
    }
}
