using BlazingApple.Twitter.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazingApple.Twitter.Models
{
    /// <summary>
    ///     Tweet entities, which have been parsed out of the text of the tweet:
    ///     <list type="bullet">
    ///         <item>hashtag</item>
    ///         <item>urls</item>
    ///         <item>user mentions</item>
    ///         <item>cashtags</item>
    ///     </list>
    /// </summary>
    /// <remarks><c>Start</c> indices are inclusive, the majority of <c>end</c> indices are exclusive, except for <c>entities.annotations.end</c>.</remarks>
    public class EntityObjects
    {
        /// <inheritdoc cref="Annotation" />
        [JsonPropertyName("annotations")]
        public List<Annotation>? Annotations { get; set; }

        /// <inheritdoc cref="CashTag" />
        [JsonPropertyName("cashtags")]
        public List<CashTag>? Cashtags { get; set; }

        /// <inheritdoc cref="HashTag" />
        [JsonPropertyName("hashtags")]
        public List<HashTag>? Hashtags { get; set; }

        /// <inheritdoc cref="Mention" />
        [JsonPropertyName("mentions")]
        public List<Mention>? Mentions { get; set; }

        /// <inheritdoc cref="Url" />
        [JsonPropertyName("urls")]
        public List<Url>? Urls { get; set; }

        /// <summary>Get the list of entities.</summary>
        /// <returns>See above.</returns>
        public List<EntityBase> ToList()
        {
            List<EntityBase> list = new();

            if (Urls is not null)
                list.AddRange(Urls);
            if (Mentions is not null)
                list.AddRange(Mentions);
            if (Hashtags is not null)
                list.AddRange(Hashtags);
            if (Cashtags is not null)
                list.AddRange(Cashtags);
            if (Annotations is not null)
                list.AddRange(Annotations);

            return list;
        }
    }
}
