using AntDesign;
using FantasyHomeCenter.Application.MqttCenter;
using FantasyHomeCenter.Application.MqttCenter.Dto;
using FantasyHomeCenter.Application.MqttCenter.Dto.Topic;
using Microsoft.AspNetCore.Components;

namespace FantasyHomeCenter.Web.Entry.Pages;

public partial class MqttTopic
{

    /// <summary>
    /// 数据源
    /// </summary>
    private MqttTopicListOutput list = new MqttTopicListOutput();

    /// <summary>
    /// mqtt服务
    /// </summary>
    [Inject]
    private IMqttService mqttService { get; set; }
    
    
    /// <summary>
    /// 消息服务
    /// </summary>
    [Inject]
    private MessageService messageService { get; set; }


    protected override void OnInitialized()
    {
        base.OnInitialized();
        var result= this.mqttService.GetTopicList();
        if (result.code == 0)
        {
            this.list = result;
        }
        else
        {
            this.messageService.Error("发生错误！请求mqtt失败!");
            
        }
    }
}