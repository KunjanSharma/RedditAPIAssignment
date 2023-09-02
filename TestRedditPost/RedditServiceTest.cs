using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using Newtonsoft.Json.Linq;
using RedditPostAssignment.Models;
using Xunit;

namespace RedditServiceTest
{
    public class RedditServiceTests
    {
        [Fact]
        public async Task GetLatestPosts_ValidAccessToken_ReturnsJObject()
        {
            // Arrange

            var responseContent = @"{
        'data': {
            'children': [
                {
    ""kind"": ""t3"",
    ""data"": {
      ""approved_at_utc"": null,
      ""subreddit"": ""worldnews"",
      ""selftext"": """",
      ""author_fullname"": ""t2_buk5z"",
      ""saved"": false,
      ""mod_reason_title"": null,
      ""gilded"": 0,
      ""clicked"": false,
      ""title"": ""Ukraine war: US sees notable progress; by Ukraine army in south"",
      ""link_flair_richtext"": [
        {
          ""e"": ""text"",
          ""t"": ""Russia/Ukraine""
        }
      ],
      ""subreddit_name_prefixed"": ""r/worldnews"",
      ""hidden"": false,
      ""pwls"": 6,
      ""link_flair_css_class"": ""russia"",
      ""downs"": 0,
      ""thumbnail_height"": 78,
      ""top_awarded_type"": null,
      ""hide_score"": true,
      ""name"": ""t3_167jr9b"",
      ""quarantine"": false,
      ""link_flair_text_color"": null,
      ""upvote_ratio"": 0.75,
      ""author_flair_background_color"": null,
      ""subreddit_type"": ""public"",
      ""ups"": 2,
      ""total_awards_received"": 0,
      ""media_embed"": {},
      ""thumbnail_width"": 140,
      ""author_flair_template_id"": null,
      ""is_original_content"": false,
      ""user_reports"": [],
      ""secure_media"": null,
      ""is_reddit_media_domain"": false,
      ""is_meta"": false,
      ""category"": null,
      ""secure_media_embed"": {},
      ""link_flair_text"": ""Russia/Ukraine"",
      ""can_mod_post"": false,
      ""score"": 2,
      ""approved_by"": null,
      ""is_created_from_ads_ui"": false,
      ""author_premium"": false,
      ""thumbnail"": ""https://b.thumbs.redditmedia.com/2n7a8fjiRIL2F0lN0g3E7xX8bA7tVysYvGO396ZkWRA.jpg"",
      ""edited"": false,
      ""author_flair_css_class"": null,
      ""author_flair_richtext"": [],
      ""gildings"": {},
      ""post_hint"": ""link"",
      ""content_categories"": null,
      ""is_self"": false,
      ""mod_note"": null,
      ""created"": 1693603921.0,
      ""link_flair_type"": ""richtext"",
      ""wls"": 6,
      ""removed_by_category"": null,
      ""banned_by"": null,
      ""author_flair_type"": ""text"",
      ""domain"": ""bbc.com"",
      ""allow_live_comments"": false,
      ""selftext_html"": null,
      ""likes"": null,
      ""suggested_sort"": null,
      ""banned_at_utc"": null,
      ""url_overridden_by_dest"": ""https://www.bbc.com/news/world-europe-66686149"",
      ""view_count"": null,
      ""archived"": false,
      ""no_follow"": false,
      ""is_crosspostable"": true,
      ""pinned"": false,
      ""over_18"": false,
      ""preview"": {
        ""images"": [
          {
            ""source"": {
              ""url"": ""https://external-preview.redd.it/cRej3zWEs0PBdCdUsr4e_gaPf5q4OPLGPsg0zQy1paQ.jpg?auto=webp&amp;s=79cb60294127678973e929e61416409c60cdba54"",
              ""width"": 1024,
              ""height"": 576
            },
            ""resolutions"": [
              {
                ""url"": ""https://external-preview.redd.it/cRej3zWEs0PBdCdUsr4e_gaPf5q4OPLGPsg0zQy1paQ.jpg?width=108&amp;crop=smart&amp;auto=webp&amp;s=32edb8cd41e380beef860c4640e7cda1b584f647"",
                ""width"": 108,
                ""height"": 60
              },
              {
                ""url"": ""https://external-preview.redd.it/cRej3zWEs0PBdCdUsr4e_gaPf5q4OPLGPsg0zQy1paQ.jpg?width=216&amp;crop=smart&amp;auto=webp&amp;s=4d8aea9856cbdce1cbd115a7edcd4705987582d7"",
                ""width"": 216,
                ""height"": 121
              },
              {
                ""url"": ""https://external-preview.redd.it/cRej3zWEs0PBdCdUsr4e_gaPf5q4OPLGPsg0zQy1paQ.jpg?width=320&amp;crop=smart&amp;auto=webp&amp;s=3d4f168d86ccba21ce4760804b950dd1565e26a4"",
                ""width"": 320,
                ""height"": 180
              },
              {
                ""url"": ""https://external-preview.redd.it/cRej3zWEs0PBdCdUsr4e_gaPf5q4OPLGPsg0zQy1paQ.jpg?width=640&amp;crop=smart&amp;auto=webp&amp;s=a9d8100e1cc4e494f7afba87dd1ce69ff48e83c8"",
                ""width"": 640,
                ""height"": 360
              },
              {
                ""url"": ""https://external-preview.redd.it/cRej3zWEs0PBdCdUsr4e_gaPf5q4OPLGPsg0zQy1paQ.jpg?width=960&amp;crop=smart&amp;auto=webp&amp;s=b4a9f37dd2d63bdf0307c615cc2f41b03375c850"",
                ""width"": 960,
                ""height"": 540
              }
            ],
            ""variants"": {},
            ""id"": ""uLCNzIdp8D4CuUeav12vE6IJPfhJVnfPKH4FDxQ1frc""
          }
        ],
        ""enabled"": false
      },
      ""all_awardings"": [],
      ""awarders"": [],
      ""media_only"": false,
      ""can_gild"": true,
      ""spoiler"": false,
      ""locked"": false,
      ""author_flair_text"": null,
      ""treatment_tags"": [],
      ""visited"": false,
      ""removed_by"": null,
      ""num_reports"": null,
      ""distinguished"": null,
      ""subreddit_id"": ""t5_2qh13"",
      ""author_is_blocked"": false,
      ""mod_reason_by"": null,
      ""removal_reason"": null,
      ""link_flair_background_color"": null,
      ""id"": ""167jr9b"",
      ""is_robot_indexable"": true,
      ""report_reasons"": null,
      ""author"": ""sometimes_correct"",
      ""discussion_type"": null,
      ""num_comments"": 0,
      ""send_replies"": true,
      ""whitelist_status"": ""all_ads"",
      ""contest_mode"": false,
      ""mod_reports"": [],
      ""author_patreon_flair"": false,
      ""author_flair_text_color"": null,
      ""permalink"": ""/r/worldnews/comments/167jr9b/ukraine_war_us_sees_notable_progress_by_ukraine/"",
      ""parent_whitelist_status"": ""all_ads"",
      ""stickied"": false,
      ""url"": ""https://www.bbc.com/news/world-europe-66686149"",
      ""subreddit_subscribers"": 32870001,
      ""created_utc"": 1693603921.0,
      ""num_crossposts"": 0,
      ""media"": null,
      ""is_video"": false
    }
  }
            ]
        }
    }";  // Mock the response as per your requirement

            var apiClientMock = new Mock<IRateLimitedApiClient>();

            // Mock the HttpMessageHandler
            apiClientMock.Setup(ac => ac.GetAsync(It.IsAny<string>()))
                         .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
                         {
                             Content = new StringContent(responseContent)
                         });

            var redditService = new RedditService("sample-token", apiClientMock.Object);

            // use a mock token

            // Act
            var result = await redditService.GetLatestPosts();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<JObject>(result);
        }
    }
}
