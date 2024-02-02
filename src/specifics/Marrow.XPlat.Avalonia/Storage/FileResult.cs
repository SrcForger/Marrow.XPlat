using System.IO;
using System.Threading.Tasks;
using Marrow.XPlat.Storage;
using APS = Avalonia.Platform.Storage;

namespace Marrow.XPlat.Avalonia.Storage
{
    internal sealed class FileResult : IFileResult
    {
        private readonly APS.IStorageFile _wrap;

        public FileResult(APS.IStorageFile wrap)
        {
            _wrap = wrap;
        }

        public Task<Stream> OpenReadAsync() => _wrap.OpenReadAsync();
        public string FileName => _wrap.Name;

        public string FullPath
        {
            get
            {
                if (_wrap.Path is { IsAbsoluteUri: true, Scheme: "file" } absolute)
                    return absolute.LocalPath;
                return _wrap.Path.ToString();
            }
        }
    }
}