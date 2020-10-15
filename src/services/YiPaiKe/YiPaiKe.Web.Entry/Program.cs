// -----------------------------------------------------------------------------
// �ļ�ͷ
// -----------------------------------------------------------------------------

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace YiPaiKe.Web.Entry
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                    //ע��Fur���
                    .Inject()
                    .UseStartup<Startup>();
                });
    }
}
