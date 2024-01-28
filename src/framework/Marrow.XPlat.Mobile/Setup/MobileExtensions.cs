using Marrow.XPlat.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace Marrow.XPlat.Mobile
{
    public static class MobileExtensions
    {
        public static IServiceCollection AddMobile(this IServiceCollection builder)
        {
            builder.AddSingleton<IFileSystem, MobileFileSystem>();
            return builder;
        }
    }
}