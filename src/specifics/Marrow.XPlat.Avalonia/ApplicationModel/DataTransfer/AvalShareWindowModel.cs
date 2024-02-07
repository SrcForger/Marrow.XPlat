using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Marrow.XPlat.Avalonia.Tools;
using AIP = Avalonia.Input.Platform;

namespace Marrow.XPlat.Avalonia
{
    public partial class AvalShareWindowModel : ObservableObject
    {
        private static AIP.IClipboard Clipboard => UiHelper.GetTopLevel()?.Clipboard!;

        [ObservableProperty]
        private Action? _closer;

        [ObservableProperty]
        private string? _title;

        [ObservableProperty]
        private string? _text;

        [RelayCommand]
        public async Task CopyClick()
        {
            var text = Text?.Trim() ?? string.Empty;
            await Clipboard.SetTextAsync(text);
            Close();
        }

        private void Close() => Closer?.Invoke();
    }
}