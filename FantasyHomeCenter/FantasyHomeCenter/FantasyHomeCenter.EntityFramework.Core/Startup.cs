using Furion;
using Furion.DatabaseAccessor;
using Microsoft.Extensions.DependencyInjection;

namespace FantasyHomeCenter.EntityFramework.Core
{
    public class Startup : AppStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDatabaseAccessor(options =>
            {
                options.AddDbPool<DefaultDbContext>(DbProvider.SqlServer);
            }, "FantasyHomeCenter.Database.Migrations");
        }
    }
}