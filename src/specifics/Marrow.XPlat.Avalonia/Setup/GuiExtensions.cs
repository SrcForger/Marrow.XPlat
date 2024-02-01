using Marrow.XPlat.ApplicationModel.DataTransfer;
using Marrow.XPlat.Avalonia.ApplicationModel.DataTransfer;
using Marrow.XPlat.Avalonia.Media;
using Marrow.XPlat.Media;
using Microsoft.Extensions.DependencyInjection;

namespace Marrow.XPlat.Avalonia.Setup
{
    public static class GuiExtensions
    {
        public static IServiceCollection AddGui(this IServiceCollection builder)
        {
            builder.AddSingleton<IScreenshot, AvalScreenshot>();
            builder.AddSingleton<IClipboard, AvalClipboard>();
            return builder;
        }
    }
}