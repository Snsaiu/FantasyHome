using System;
using Newtonsoft.Json;
using RestSharp;

namespace Weather
{
    public  static class HttpRequest
    {
        public static string GET(string url)
        {
            try
            {
                var client = new RestClient(url);
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                IRestResponse response = client.Execute(request);

                return response.Content;

            }
            catch (Exception e)
            {
                return "";
            }

        }
    }
}