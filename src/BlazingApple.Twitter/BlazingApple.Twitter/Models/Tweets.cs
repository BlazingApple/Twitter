using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace BlazingApple.Twitter.Models;

/// <summary>DTO Intended to be used by the application.</summary>
public class Tweets : List<Tweet>
{
    /// <summary>Indicates when the tweet collection was instantiated/last requested.</summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>A collection of tweets.</summary>
    /// <param name="tweets">The tweets to add.</param>
    /// <param name="authors">The authors.</param>
    /// <param name="tweetAttachments">The media.</param>
    public Tweets(IEnumerable<Tweet> tweets, IEnumerable<TwitterAuthor> authors, IEnumerable<TweetAttachment>? tweetAttachments = null)
        : base()
    {
        CreatedAt = DateTime.Now;
        Dictionary<string, TwitterAuthor>? authorDictionary = authors.ToDictionary(a => a.Id!);

        Dictionary<string, TweetAttachment>? mediaDictionary = null;
        if (tweetAttachments is not null)
            mediaDictionary = tweetAttachments.ToDictionary(a => a.Id!);

        foreach (Tweet tweet in tweets)
        {
            tweet.Author = authorDictionary[tweet.AuthorId!];
            tweet.Includes = new()
            {
                Media = new List<TweetAttachment>(),
            };

            if (tweet.MediaKeys is not null && mediaDictionary is not null)
            {
                foreach (string mediaKey in tweet.MediaKeys)
                {
                    tweet.Media!.Add(mediaDictionary[mediaKey]);
                }
            }
        }

        AddRange(tweets);
    }
}
