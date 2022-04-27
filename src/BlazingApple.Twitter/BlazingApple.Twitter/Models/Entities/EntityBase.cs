using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazingApple.Twitter.Models.Entities
{
    /// <summary>Base entity class, shared properties.</summary>
    public class EntityBase
    {
        /// <summary>The end index. Usually <c>exclusive</c>, except for <see cref="Annotation" />.</summary>
        [JsonPropertyName("end")]
        public int End { get; set; }

        /// <summary>The start index. Always <c>inclusive</c>.</summary>
        [JsonPropertyName("start")]
        public int Start { get; set; }

        /// <summary>The mentioned hashtag, user, or cashtag tied to the <see cref="EntityBase" />.</summary>
        [JsonPropertyName("tag")]
        public string? Tag { get; set; }
    }
}
