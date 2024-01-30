using Marrow.XPlat.ApplicationModel;
using Marrow.XPlat.Storage;
using Microsoft.Extensions.DependencyInjection;
using Marrow.XPlat.ApplicationModel.Communication;

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
            return builder;
        }
    }
}