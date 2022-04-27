using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazingApple.Twitter.Services;

/// <summary>Settings for Twitter.</summary>
/// <seealso cref="TwitterService" />
public class TwitterSettings
{
    /// <summary>Bearer token for requests to the API.</summary>
    public string? BearerToken { get; set; }

    /// <summary>The public api key.</summary>
    public string? Key { get; set; }

    /// <summary>The private secret.</summary>
    public string? KeySecret { get; set; }
}
