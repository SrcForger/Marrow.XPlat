using System;
using Marrow.XPlat.Tools;
using OSVersionExtension;
using OperatingSystem = System.OperatingSystem;
using DI = (string m, string n, string v, string c);

namespace Marrow.XPlat.Devices
{
    public sealed class DesktopDeviceInfo : IDeviceInfo
    {
        private readonly Lazy<DI> _data;

        public DesktopDeviceInfo()
        {
            _data = new Lazy<DI>(Load);
        }

        private static DI Load()
        {
            if (OperatingSystem.IsWindows())
            {
                var wVendor = "Microsoft";
                var wName = "Windows";
                var osStr = OSVersion.GetOperatingSystem().ToString();
                var winVerId = Strings.Space(osStr).Replace(wName, string.Empty).Trim();
                var winCodeName = OSVersion.MajorVersion10Properties().DisplayVersion;
                return (wName, wVendor, winVerId, winCodeName);
            }
            if (OperatingSystem.IsLinux() &&
                SystemEnv.TryGetEnvFile("/etc/os-release", out var osRel) &&
                osRel.TryGetValue("name", out var distroName) &&
                osRel.TryGetValue("version_id", out var distroVerId) &&
                osRel.TryGetValue("version_codename", out var distroCodeName))
            {
                return ("Linux", distroName, distroVerId, distroCodeName);
            }
            if (OperatingSystem.IsMacOS())
            {
                throw new NotImplementedException();
            }
            throw new InvalidOperationException("No support for your OS!");
        }

        public string Model => _data.Value.c;
        public string Manufacturer => _data.Value.n;
        public string Name => _data.Value.m;
        public string VersionString => _data.Value.v;
    }
}