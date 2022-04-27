using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazingApple.Twitter.Models.Entities
{
    /// <summary>An AI determined definition of what the tweet is referring to.</summary>
    public class Annotation : EntityBase
    {
        /// <summary>The display text for this concept.</summary>
        [JsonPropertyName("normalized_text")]
        public string? NormalizedText { get; set; }

        /// <summary>Likelihood this annotation is correct.</summary>
        public double Probability { get; set; }

        /// <summary>The type of annotation, like a product, person, etc.</summary>
        public string? Type { get; set; }
    }
}
