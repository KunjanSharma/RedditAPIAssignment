namespace RedditPostAssignment.Models
{
    public class StatisticModel
    {

        public class Post
        {
            public string? Id { get; set; }
            public string? Title { get; set; }
            public string? Author { get; set; }
            public int Upvotes { get; set; }
        }

        public class UserStats
        {
            public string? Username { get; set; }
            public int PostCount { get; set; }
        }
    }
}
