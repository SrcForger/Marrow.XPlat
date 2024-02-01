using System.Threading.Tasks;

namespace Marrow.XPlat.Media
{
    public interface IScreenshot
    {
        bool IsCaptureSupported { get; }

        Task<IScreenshotResult> CaptureAsync();
    }
}