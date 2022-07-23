using System.Collections.Generic;
using FantasyRoomDisplayDevice.Models;

namespace FantasyRoomDisplayDevice.MqttMessageParse
{
    public abstract class MqttMessageParseBase
    {
        private readonly string flag;
        private readonly MqttSendInfo data;

        protected abstract string GetParserFlag();

        public MqttMessageParseBase Next { get; set; }

        public MqttMessageParseBase(string flag, MqttSendInfo data)
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
                    throw new System.NullReferenceException("没有对应的处理器处理");
                this.Next.Process();
            }
            else
            {
                this.Parse(this.data);
            }

         

        }

    }
}