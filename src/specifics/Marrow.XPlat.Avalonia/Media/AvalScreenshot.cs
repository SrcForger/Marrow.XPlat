using System.IO;
using System.Threading.Tasks;
using Marrow.XPlat.Avalonia.Tools;
using Marrow.XPlat.Media;

namespace Marrow.XPlat.Avalonia.Media
{
    public sealed class AvalScreenshot : IScreenshot
    {
        public bool IsCaptureSupported => true;

        public Task<IScreenshotResult> CaptureAsync()
        {
            var view = UiHelper.GetMainView()!;
            var image = view.RenderToImage();
            IScreenshotResult res = new ScreenshotResult(image);
            return Task.FromResult(res);
        }

        private sealed class ScreenshotResult : IScreenshotResult
        {
            private readonly RenderedImg _image;

            public ScreenshotResult(RenderedImg image)
            {
                _image = image;
            }

            public int Width => (int)_image.Size.Width;
            public int Height => (int)_image.Size.Height;

            public Task CopyToAsync(Stream destination, int quality = 100)
                => _image.Stream.CopyToAsync(destination);

            public Task<Stream> OpenReadAsync(int quality = 100)
            {
                Stream stream = _image.Stream;
                return Task.FromResult(stream);
            }
        }
    }
}