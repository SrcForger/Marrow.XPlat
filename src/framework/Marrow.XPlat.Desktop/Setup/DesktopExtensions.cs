using Marrow.XPlat.ApplicationModel;
using Marrow.XPlat.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace Marrow.XPlat.Setup
{
    public static class DesktopExtensions
    {
        public static IServiceCollection AddDesktop(this IServiceCollection builder)
        {
            builder.AddSingleton<IFileSystem, DesktopFileSystem>();
            builder.AddSingleton<IPreferences, DesktopPreferences>();
            builder.AddSingleton<IBrowser, DesktopBrowser>();
            return builder;
        }
    }
}