using System.IO;
using Avalonia;

namespace Marrow.XPlat.Avalonia.Tools
{
    public record RenderedImg(MemoryStream Stream, Size Size);
}