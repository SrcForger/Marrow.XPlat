using System;
using Marrow.XPlat.Storage;

namespace Sample.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IFileSystem _fs;
        private readonly IPreferences _pref;

        public MainViewModel(IFileSystem fs, IPreferences pref)
        {
            _fs = fs;
            _pref = pref;
        }

        public string CacheDirectory => _fs.CacheDirectory;
        public string AppDataDirectory => _fs.AppDataDirectory;

        public record PrefTest(string FirstName, int Age, bool HasPets, double Price,
            float Share, long Count, DateTime Time, bool HasKey1, bool HasKey2);

        public string Preferences
        {
            get
            {
                _pref.Clear();
                _pref.Set("first_name", "John");
                _pref.Set("age", 28);
                _pref.Set("has_pets", true);
                _pref.Set("price", 139.35d);
                _pref.Set("share", 25.12f);
                _pref.Set("count", 30934984L);
                _pref.Set("time", DateTime.Now);
                var firstName = _pref.Get("first_name", "Unknown");
                var age = _pref.Get("age", -1);
                var hasPets = _pref.Get("has_pets", false);
                var price = _pref.Get("price", default(double));
                var share = _pref.Get("share", default(float));
                var count = _pref.Get("count", default(long));
                var time = _pref.Get("time", default(DateTime));
                var hasKey1 = _pref.ContainsKey("price");
                _pref.Remove("price");
                var hasKey2 = _pref.ContainsKey("price");
                var r = new PrefTest(firstName, age, hasPets, price, share, count, time, hasKey1, hasKey2);
                return r.ToString();
            }
        }
    }
}