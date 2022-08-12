using BlazingApple.Twitter.Models;
using BlazingApple.Twitter.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazingAppleConsumer.Twitter.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TwitterController : ControllerBase
    {
        private readonly TwitterService _twitterService;

        /// <summary>Constructor accepts DI services.</summary>
        public TwitterController(TwitterService twitterService)
            => _twitterService = twitterService;

        /// <summary>Get tweets for homepage.</summary>
        /// <returns>A collection of tweets.</returns>
        [HttpGet]
        public async Task<IEnumerable<Tweet>> GetTweets(string? username = null, string? listId = null)
        {
            IEnumerable<Tweet> results;

            if (listId is not null)
                results = await _twitterService.GetTweetsFromList(listId);
            else if (username is not null)
                results = await _twitterService.GetTweetsForUser(username);
            else
                throw new InvalidOperationException("Both userId and listId are null, which is invalid");
            return results.Take(15);
        }
    }
}
