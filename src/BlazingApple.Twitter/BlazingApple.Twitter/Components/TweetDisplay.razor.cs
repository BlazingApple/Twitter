using BlazingApple.Twitter.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazingApple.Twitter.Components;

/// <summary>Renders a tweet.</summary>
public partial class TweetDisplay : ComponentBase
{
    /// <summary>The tweet to render.</summary>
    [Parameter, EditorRequired]
    public Tweet? Tweet { get; set; }
}
