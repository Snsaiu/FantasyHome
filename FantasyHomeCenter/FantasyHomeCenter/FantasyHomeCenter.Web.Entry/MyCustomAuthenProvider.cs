using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using FantasyHomeCenter.Application.UserCenter;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace FantasyHomeCenter.Web.Entry
{
	public class MyCustomAuthenProvider: AuthenticationStateProvider
	{
		
		private readonly ProtectedLocalStorage localStorage;
		private readonly HttpClient httpClient;

		public MyCustomAuthenProvider(ProtectedLocalStorage localStorage,HttpClient httpClient)
		{
			this.localStorage = localStorage;
			this.httpClient = httpClient;
		}

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {


	        var tokenResult =await this.localStorage.GetAsync<string>("token");
	        string token = "";
	        if (tokenResult.Success)
		        token = tokenResult.Value;

	        var identity = new ClaimsIdentity();
	        this.httpClient.DefaultRequestHeaders.Authorization = null;

	        if (!string.IsNullOrEmpty(token))
	        {
		        identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
		        this.httpClient.DefaultRequestHeaders.Authorization =
			        new AuthenticationHeaderValue("Bearer", token.Replace("\"", ""));
	        }

	        var user = new ClaimsPrincipal(identity);
	        var state = new AuthenticationState(user);

	        NotifyAuthenticationStateChanged(Task.FromResult(state));

	        return state;

        }
        
        
        public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
	        var payload = jwt.Split('.')[1];
	        var jsonBytes = ParseBase64WithoutPadding(payload);
	        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
	        return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
	        switch (base64.Length % 4)
	        {
		        case 2: base64 += "=="; break;
		        case 3: base64 += "="; break;
	        }
	        return Convert.FromBase64String(base64);
        }
	}
}

