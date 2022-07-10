using System.Collections.Generic;

namespace FantasyHomeCenter.Application.MqttCenter.Dto.MqttSubscription;



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
    public string topic { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int qos { get; set; }
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
 
public class MqttSubscriptionsOutput
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