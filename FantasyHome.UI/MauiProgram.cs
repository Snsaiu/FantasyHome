﻿namespace FantasyHome.UI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("typicons.ttf", "typicons");
                });
            builder.Services.AddTransient<Views.MainPage>();
            builder.Services.AddTransient<ViewModels.MainPageModel>();

            return builder.Build();
        }
    }
}