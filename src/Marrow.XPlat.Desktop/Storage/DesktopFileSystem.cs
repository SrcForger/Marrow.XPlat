using System;

namespace Marrow.XPlat.Storage
{
    public sealed class DesktopFileSystem : IFileSystem
    {
        public string CacheDirectory => throw new NotImplementedException();

        public string AppDataDirectory => throw new NotImplementedException();
    }
}