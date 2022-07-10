using System.Collections.Generic;

namespace FantasyHomeCenter.Application.MqttCenter.Dto.Client;


//如果好用，请收藏地址，帮忙分享。
public class ObjectsItem
{
    /// <summary>
    /// 
    /// </summary>
    public string client_id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string username { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string ipaddress { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int port { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string clean_sess { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int proto_ver { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int keepalive { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string connected_at { get; set; }
}
 
public class Result
{
    /// <summary>
    /// 
    /// </summary>
    public int current_page { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int page_size { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int total_num { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int total_page { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List <ObjectsItem > objects { get; set; }
}
 
public class MqttClientListOutput
{
    /// <summary>
    /// 
    /// </summary>
    public int code { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public Result result { get; set; }
}