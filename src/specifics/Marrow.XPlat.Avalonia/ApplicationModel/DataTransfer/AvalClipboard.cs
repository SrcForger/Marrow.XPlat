using System.Threading.Tasks;
using Marrow.XPlat.ApplicationModel.DataTransfer;
using Marrow.XPlat.Avalonia.Tools;
using AIP = Avalonia.Input.Platform;

namespace Marrow.XPlat.Avalonia.ApplicationModel.DataTransfer
{
    public sealed class AvalClipboard : IClipboard
    {
        private static AIP.IClipboard Clipboard => UiHelper.GetTopLevel()?.Clipboard!;

        public Task SetTextAsync(string? text)
        {
            return text == null ? Clipboard.ClearAsync() : Clipboard.SetTextAsync(text);
        }

        public Task<string?> GetTextAsync()
        {
            return Clipboard.GetTextAsync();
        }
    }
}