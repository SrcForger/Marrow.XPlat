namespace Marrow.XPlat.Devices
{
    public interface IDeviceInfo
    {
        string Model { get; }

        string Manufacturer { get; }

        string Name { get; }

        string VersionString { get; }
    }
}