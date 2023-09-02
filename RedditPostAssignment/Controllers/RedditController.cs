using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using RedditPostAssignment.Models;

[Route("api/[controller]")]
[ApiController]
public class RedditController : ControllerBase
{
    private readonly IRedditOAuth _redditOAuth;
    private readonly IRedditService _redditService;

    public RedditController(IRedditOAuth redditOAuth, IRedditService redditService)
    {
        _redditOAuth = redditOAuth;
        _redditService = redditService;
    }

    [HttpGet("getAccessToken")]
    public async Task<IActionResult> GetAccessToken()
    {
        try
        {
            var accessToken = await _redditOAuth.GetAccessToken();
            if (string.IsNullOrEmpty(accessToken))
            {
                return BadRequest("Unable to obtain access token.");
            }

            return Ok(new { AccessToken = accessToken });
        }
        catch (Exception ex)
        {
            // Log the exception (if you have logging set up)
            return StatusCode(500, "Internal server error. Please try again later.");
        }
    }

    [HttpGet("getLatestPosts")]
    public async Task<IActionResult> GetLatestPosts()
    {
        try
        {
            var statsService = new StatisticEval();
            Newtonsoft.Json.Linq.JObject latestPosts = await _redditService.GetLatestPosts();
            if (latestPosts == null)
            {
                return BadRequest("Failed to fetch latest posts.");
            }
            foreach (var post in latestPosts["data"]!["children"]!)
            {
                statsService.TrackPost((Newtonsoft.Json.Linq.JObject)post);
            }

            var topUser = statsService.GetUserWithMostPosts();
            var topPost = statsService.GetPostWithMostUpvotes();

            return Ok(new
            {
                TopUser = topUser,
                TopPost = topPost
            });
        }
        catch (Exception ex)
        {
            // Log the exception (if you have logging set up)
            return StatusCode(500, "Internal server error. Please try again later.");
        }
    }
}
