﻿using FantasyHomeCenter.Application.MqttCenter.Dto;
using FantasyHomeCenter.Core.Entities;
using Furion;
using Furion.DatabaseAccessor;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FantasyHomeCenter.Web.Core
{
    public class Startup : AppStartup
    {
       
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
            services.AddControllers().AddInjectWithUnifyResult();
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddConfigurableOptions<MqttServiceOptions>();
            services.AddStackExchangeRedisCache(options =>
            {
                
                // 连接字符串，这里也可以读取配置文件
                options.Configuration = App.Configuration["RedisConnection"];
               
               
            });
         
         
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