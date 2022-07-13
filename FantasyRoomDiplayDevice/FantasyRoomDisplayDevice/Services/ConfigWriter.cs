using System;
using System.IO;
using System.Windows;

namespace FantasyRoomDisplayDevice.Services
{
    public class ConfigWriter:IConfigWriter
    {
        public bool Write(string content)
        {

            try
            {
                string file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");
                File.WriteAllText(file,content);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
         

        }
    }
}