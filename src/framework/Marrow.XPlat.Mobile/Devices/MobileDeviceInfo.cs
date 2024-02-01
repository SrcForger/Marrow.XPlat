using MAD = Microsoft.Maui.Devices;

namespace Marrow.XPlat.Devices
{
    public sealed class MobileDeviceInfo : IDeviceInfo
    {
        private readonly MAD.IDeviceInfo _current;

        public MobileDeviceInfo(MAD.IDeviceInfo? current = null)
        {
            _current = current ?? MAD.DeviceInfo.Current;
        }

        public string Model => _current.Model;
        public string Manufacturer => _current.Manufacturer;
        public string Name => _current.Name;
        public string VersionString => _current.VersionString;
    }
}