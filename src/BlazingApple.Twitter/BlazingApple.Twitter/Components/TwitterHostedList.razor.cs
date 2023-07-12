using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazingApple.Twitter.Components;

/// <summary>
/// Renders a list using Javascript and iframes hosted on Twitter.
/// </summary>
public partial class TwitterHostedList : ComponentBase
{
	[Inject]
	private IJSRuntime JSRuntime { get; set; } = null!;

	/// <summary>Id of the resource to consume on Twitter.</summary>
	/// <remarks>This is the <c>list id</c> of the twitter list, or a <c>user's profile/screen name</c>, pending on <see cref="Type"/></remarks>
	[Parameter]
	public string? Id { get; set; }

	/// <summary>
	/// The id on the element in which to render the list.
	/// </summary>
	[Parameter]
	public string ElementId { get; set; } = "ba-twitter-timeline";

	/// <summary><see cref="TimelineType"/></summary>
	[Parameter]
	public TimelineType Type { get; set; }

	/// <summary>The desired height in pixels.</summary>
	[Parameter]
	public int? HeightInPixels { get; set; }

	/// <summary>The max number of tweets to display</summary>
	/// <remarks>Must be between 1 an 20.</remarks>
	[Parameter]
	public int? MaxCount { get; set; }

	/// <inheritdoc />
	protected override void OnInitialized()
	{
		base.OnInitialized();

		if (Id is null)
		{
			throw new InvalidOperationException("Missing required twitter id to render tweets");
		}
	}

	/// <inheritdoc />
	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		await base.OnAfterRenderAsync(firstRender);
		await LoadData();
	}

	private async Task LoadData()
	{
		string functionName = Type switch
		{
			TimelineType.List => "createListTimeline",
			TimelineType.Profile => "createProfileTimeline",
			_ => throw new ArgumentOutOfRangeException(nameof(Type)),
		};

		if (Id is not null)
		{
			await JSRuntime.InvokeVoidAsync(functionName, ElementId, Id, HeightInPixels, MaxCount);
		}
	}

	/// <summary>
	/// Refresh the twitter list.
	/// </summary>
	/// <returns>Async op.</returns>
	public async Task Refresh()
	{
		await LoadData();
	}
}

/// <summary>The type of timeline to render.</summary>
public enum TimelineType
{
	/// <summary>A twitter list compiled of many profiles.</summary>
	List,
	/// <summary>The tweets from a single profile.</summary>
	Profile
}