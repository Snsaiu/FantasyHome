using FantasyResultModel;
using FantasyResultModel.Impls;
using Newtonsoft.Json;
using RestSharp;

namespace FantasyHome.UI.Utils;

public  static class HttpRequest<T>
{
    public static ResultBase<T> GET(string url)
    {
        try
        {
            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);

            var data= JsonConvert.DeserializeObject<T>(response.Content);
            return new SuccessResultModel<T>(data);
        }
        catch (Exception e)
        {
            return new ErrorResultModel<T>(e.Message);
        }

    }
}