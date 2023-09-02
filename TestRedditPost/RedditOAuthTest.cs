using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using Newtonsoft.Json.Linq;
using RedditPostAssignment.Models;
using Xunit;

namespace TestRedditPost
{
    public class RedditOAuthTest
    {
        [Fact]
        public async Task GetAccessToken_ReturnsAccessToken_WhenResponseIsValid()
        {
            // Arrange
            var apiClientMock = new Mock<IRateLimitedApiClient>();
            apiClientMock.Setup(ac => ac.PostAsync(It.IsAny<string>(), It.IsAny<HttpContent>()))
                         .ReturnsAsync(new HttpResponseMessage
                         {
                             StatusCode = HttpStatusCode.OK,
                             Content = new StringContent("{\"access_token\": \"test-token\"}"),
                         });

            var redditOAuth = new RedditOAuth("testClientId", "testClientSecret", "testUsername", "testPassword", apiClientMock.Object);

            // Act
            var token = await redditOAuth.GetAccessToken();

            // Assert
            Assert.Equal("test-token", token);
            apiClientMock.Verify(ac => ac.PostAsync(It.IsAny<string>(), It.IsAny<HttpContent>()), Times.Once());
        }

        [Fact]
        public async Task GetAccessToken_HandlesException_ReturnsNull()
        {
            // Arrange
            var apiClientMock = new Mock<IRateLimitedApiClient>();
            apiClientMock.Setup(ac => ac.PostAsync(It.IsAny<string>(), It.IsAny<HttpContent>()))
                         .ReturnsAsync(new HttpResponseMessage
                         {
                             StatusCode = HttpStatusCode.OK,
                             Content = new StringContent("{\"access_token\": \"test-token\"}"),
                         });
            apiClientMock.Setup(ac => ac.PostAsync(It.IsAny<string>(), It.IsAny<HttpContent>()))
             .Throws(new HttpRequestException("An error occurred."));


            //var httpClient = new HttpClient(mockHandler.Object);

            var redditOAuth = new RedditOAuth("sample-clientId", "sample-clientSecret", "sample-username", "sample-password", apiClientMock.Object);

            // Act
            var result = await redditOAuth.GetAccessToken();

            // Assert
            Assert.Null(result);
        }

        // ... Additional test cases ...
    }
}
