using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace BlazingApple.Twitter.Models;

/// <summary>A set of authors, from twitter.</summary>
public class TwitterAuthorsResponse
{
    /// <summary>A set of authors.</summary>
    public IEnumerable<TwitterAuthor>? Data { get; set; }
}
