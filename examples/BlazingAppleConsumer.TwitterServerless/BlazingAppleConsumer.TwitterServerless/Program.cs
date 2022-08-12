using BlazingApple.Components.Configuration;
using BlazingApple.Twitter.Services;
using BlazingAppleConsumer.TwitterServerless;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddBlazingAppleComponents(builder.Configuration);
builder.Services.AddTwitter(builder.Configuration);
await builder.Build().RunAsync();
