using Microsoft.Extensions.DependencyInjection;
using Sample.ViewModels;

namespace Sample.Setup
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