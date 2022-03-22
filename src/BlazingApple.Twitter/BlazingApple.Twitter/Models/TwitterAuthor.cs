using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace BlazingApple.Twitter.Models;

/// <summary>An author of a tweet.</summary>
public class TwitterAuthor
{
    /// <summary>The public name for the user.</summary>
    [JsonPropertyName("name")]
    public string? DisplayName { get; set; }

    /// <summary>Unique identifier.</summary>
    public string? Id { get; set; }

    /// <summary>The user's profile image url.</summary>
    [JsonPropertyName("profile_image_url")]
    public string? ProfileImageUrl { get; set; }

    /// <summary>The link to their profile.</summary>
    public string? Url => $"https://twitter.com/{UserName}";

    /// <summary>The user's username/unique identifier.</summary>
    [JsonPropertyName("username")]
    public string? UserName { get; set; }
}
