using Marrow.XPlat.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace Marrow.XPlat.Desktop
{
    public static class DesktopExtensions
    {
        public static IServiceCollection AddDesktop(this IServiceCollection builder)
        {
            builder.AddSingleton<IFileSystem, DesktopFileSystem>();
            return builder;
        }
    }
}