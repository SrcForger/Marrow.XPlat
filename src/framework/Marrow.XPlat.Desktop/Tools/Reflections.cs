using System;
using System.Linq;
using System.Reflection;

namespace Marrow.XPlat.Tools
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
            var company = ass.GetCustomAttribute<AssemblyCompanyAttribute>()?.Company ?? "_";
            var product = ass.GetCustomAttribute<AssemblyProductAttribute>()?.Product ?? "_";
            var version = ass.GetCustomAttribute<AssemblyFileVersionAttribute>()?.Version ?? "_";
            var title = ass.GetCustomAttribute<AssemblyTitleAttribute>()?.Title ?? "_";
            var i = ass.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ?? "_";
            i = i.Split('+', 2).Last();
            return new ProductInfo(company, product, version, title, i);
        }
    }
}