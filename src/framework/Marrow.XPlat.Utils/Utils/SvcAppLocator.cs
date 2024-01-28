using System;
using Microsoft.Extensions.DependencyInjection;

namespace Marrow.XPlat.Utils
{
    public static class SvcAppLocator
    {
        public static IServiceProvider Bind(this Action<IServiceCollection> apply, IServiceCollection? coll = null)
        {
            var services = coll ?? new ServiceCollection();
            apply(services);
            return services.BuildServiceProvider();
        }

        private static IServiceProvider? _provider;

        public static void Init(IServiceProvider provider)
        {
            _provider = provider;
        }

        public static T Get<T>(Type? type = null) where T : notnull
        {
            var svc = _provider ?? throw new InvalidOperationException("No ioc init!");
            T res;
            if (type == null)
            {
                res = svc.GetRequiredService<T>();
            }
            else
            {
                var raw = svc.GetRequiredService(type);
                res = (T)raw;
            }
            return res;
        }
    }
}