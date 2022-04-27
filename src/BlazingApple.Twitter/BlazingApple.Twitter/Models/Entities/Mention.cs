using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazingApple.Twitter.Models.Entities
{
    /// <summary>Mentions (@username)'s in twitter.</summary>
    public class Mention : EntityBase
    {
        /// <summary>The user id of the user being "atted".</summary>
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        /// <summary>The user being "atted".</summary>
        [JsonPropertyName("username")]
        public string? Username { get; set; }
    }
}
