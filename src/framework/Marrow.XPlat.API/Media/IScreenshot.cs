using System.IO;
using System.Threading.Tasks;

namespace Marrow.XPlat.Media
{
    public interface IScreenshot
    {
        bool IsCaptureSupported { get; }

        Task<IScreenshotResult> CaptureAsync();
    }

    public interface IScreenshotResult
    {
        int Width { get; }

        int Height { get; }

        Task<Stream> OpenReadAsync(int quality = 100);

        Task CopyToAsync(Stream destination, int quality = 100);
    }
}