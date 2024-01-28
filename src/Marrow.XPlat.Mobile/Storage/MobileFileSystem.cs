using MMS = Microsoft.Maui.Storage;

namespace Marrow.XPlat.Storage
{
    public sealed class MobileFileSystem : IFileSystem
    {
        private readonly MMS.IFileSystem _current;

        public MobileFileSystem(MMS.IFileSystem? current = null)
        {
            _current = current ?? MMS.FileSystem.Current;
        }

        public string CacheDirectory => _current.CacheDirectory;

        public string AppDataDirectory => _current.AppDataDirectory;
    }
}