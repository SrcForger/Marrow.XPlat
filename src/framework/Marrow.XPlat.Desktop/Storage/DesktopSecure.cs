using System;
using System.Text;
using System.Threading.Tasks;

namespace Marrow.XPlat.Storage
{
    public sealed class DesktopSecure : DesktopPreferences, ISecureStorage
    {
        public DesktopSecure(IFileSystem fs) : base(fs, name: "secured")
        {
        }

        private static string? Decrypt(string? plain)
        {
            if (plain == null) return null;
            var bytes = Convert.FromBase64String(plain);
            var res = Encoding.UTF8.GetString(bytes);
            return res;
        }

        private static string? Encrypt(string? plain)
        {
            if (plain == null) return null;
            var bytes = Encoding.UTF8.GetBytes(plain);
            var res = Convert.ToBase64String(bytes);
            return res;
        }

        public Task<string?> GetAsync(string plainKey)
        {
            var key = Encrypt(plainKey)!;
            var val = Get<string?>(key, null);
            var res = Decrypt(val);
            return Task.FromResult(res);
        }

        bool ISecureStorage.Remove(string plainKey)
        {
            var key = Encrypt(plainKey)!;
            if (!ContainsKey(key))
                return false;
            Remove(key);
            return true;
        }

        public void RemoveAll()
        {
            Clear();
        }

        public Task SetAsync(string plainKey, string plainValue)
        {
            var key = Encrypt(plainKey)!;
            var value = Encrypt(plainValue);
            Set(key, value);
            return Task.CompletedTask;
        }
    }
}