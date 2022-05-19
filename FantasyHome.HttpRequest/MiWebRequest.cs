

using System.Net;
using System.Net.Http.Json;
using FantasyHome.HttpRequest;
using Newtonsoft.Json;
using RestSharp;

namespace FantasyHass.WebRequest;

public class MiWebRequest:IWebRequest
{
    public async Task<string> PostAsync(string url, object? Body, Dictionary<string,string>? Heaeders,Dictionary<string ,string > param)
    {
        try
        {
            RestClient client = new RestClient(WebParam.MiBaseUrl);
            client.Timeout = -1;
            var request = new RestRequest(url);

            if (Heaeders != null)
            {
                request.AddHeaders(Heaeders);
            }

            if (param!=null)
            {
                foreach (var item in param)
                {
                    request.AddParameter(item.Key, item.Value);
                }
            }
         

            if (Body != null)
            {
                request.AddJsonBody(Body);
            }
            var result = await client.ExecuteAsync(request,Method.POST);
            return result.Content;

        }
        catch (Exception e)
        {

            throw;
        }
      
      

    }
}