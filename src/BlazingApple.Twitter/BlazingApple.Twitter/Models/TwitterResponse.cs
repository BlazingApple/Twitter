using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using static BlazingApple.Twitter.Models.Tweet;

namespace BlazingApple.Twitter.Models;

/// <summary>Response from Twitter</summary>
public class TwitterResponse
{
    /// <summary>The dictionary of authors, keyed by identifier.</summary>
    public Dictionary<string, TwitterAuthor>? Authors { get; set; }

    /// <summary>The collection of tweets and their data.</summary>
    public IEnumerable<Tweet>? Data { get; set; }

    /// <summary>Only used to properly deserialize without writing a custom converter.</summary>
    [JsonPropertyName("includes")]
    public MediaIncludes? Includes { get; set; }

    /// <summary>The collection of tweets and their data.</summary>
    public IEnumerable<TweetAttachment>? Media => Includes?.Media;

    /// <summary>Gets the list of authors from a set of tweets.</summary>
    /// <returns>A set of author ids.</returns>
    public IEnumerable<string> GetAuthors()
    {
        if (Data is null)
            return Enumerable.Empty<string>();
        else
            return Data.Select(tweet => tweet.AuthorId!).Distinct();
    }
}
