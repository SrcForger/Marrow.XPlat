using Marrow.XPlat.ApplicationModel;
using Marrow.XPlat.Storage;
using Microsoft.Extensions.DependencyInjection;
using Marrow.XPlat.ApplicationModel.Communication;
using Marrow.XPlat.Devices;

namespace Marrow.XPlat.Setup
{
    public static class DesktopExtensions
    {
        public static IServiceCollection AddDesktop(this IServiceCollection builder)
        {
            builder.AddSingleton<IFileSystem, DesktopFileSystem>();
            builder.AddSingleton<IPreferences, DesktopPreferences>();
            builder.AddSingleton<IBrowser, DesktopBrowser>();
            builder.AddSingleton<IEmail, DesktopEmail>();
            builder.AddSingleton<ISecureStorage, DesktopSecure>();
            builder.AddSingleton<IAppInfo, DesktopAppInfo>();
            builder.AddSingleton<IDeviceInfo, DesktopDeviceInfo>();
            return builder;
        }
    }
}