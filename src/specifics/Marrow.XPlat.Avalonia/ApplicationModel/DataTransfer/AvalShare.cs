using System.Threading.Tasks;
using Marrow.XPlat.ApplicationModel.DataTransfer;
using Marrow.XPlat.Avalonia.Tools;
using AIP = Avalonia.Input.Platform;

namespace Marrow.XPlat.Avalonia.ApplicationModel.DataTransfer
{
    public sealed class AvalShare : IShare
    {
        private static AIP.IClipboard Clipboard => UiHelper.GetTopLevel()?.Clipboard!;

        public Task RequestAsync(ShareTextRequest request)
        {


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
    }
}