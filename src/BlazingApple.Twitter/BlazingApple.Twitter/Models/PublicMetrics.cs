using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazingApple.Twitter.Models;

/// <summary>The public metrics for a tweet.</summary>
public class PublicMetrics
{
    /// <summary>The number of likes</summary>
    [JsonPropertyName("like_count")]
    public int LikeCount { get; set; }

    /// <summary>The number of times quoted</summary>
    [JsonPropertyName("quote_count")]
    public int QuoteCount { get; set; }

    /// <summary>The number of retweets</summary>
    [JsonPropertyName("reply_count")]
    public int ReplyCount { get; set; }

    /// <summary>The number of retweets</summary>
    [JsonPropertyName("retweet_count")]
    public int RetweetCount { get; set; }
}
