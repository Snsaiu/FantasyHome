using CommunityToolkit.Maui;
using FantasyHome.UI.Utils;
using Microsoft.Maui.LifecycleEvents;


#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.Graphics;
#endif

namespace FantasyHome.UI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("typicons.ttf", "typicons");
                    fonts.AddFont("qweather-icons.ttf", "weather");
                });

#if WINDOWS
            builder.ConfigureLifecycleEvents(events =>
            {
                events.AddWindows(wndLifeCycleBuilder =>
                {
                    wndLifeCycleBuilder.OnWindowCreated(window =>
                    {
                        IntPtr nativeWindowHandle = WinRT.Interop.WindowNative.GetWindowHandle(window);
                        WindowId win32WindowsId = Win32Interop.GetWindowIdFromWindow(nativeWindowHandle);
                        AppWindow winuiAppWindow = AppWindow.GetFromWindowId(win32WindowsId);

                        const int width = 500;
                        const int height = 900;
                        //winuiAppWindow.MoveAndResize(new RectInt32(1920 / 2 - width / 2, 1080 / 2 - height / 2, width, height));
                        winuiAppWindow.MoveAndResize(new RectInt32(0, 0, width, height));
                    });
                });
            });

#endif
            
            builder.Services.AddTransient<Views.MainPage>();
            builder.Services.AddTransient<ViewModels.MainPageModel>();
           

            return builder.Build();
        }
    }
}