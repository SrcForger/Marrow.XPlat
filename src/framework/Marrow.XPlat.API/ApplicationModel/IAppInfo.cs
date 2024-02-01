namespace Marrow.XPlat.ApplicationModel
{
    public interface IAppInfo
    {
        string PackageName { get; }

        string Name { get; }

        string VersionString { get; }

        string BuildString { get; }
    }
}