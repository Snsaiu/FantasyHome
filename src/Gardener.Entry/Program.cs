// -----------------------------------------------------------------------------
// ԰��,�Ǹ��ܼ򵥵Ĺ���ϵͳ
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Gardener.Entry
{
    /// <summary>
    /// �������
    /// </summary>
    public class Program
    {
        /// <summary>
        /// ��ڷ���
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                    //ע��Furion���
                    .Inject()
                    .UseStartup<Startup>();
                });//.UseSerilogDefault();
    }
}
