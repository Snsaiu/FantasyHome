
using FantasyHomeCenter.Web.Core;
using FantasyHomeCenter.Web.Entry;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args).Inject();
builder.Services.AddAntDesign();
builder.Services.AddRemoteRequest();
builder.Services.AddScoped<AuthenticationStateProvider, MyCustomAuthenProvider>();
builder.Services.AddAuthenticationCore();

var app = builder.Build();
app.Run();
