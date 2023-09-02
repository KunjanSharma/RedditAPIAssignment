using RedditPostAssignment.Models;

namespace RedditPostAssignment
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Read Reddit credentials from appsettings.json
            var clientId = builder.Configuration["RedditCredentials:ClientId"];
            var clientSecret = builder.Configuration["RedditCredentials:ClientSecret"];
            var username = builder.Configuration["RedditCredentials:Username"];
            var password = builder.Configuration["RedditCredentials:Password"];
            // Create an instance of RateLimitedApiClient
            var apiClient = new RateLimitedApiClient(new HttpClient());

            var redditOAuth = new RedditOAuth(clientId, clientSecret, username, password, apiClient);
            var accessToken = await redditOAuth.GetAccessToken();

            if (accessToken == null)
            {
                throw new Exception("Unable to obtain access token");
            }

            // Register your services in the DI container
            builder.Services.AddSingleton<IRedditOAuth>(_ => redditOAuth);
            builder.Services.AddTransient<IRedditService>(_ => new RedditService(accessToken, apiClient));
            builder.Services.AddControllers();

            var app = builder.Build();

            app.MapGet("/", () => { return Task.CompletedTask; });
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

            app.Run();
        }
    }
}
