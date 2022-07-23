using FantasyHomeCenter.Application.MqttCenter.Dto;

namespace FantasyHomeCenter.Application.MqttCenter.MessageProcess;

public abstract class MessageParserBase
{
    private readonly string flag;
    private readonly MqttSendInfo data;

    protected abstract string GetParserFlag();

    public MessageParserBase Next { get; set; }

    public MessageParserBase(string flag, MqttSendInfo data)
    {
        this.flag = flag;
        this.data = data;
    }

    protected abstract void Parse(MqttSendInfo data);

    public void Process()
    {
        if (this.flag != this.GetParserFlag())
        {
            if (this.Next == null)
            {
                return;
            }

           // throw new System.NullReferenceException("没有对应的处理器处理");
            this.Next.Process();
        }
        else
        {
            this.Parse(this.data);
        }



    }
}