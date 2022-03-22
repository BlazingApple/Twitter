using BlazingApple.Twitter.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazingApple.Twitter.Components;

/// <summary>Renders a list of tweets.</summary>
public partial class TweetList : ComponentBase
{
    /// <summary>Renders a list of tweets.</summary>
    [Parameter, EditorRequired]
    public IEnumerable<Tweet>? Tweets { get; set; }
}
