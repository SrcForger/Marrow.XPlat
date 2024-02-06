using System.Linq;
using System.Threading.Tasks;
using MAD = Microsoft.Maui.ApplicationModel.DataTransfer;

namespace Marrow.XPlat.ApplicationModel.DataTransfer
{
    public sealed class MobileShare : IShare
    {
        private readonly MAD.IShare _current;

        public MobileShare(MAD.IShare? current = null)
        {
            _current = current ?? MAD.Share.Default;
        }

        public Task RequestAsync(ShareTextRequest request)
        {
            var req = new MAD.ShareTextRequest
            {
                Text = request.Text,
                Title = request.Title,
                Uri = request.Uri
            };
            return _current.RequestAsync(req);
        }

        public Task RequestAsync(ShareFileRequest request)
        {
            var req = new MAD.ShareFileRequest
            {
                Title = request.Title,
                File = Convert(request.File)
            };
            return _current.RequestAsync(req);
        }

        public Task RequestAsync(ShareMultipleFilesRequest request)
        {
            var req = new MAD.ShareMultipleFilesRequest
            {
                Title = request.Title,
                Files = request.Files?.Select(sf => Convert(sf)!).ToList()
            };
            return _current.RequestAsync(req);
        }

        private static MAD.ShareFile? Convert(ShareFile? sf)
        {
            if (sf == null) return null;
            var fullPath = sf.Path;
            return new MAD.ShareFile(fullPath);
        }
    }
}