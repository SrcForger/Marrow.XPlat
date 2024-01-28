namespace Marrow.XPlat.Storage
{
    public interface IFileSystem
	{
		string CacheDirectory { get; }

		string AppDataDirectory { get; }
	}
}