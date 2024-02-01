using System.IO;
using System.Threading.Tasks;
using MMM = Microsoft.Maui.Media;

namespace Marrow.XPlat.Media
{
    public sealed class MobileScreenshot : IScreenshot
    {
        private readonly MMM.IScreenshot _current;

        public MobileScreenshot(MMM.IScreenshot? current = null)
        {
            _current = current ?? MMM.Screenshot.Default;
        }

        public bool IsCaptureSupported => _current.IsCaptureSupported;

        public async Task<IScreenshotResult> CaptureAsync()
        {
            var result = await _current.CaptureAsync();
            var conv = new ScreenshotResult(result);
            return conv;
        }

        private sealed class ScreenshotResult : IScreenshotResult
        {
            private readonly MMM.IScreenshotResult _current;

            public ScreenshotResult(MMM.IScreenshotResult current)
            {
                _current = current;
            }

            public int Width => _current.Width;
            public int Height => _current.Height;

            public Task CopyToAsync(Stream destination, int quality = 100)
            {
                var fmt = MMM.ScreenshotFormat.Png;
                var res = _current.CopyToAsync(destination, fmt, quality);
                return res;
            }

            public Task<Stream> OpenReadAsync(int quality = 100)
            {
                var fmt = MMM.ScreenshotFormat.Png;
                var res = _current.OpenReadAsync(fmt, quality);
                return res;
            }
        }
    }
}