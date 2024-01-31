using System.IO;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media.Imaging;

namespace Marrow.XPlat.Avalonia.Tools
{
    public record RenderedImg(MemoryStream Stream, Size Size);

    public static class PixelHelper
    {
        public static RenderedImg RenderToImage(this Control control, int dpi = 96)
        {
            var pxSize = new PixelSize((int)control.Width, (int)control.Height);
            var size = new Size(control.Width, control.Height);
            var dpiV = new Vector(dpi, dpi);
            using var bitmap = new RenderTargetBitmap(pxSize, dpiV);
            control.Measure(size);
            control.Arrange(new Rect(size));
            bitmap.Render(control);
            var mem = new MemoryStream();
            bitmap.Save(mem);
            mem.Position = 0L;
            return new RenderedImg(mem, bitmap.Size);
        }
    }
}