using System;
using System.Linq;

using FantasyHomeCenter.Application.DeviceCenter;
using FantasyHomeCenter.Application.MqttCenter;
using FantasyHomeCenter.Application.MqttCenter.Dto;
using FantasyHomeCenter.Application.MqttCenter.Dto.Service;
using FantasyHomeCenter.Core.Entities;
using Furion;
using Furion.DatabaseAccessor;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FantasyHomeCenter.Web.Core
{
    public class Startup : AppStartup
    {
     private   string CustomSchemaIdSelector(Type modelType)
        {
            if (!modelType.IsConstructedGenericType) return modelType.FullName.Replace("[]", "Array");

            var prefix = modelType.GetGenericArguments()
                .Select(genericArg => CustomSchemaIdSelector(genericArg))
                .Aggregate((previous, current) => previous + current);

            return prefix + modelType.FullName.Split('`').First();
        }
     
        public void ConfigureServices(IServiceCollection services)
        {
            // JWT 和 Cookies 混合身份验证
            services.AddJwt(options =>
                {
                    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    options.LoginPath = "/Login";
                });
            services.AddControllers().AddInjectWithUnifyResult(options =>
            {
                options.SpecificationDocumentConfigure = spt =>
                {
                    spt.SwaggerGenConfigure = gen =>
                    {
                        gen.CustomSchemaIds(CustomSchemaIdSelector);
                    };
                };
            });
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddConfigurableOptions<MqttServiceOptions>();
            // services.AddStackExchangeRedisCache(options =>
            // {
            //     
            //     // 连接字符串，这里也可以读取配置文件
            //     options.Configuration = App.Configuration["RedisConnection"];
            //    
            //    
            // });
           
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton(new MqttServerInstance());

           // services.AddInitPluginService();
            services.AddMqttServiceAsync();
           // services.AddSendRoomToControlDevice();
          //  services.AddBackgroundTaskService();


        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseInject();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}