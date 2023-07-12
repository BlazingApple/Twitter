using BlazingApple.Twitter.Models;
using Microsoft.AspNetCore.Components;

namespace BlazingApple.Twitter.Components;

/// <summary>Renders a list of tweets.</summary>
public partial class TweetList : ComponentBase
{
	private TwitterHostedList? _embeddedList;
	private string? _oldTwitterId;

	/// <summary>Renders a list of tweets.</summary>
	[Parameter]
	public IEnumerable<Tweet>? Tweets { get; set; }

	/// <summary>Renders tweets using JS and Twitter's embedding technology.</summary>
	[Parameter]
	public string? TwitterId { get; set; }

	/// <inheritdoc cref="TimelineType"/>
	[Parameter]
	public TimelineType TimelineType { get; set; }

	/// <summary>The desired height in pixels.</summary>
	[Parameter]
	public int? HeightInPixels { get; set; }

	/// <summary>The max number of tweets to display</summary>
	/// <remarks>Must be between 1 an 20.</remarks>
	[Parameter]
	public int? MaxCount { get; set; }

	/// <inheritdoc />
	protected override async Task OnParametersSetAsync()
	{
		await base.OnParametersSetAsync();
		if (_oldTwitterId != TwitterId)
		{
			_oldTwitterId = TwitterId;

			if (_embeddedList is not null)
			{
				await _embeddedList.Refresh();
			}
		}
	}
}
