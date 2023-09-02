namespace RedditPostAssignment.Models
{
    public interface IRedditOAuth
    {
        Task<string?> GetAccessToken();
    }
}
