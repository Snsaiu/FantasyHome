using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FantasyHomeCenter.Application.MqttCenter.Dto;

public class MqttTopicListOutput
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

public class ObjectsItem
{
    /// <summary>
    /// 
    /// </summary>
    [Display(Name = "主题")]
    public string topic { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [Display(Name = "节点")]
    public string node { get; set; }
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
