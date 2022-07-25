using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Markup;

using FantasyRoomDisplayDevice.Models;

namespace FantasyRoomDisplayDevice.MqttMessageParse
{
    public abstract class MqttMessageParseBase
    {
        private readonly string flag;
        private readonly MqttSendInfo data;

        protected abstract string GetParserFlag();

        public MqttMessageParseBase Next { get; set; }

        public bool DeviceUiUpdate { get; set; } = false;

        public MqttMessageParseBase(string flag, MqttSendInfo data)
        {
            this.flag = flag;
            this.data = data;
        }

        protected abstract void Parse(MqttSendInfo data);

        protected virtual List<string> GetParseFlags()
        {
            return new List<string>();
        }

        public void Process()
        {
            if (this.DeviceUiUpdate == false)
            {
                if (this.flag != this.GetParserFlag())
                {
                    if (this.Next == null)
                        return;
                    this.Next.Process();
                }
                else
                {
                    this.Parse(this.data);
                }
            }
            else
            {
               var list=  this.GetParseFlags();
               if (list.Count == 0)
               {
                    if (this.Next == null) return;
                    else
                    {
                        this.Next.Process();
                    }
               }

                if (list.Any(x => x == this.flag))
                    this.Parse(this.data);
                else if (this.Next == null) return;
                else this.Next.Process();

            }



        }

    }
}