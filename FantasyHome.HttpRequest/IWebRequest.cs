namespace FantasyHome.HttpRequest
{
    public interface IWebRequest
    {

        Task<string> PostAsync(string url, object? Body, Dictionary<string, string>? Heaeders, Dictionary<string,string>?param);

    }
}