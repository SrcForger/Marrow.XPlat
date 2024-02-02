using System.IO;
using System.Threading.Tasks;
using MMS = Microsoft.Maui.Storage;

namespace Marrow.XPlat.Storage
{
    internal sealed class FileResult : IFileResult
    {
        private readonly MMS.FileResult _wrap;

        public FileResult(MMS.FileResult wrap)
        {
            _wrap = wrap;
        }

        public string FileName => _wrap.FileName;
        public string FullPath => _wrap.FullPath;
        public Task<Stream> OpenReadAsync() => _wrap.OpenReadAsync();
    }
}