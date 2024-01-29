using System;
using Marrow.XPlat.Setup;

namespace Marrow.XPlat.Storage
{
    public sealed class DesktopFileSystem : IFileSystem
    {
        private readonly Lazy<(string cache, string data)> _data;

        public DesktopFileSystem()
        {
            _data = new Lazy<(string cache, string data)>(Load);
        }

        private (string cache, string data) Load()
        {
            var info = Reflections.GetProductInfo();
            if (OperatingSystem.IsWindows() &&
                SystemEnv.TryGetEnvVariable("APPDATA", out var roamAppData) &&
                SystemEnv.TryGetEnvVariable("LOCALAPPDATA", out var localAppData))
            {
                var cacheDir = SystemEnv.CreateGetDir(localAppData, info.Company, info.Product, "Cache");
                var filesDir = SystemEnv.CreateGetDir(roamAppData, info.Company, info.Product, "Files");
                return (cacheDir, filesDir);
            }
            throw new InvalidOperationException("No support for your OS!");
        }

        public string CacheDirectory => _data.Value.cache;
        public string AppDataDirectory => _data.Value.data;
    }
}