using Newtonsoft.Json.Linq;

namespace RedditPostAssignment.Models
{
    public interface IRedditService
    {
        Task<JObject> GetLatestPosts();
    }
}
