using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using Marrow.XPlat.ApplicationModel.DataTransfer;
using Marrow.XPlat.Avalonia.Tools;

namespace Marrow.XPlat.Avalonia.ApplicationModel.DataTransfer
{
    public sealed class AvalShare : IShare
    {
        public async Task RequestAsync(ShareTextRequest request)
        {
            var plain = (request.Text + Environment.NewLine + request.Uri).Trim();
            var model = new AvalShareWindowModel
            {
                Title = request.Title, Text = plain
            };
            await ShowModal(model);
        }

        public async Task RequestAsync(ShareFileRequest request)
        {
            var model = new AvalShareWindowModel
            {
                Title = request.Title
            };
            await ShowModal(model);
        }

        public async Task RequestAsync(ShareMultipleFilesRequest request)
        {
            var model = new AvalShareWindowModel
            {
                Title = request.Title
            };
            await ShowModal(model);
        }

        private static async Task ShowModal(AvalShareWindowModel model)
        {
            var owner = (Window)UiHelper.GetMainView()!;
            var window = new AvalShareWindow
            {
                DataContext = model
            };
            model.Closer = window.Close;
            await window.ShowDialog(owner);
        }
    }
}