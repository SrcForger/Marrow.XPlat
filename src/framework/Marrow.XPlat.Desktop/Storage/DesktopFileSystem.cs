using System;
using Marrow.XPlat.Tools;

namespace Marrow.XPlat.Storage
{
    public sealed class DesktopFileSystem : IFileSystem
    {
        private readonly Lazy<(string cache, string data)> _data;

        public DesktopFileSystem()
        {
            _data = new Lazy<(string cache, string data)>(Load);
        }

        private static (string cache, string data) Load()
        {
            var info = Reflections.GetProductInfo();
            if (OperatingSystem.IsWindows() &&
                SystemEnv.TryGetEnvVariable("APPDATA", out var roamAppData) &&
                SystemEnv.TryGetEnvVariable("LOCALAPPDATA", out var localAppData))
            {
                var cDir = SystemEnv.CreateGetDir(localAppData, info.Company, info.Product, "tmp");
                var fDir = SystemEnv.CreateGetDir(roamAppData, info.Company, info.Product, "dat");
                return (cDir, fDir);
            }
            if (OperatingSystem.IsLinux() &&
                SystemEnv.TryGetEnvVariable("HOME", out var home))
            {
                var cf = SystemEnv.CreateGetDir(home, ".cache", info.Company, info.Product, "tmp");
                var ff = SystemEnv.CreateGetDir(home, ".config", info.Company, info.Product, "dat");
                return (cf, ff);
            }
            if (OperatingSystem.IsMacOS() &&
                SystemEnv.TryGetEnvVariable("HOME", out home))
            {
                var lib = SystemEnv.CreateGetDir(home, "Library");
                var c = SystemEnv.CreateGetDir(lib, "Caches", info.Company, info.Product, "tmp");
                var f = SystemEnv.CreateGetDir(lib, "Application Support", info.Company, info.Product, "dat");
                return (c, f);
            }
            throw new InvalidOperationException("No support for your OS!");
        }

        public string CacheDirectory => _data.Value.cache;
        public string AppDataDirectory => _data.Value.data;
    }
}