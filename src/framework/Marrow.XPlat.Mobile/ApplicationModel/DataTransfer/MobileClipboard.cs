using System.Threading.Tasks;
using MAD = Microsoft.Maui.ApplicationModel.DataTransfer;

namespace Marrow.XPlat.ApplicationModel.DataTransfer
{
    public sealed class MobileClipboard : IClipboard
    {
        private readonly MAD.IClipboard _current;

        public MobileClipboard(MAD.IClipboard? current = null)
        {
            _current = current ?? MAD.Clipboard.Default;
        }

        public Task SetTextAsync(string? text) => _current.SetTextAsync(text);

        public Task<string?> GetTextAsync() => _current.GetTextAsync();
    }
}