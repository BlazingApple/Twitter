using BlazingApple.Twitter.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace BlazingAppleConsumer.Twitter.Client.Pages
{
    /// <summary>Main index page.</summary>
    public partial class Index : ComponentBase
    {
        private IEnumerable<Tweet>? _tweets;

        /// <inheritdoc cref="HttpClient" />
        [Inject]
        public HttpClient Http { get; set; } = null!;

        /// <summary>The twitter list to show. Required, if the twitter user is not provided.</summary>
        [Parameter]
        public string? ListId { get; set; }

        /// <summary>The twitter user name, required if List is not provided.</summary>
        [Parameter]
        public string? TwitterUserName { get; set; }

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await GetTweets();
        }

        private async Task GetTweets()
        {
            if (ListId != null)
                _tweets = await Http.GetFromJsonAsync<IEnumerable<Tweet>>($"api/twitter/?listId={ListId}");
            else if (TwitterUserName != null)
                _tweets = await Http.GetFromJsonAsync<IEnumerable<Tweet>>($"api/twitter/?username={TwitterUserName}");
        }
    }
}
