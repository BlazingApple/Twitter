using BlazingApple.Twitter.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace BlazingApple.Twitter.Services;

/// <summary>Retrieves Twitter data.</summary>
public sealed class TwitterService
{
    private const string _baseAddress = "https://api.twitter.com/2/";
    private const string _defaultTwitterError = "Null data from twitter";
    private readonly HttpClient _httpClient;
    private readonly Dictionary<string, Tweets> _tweetCache;
    private DateTime _lastPurge;

    /// <summary>DI Constructor.</summary>
    public TwitterService(IConfiguration config, HttpClient httpClient)
    {
        string bearerToken = config.GetSection("Twitter")["BearerToken"];
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(_baseAddress);
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
        _tweetCache = new Dictionary<string, Tweets>();
        _lastPurge = DateTime.Now;
    }

    /// <summary>Get the recent tweets for a user.</summary>
    /// <param name="username">The username</param>
    /// <returns>The tweets for a user.</returns>
    public async Task<IEnumerable<Tweet>> GetTweetsForUser(string username)
    {
        string authorsUrl = $"users/by/username/{username}?user.fields=profile_image_url";
        TwitterAuthorsResponse? authorsResponse = await _httpClient.GetFromJsonAsync<TwitterAuthorsResponse>(authorsUrl);
        if (authorsResponse is null || authorsResponse.Data is null)
            throw new InvalidOperationException(_defaultTwitterError);

        TwitterAuthor author = authorsResponse.Data.First();
        string url = $"users/{author.Id}/tweets?" + GetQueryStringParams();

        TwitterResponse? twitterResponse = await _httpClient.GetFromJsonAsync<TwitterResponse>(url);

        if (twitterResponse is null || twitterResponse.Data is null)
            throw new InvalidOperationException(_defaultTwitterError);

        return new Tweets(twitterResponse.Data, authorsResponse.Data, twitterResponse.Media);
    }

    /// <summary>Get the latest tweets for a list.</summary>
    /// <param name="listId">The list whose tweets to retrieve.</param>
    /// <param name="maxCount">If provided, the service will only return <c>maxCount</c> number of results.</param>
    /// <returns>The tweets for the list.</returns>
    public async Task<IEnumerable<Tweet>> GetTweetsFromList(string listId, int? maxCount = null)
    {
        RemoveStaleDataFromCache();
        return await TryGetListFromCache(listId, maxCount);
    }

    private static string GetQueryStringParams()
    {
        string returnVal = "tweet.fields=created_at,author_id,public_metrics,attachments,entities"
            + "&expansions=attachments.media_keys"
            + "&media.fields=duration_ms,height,media_key,preview_image_url,public_metrics,type,url,width,alt_text"
            + "&user.fields=profile_image_url";
        return returnVal;
    }

    private static bool IsStale(Tweets tweets)
        => tweets.CreatedAt < DateTime.Now - TimeSpan.FromMinutes(2);

    private Tweets AddToCache(string id, Tweets tweets)
    {
        if (_tweetCache.ContainsKey(id))
            _tweetCache.Remove(id);

        _tweetCache.Add(id, tweets);
        return tweets;
    }

    private async Task<Tweets> GetFromListInternal(string listId, int? maxCount = null)
    {
        string url = $"lists/{listId}/tweets?" + GetQueryStringParams();
        TwitterResponse? twitterResponse = await _httpClient.GetFromJsonAsync<TwitterResponse>(url);

        if (twitterResponse != null && twitterResponse.Data is not null)
        {
            if (maxCount.HasValue)
                twitterResponse.Data = twitterResponse.Data.Take(maxCount.Value);

            IEnumerable<string> authors = twitterResponse.GetAuthors();
            string userIds = string.Join(',', authors);
            string authorsUrl = $"users?ids={userIds}&user.fields=profile_image_url";

            TwitterAuthorsResponse? authorsResponse = await _httpClient.GetFromJsonAsync<TwitterAuthorsResponse>(authorsUrl);

            if (authorsResponse is null || authorsResponse.Data is null)
                throw new InvalidOperationException(_defaultTwitterError);

            return new Tweets(twitterResponse.Data, authorsResponse.Data, twitterResponse.Media);
        }
        else
        {
            throw new InvalidOperationException(_defaultTwitterError);
        }
    }

    private void RemoveStaleDataFromCache()
    {
        if (_lastPurge >= DateTime.Now - TimeSpan.FromMinutes(2))
            return;
        else
            _lastPurge = DateTime.Now;

        List<KeyValuePair<string, Tweets>> staleData = new();
        foreach (KeyValuePair<string, Tweets> cacheVal in _tweetCache)
        {
            if (IsStale(cacheVal.Value))
                staleData.Add(cacheVal);
        }
        foreach (KeyValuePair<string, Tweets> cacheVal in staleData)
        {
            if (_tweetCache.ContainsKey(cacheVal.Key))
                _tweetCache.Remove(cacheVal.Key);
        }
    }

    private async Task<Tweets> TryGetListFromCache(string listId, int? maxCount = null)
    {
        if (_tweetCache.TryGetValue(listId, out Tweets? cache))
        {
            if (cache is null || IsStale(cache))
            {
                Tweets tweets = await GetFromListInternal(listId, maxCount);
                AddToCache(listId, tweets);

                return tweets;
            }
            else
            {
                return cache;
            }
        }
        else
        {
            Tweets tweets = await GetFromListInternal(listId, maxCount);
            AddToCache(listId, tweets);
            return tweets;
        }
    }
}
