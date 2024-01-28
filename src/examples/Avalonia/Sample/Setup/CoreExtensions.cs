using Microsoft.Extensions.DependencyInjection;
using Sample.ViewModels;

namespace Marrow.XPlat.Core
{
    public static class CoreExtensions
    {
        public static IServiceCollection AddCore(this IServiceCollection builder)
        {
            builder.AddTransient<MainViewModel>();
            return builder;
        }
    }
}