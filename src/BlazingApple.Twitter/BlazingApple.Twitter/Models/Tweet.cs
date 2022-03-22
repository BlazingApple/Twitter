using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace BlazingApple.Twitter.Models;

/// <summary>Represents a tweet, from Twitter.</summary>
public class Tweet
{
    /// <summary>List of keys referencing media.</summary>
    [JsonPropertyName("attachments")]
    public MediaAttachments? Attachments { get; set; }

    /// <summary>The author object.</summary>
    public TwitterAuthor? Author { get; set; }

    /// <summary>The author's twitter id</summary>
    [JsonPropertyName("author_id")]
    public string? AuthorId { get; set; }

    /// <summary>The datetime the tweet was created.</summary>
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    /// <summary>The id for the tweet.</summary>
    public string? Id { get; set; }

    /// <summary>Only used to properly deserialize without writing a custom converter.</summary>
    [JsonPropertyName("includes")]
    public MediaIncludes? Includes { get; set; }

    /// <summary>Media relating to the tweet, if any.</summary>
    public List<TweetAttachment>? Media => Includes?.Media;

    /// <summary>The keyed identifiers referring to media on the object.</summary>
    public IEnumerable<string>? MediaKeys => Attachments?.MediaKeys;

    /// <inheritdoc cref="PublicMetrics" />
    [JsonPropertyName("public_metrics")]
    public PublicMetrics? Metrics { get; set; }

    /// <summary>The text of the tweet.</summary>
    public string? Text { get; set; }

    /// <summary>The link to the tweet.</summary>
    public string? Url => $"https://twitter.com/{Author?.UserName}/status/{Id}";

    /// <summary>An object provided by twitter to group keyed media content.</summary>
    public class MediaAttachments
    {
        /// <summary>Identifiers indicating which media contents to render.</summary>
        [JsonPropertyName("media_keys")]
        public IEnumerable<string>? MediaKeys { get; set; }
    }

    /// <summary>Nested property access.</summary>
    public class MediaIncludes
    {
        /// <summary>The media objects.</summary>
        [JsonPropertyName("media")]
        public List<TweetAttachment>? Media { get; set; }
    }
}
