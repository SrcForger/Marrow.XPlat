using MMS = Microsoft.Maui.Storage;

namespace Marrow.XPlat.Storage
{
    public sealed class MobilePreferences : IPreferences
    {
        private readonly MMS.IPreferences _current;

        public MobilePreferences(MMS.IPreferences? current = null)
        {
            _current = current ?? MMS.Preferences.Default;
        }

        public void Clear() => _current.Clear();
        public bool ContainsKey(string key) => _current.ContainsKey(key);
        public T Get<T>(string key, T defaultValue) => _current.Get(key, defaultValue);
        public void Remove(string key) => _current.Remove(key);
        public void Set<T>(string key, T value) => _current.Set(key, value);
    }
}