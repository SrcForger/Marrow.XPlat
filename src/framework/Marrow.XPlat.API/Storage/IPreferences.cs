namespace Marrow.XPlat.Storage
{
	public interface IPreferences
	{
		void Clear();

		void Set<T>(string key, T value);

		T Get<T>(string key, T defaultValue);

		bool ContainsKey(string key);

		void Remove(string key);
	}
}