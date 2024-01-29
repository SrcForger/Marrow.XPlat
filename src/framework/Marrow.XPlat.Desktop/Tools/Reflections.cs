using System;
using System.Reflection;

namespace Marrow.XPlat.Setup
{
    public static class Reflections
    {
        public static Assembly GetExeAssembly()
        {
            var ass = Assembly.GetEntryAssembly();
            return ass ?? throw new InvalidOperationException("No entry found!");
        }

        public static ProductInfo GetProductInfo(Assembly? ass = null)
        {
            ass ??= GetExeAssembly();
            var company = ass?.GetCustomAttribute<AssemblyCompanyAttribute>()?.Company ?? "_";
            var product = ass?.GetCustomAttribute<AssemblyProductAttribute>()?.Product ?? "_";
            return new ProductInfo(company, product);
        }
    }

    public record struct ProductInfo(string Company, string Product);
}