using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazingApple.Twitter.Models.Entities
{
    /// <summary>A twitter url.</summary>
    public class Url : EntityBase
    {
        /// <summary>The details of the link.</summary>
        public string? Description { get; set; }

        /// <summary>The display url.</summary>
        [JsonPropertyName("display_url")]
        public string? DisplayUrl { get; set; }

        /// <summary>The expanded url.</summary>
        [JsonPropertyName("expanded_url")]
        public string? ExpandedUrl { get; set; }

        /// <summary>Url images.</summary>
        [JsonPropertyName("images")]
        public List<UrlImage>? Images { get; set; }

        /// <summary>The short url (t.co).</summary>
        [JsonPropertyName("url")]
        public string? ShortUrl { get; set; }

        /// <summary>The tooltip/title of the link to display on hover.</summary>
        public string? Title { get; set; }

        /// <summary>The unwound url.</summary>
        [JsonPropertyName("unwound_url")]
        public string? UnwoundUrl { get; set; }
    }
}
