using MMAM = Microsoft.Maui.ApplicationModel;

namespace Marrow.XPlat.ApplicationModel
{
    public sealed class MobileAppInfo : IAppInfo
    {
        private readonly MMAM.IAppInfo _current;

        public MobileAppInfo(MMAM.IAppInfo? current = null)
        {
            _current = current ?? MMAM.AppInfo.Current;
        }

        public string PackageName => _current.PackageName;
        public string Name => _current.Name;
        public string VersionString => _current.VersionString;
        public string BuildString => _current.BuildString;
    }
}