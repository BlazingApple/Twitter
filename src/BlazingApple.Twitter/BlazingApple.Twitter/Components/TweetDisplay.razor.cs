using BlazingApple.Twitter.Models;
using BlazingApple.Twitter.Models.Entities;
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
    private string? _tweetMarkupText;

    /// <summary>The tweet to render.</summary>
    [Parameter, EditorRequired]
    public Tweet? Tweet { get; set; }

    /// <inheritdoc />
    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        if (Tweet is null || Tweet.Entities is null || Tweet.Text is null)
            return;

        List<IGrouping<int, EntityBase>> entitiesByStartIndex = Tweet.Entities
            .ToList()
            .GroupBy(e => e.Start)
            .ToList();

        _tweetMarkupText = "";
        for (int i = 0; i < Tweet.Text.Length; i++)
        {
            if (entitiesByStartIndex.Any(g => g.Key == i))
            {
                var group = entitiesByStartIndex.Single(g => g.Key == i);
                entitiesByStartIndex.Remove(group); // don't need to search this anymore.
                EntityBase entity = group.First();
                string? htmlContent = ProcessGroupForHtml(group);

                if (htmlContent != null)
                {
                    _tweetMarkupText += htmlContent;
                    i = entity.End - 1;
                }
                else
                {
                    _tweetMarkupText += Tweet.Text[i];
                }
            }
            else
            {
                _tweetMarkupText += Tweet.Text[i];
            }
            _tweetMarkupText = _tweetMarkupText.Replace("\n", "<br/>");
        }
    }

    private static string? ProcessGroupForHtml(IGrouping<int, EntityBase> group)
    {
        string? html = group.First() switch
        {
            Url url => $"<a target=\"_blank\" title=\"{url.Description}\" href=\"{url.ShortUrl}\">{url.ShortUrl}</a>",
            Mention mention => $"<a target=\"_blank\" href=\"https://twitter.com/{mention.Username}\">@{mention.Username}</a>",
            HashTag tag => $"<a target=\"_blank\" href=\"https://twitter.com/hashtag/{tag.Tag}?src=hashtag_click>#{tag.Tag}</a>",
            _ => null,
        };
        return html;
    }
}
