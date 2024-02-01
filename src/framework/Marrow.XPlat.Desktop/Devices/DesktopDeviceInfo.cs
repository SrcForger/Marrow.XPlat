using System;
using Marrow.XPlat.Tools;
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
                throw new NotImplementedException();
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