using System.Threading.Tasks;
using Avalonia.Controls;
using Marrow.XPlat.ApplicationModel.DataTransfer;
using Marrow.XPlat.Avalonia.Tools;
using AIP = Avalonia.Input.Platform;

namespace Marrow.XPlat.Avalonia.ApplicationModel.DataTransfer
{
    public sealed class AvalShare : IShare
    {
        private static AIP.IClipboard Clipboard => UiHelper.GetTopLevel()?.Clipboard!;

        public async Task RequestAsync(ShareTextRequest request)
        {
            await ShowModal();

            throw new System.NotImplementedException();
        }

        public Task RequestAsync(ShareFileRequest request)
        {

            throw new System.NotImplementedException();
        }

        public Task RequestAsync(ShareMultipleFilesRequest request)
        {

            throw new System.NotImplementedException();
        }

        private static async Task ShowModal()
        {
            var owner = (Window)UiHelper.GetMainView()!;
            var window = new AvalShareWindow();
            await window.ShowDialog(owner);
        }
    }
}