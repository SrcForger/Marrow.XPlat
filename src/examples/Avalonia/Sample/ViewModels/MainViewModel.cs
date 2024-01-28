using Marrow.XPlat.Storage;

namespace Sample.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IFileSystem _fs;

        public MainViewModel(IFileSystem fs)
        {
            _fs = fs;
        }

        public string CacheDirectory => _fs.CacheDirectory;
        public string AppDataDirectory => _fs.AppDataDirectory;
    }
}