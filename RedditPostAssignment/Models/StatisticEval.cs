using Newtonsoft.Json.Linq;
using System.Collections.Concurrent;

namespace RedditPostAssignment.Models
{
    public class StatisticEval
    {
        private readonly ConcurrentDictionary<string, int> _userPostCounts = new ConcurrentDictionary<string, int>();
        private readonly ConcurrentDictionary<string, int> _postUpvotes = new ConcurrentDictionary<string, int>();

        public void TrackPost(JObject post)
        {
            var data = post?["data"];
            if (data == null) return;

            var user = data["author"]?.ToString();
            var upvotesString = data["ups"]?.ToString();
            var postId = data["id"]?.ToString();

            if (user == null || upvotesString == null || postId == null) return;

            if (!int.TryParse(upvotesString, out var upvotes)) return;

            _userPostCounts.AddOrUpdate(user, 1, (key, count) => count + 1);
            _postUpvotes.AddOrUpdate(postId, upvotes, (key, oldValue) => upvotes);
        }

        public KeyValuePair<string, int> GetUserWithMostPosts()
        {
            return _userPostCounts.OrderByDescending(kv => kv.Value).FirstOrDefault();
        }

        public KeyValuePair<string, int> GetPostWithMostUpvotes()
        {
            return _postUpvotes.OrderByDescending(kv => kv.Value).FirstOrDefault();
        }
    }
}
