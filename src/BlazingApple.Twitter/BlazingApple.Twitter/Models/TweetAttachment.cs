using System.Text.Json.Serialization;

namespace BlazingApple.Twitter.Models;

/// <summary>A single media or poll attachment to a tweet</summary>
public class TweetAttachment
{
    /// <summary>The duration in milliseconds of the video, if a video.</summary>
    [JsonPropertyName("duration_ms")]
    public int? DurationInMilliseconds { get; set; }

    /// <summary>The height of the media, in pixels</summary>
    public int Height { get; set; }

    /// <summary>The id/media-key</summary>
    [JsonPropertyName("media_key")]
    public string? Id { get; set; }

    /// <summary>The type of the media ( <c>poll</c> or <c>photo</c> or <c>video</c>).</summary>
    public string? Type { get; set; }

    /// <summary>The url for the image or poll.</summary>
    [JsonPropertyName("url")]
    public string? Url { get; set; }

    /// <summary>The number of views, if a video.</summary>
    [JsonPropertyName("public_metrics.view_count")]
    public int? ViewCount { get; set; }

    /// <summary>The width of the media, in pixels</summary>
    public int Width { get; set; }
}
