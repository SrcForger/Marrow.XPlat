using System;
using Avalonia;
using Avalonia.ReactiveUI;
using Marrow.XPlat.Setup;
using Marrow.XPlat.Utils;
using Marrow.XPlat.Avalonia.Setup;
using Sample.Setup;

namespace Sample.Desktop
{
    internal sealed class Program
    {
        [STAThread]
        public static void Main(string[] args) => BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);

        public static AppBuilder BuildAvaloniaApp()
        {
            SvcAppLocator.Bind(svc => svc.AddCore().AddDesktop().AddGui()).Init();

            return AppBuilder.Configure<App>()
                        .UsePlatformDetect()
                        .WithInterFont()
                        .LogToTrace()
                        .UseReactiveUI();
        }
    }
}