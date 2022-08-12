using BlazingApple.Twitter.Models;
using BlazingApple.Twitter.Services;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace BlazingAppleConsumer.TwitterServerless.Pages;

/// <summary>Main index page.</summary>
public partial class Index : ComponentBase
{
    private IEnumerable<Tweet>? _tweets;

    /// <summary>The twitter list to show. Required, if the twitter user is not provided.</summary>
    [Parameter]
    public string? ListId { get; set; }

    /// <summary>The twitter user name, required if List is not provided.</summary>
    [Parameter]
    public string? TwitterUserName { get; set; }

    /// <summary>
    ///     This is the primary service used to interact with Twitter's REST API. Normally, you'd want this on the server, not the client, so as not
    ///     to expose any keys.
    /// </summary>
    [Inject]
    private TwitterService TwitterService { get; set; } = null!;

    /// <inheritdoc />
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        await GetTweets();
    }

    private async Task GetTweets()
    {
        if (ListId != null)
            _tweets = await TwitterService.GetTweetsFromList(ListId);
        else if (TwitterUserName != null)
            _tweets = await TwitterService.GetTweetsForUser(TwitterUserName);
    }
}
