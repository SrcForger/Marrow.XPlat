using System.Threading.Tasks;
using MMS = Microsoft.Maui.Storage;

namespace Marrow.XPlat.Storage
{
    public sealed class MobileSecure : ISecureStorage
    {
        private readonly MMS.ISecureStorage _current;

        public MobileSecure(MMS.ISecureStorage? current = null)
        {
            _current = current ?? MMS.SecureStorage.Default;
        }

        public Task<string?> GetAsync(string key) => _current.GetAsync(key);
        public bool Remove(string key) => _current.Remove(key);
        public void RemoveAll() => _current.RemoveAll();
        public Task SetAsync(string key, string value) => _current.SetAsync(key, value);
    }
}