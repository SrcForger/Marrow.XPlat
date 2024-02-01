using Marrow.XPlat.Tools;

namespace Marrow.XPlat.ApplicationModel
{
    public sealed class DesktopAppInfo : IAppInfo
    {
        private readonly ProductInfo _info;

        public DesktopAppInfo()
        {
            _info = Reflections.GetProductInfo();
        }

        public string Name => _info.Product;
        public string VersionString => _info.Version;
        public string PackageName => _info.Title;
        public string BuildString => _info.Build[..7];
    }
}