using System;
using System.Threading.Tasks;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using Marrow.XPlat.Storage;

namespace Sample.ViewModels
{
    public partial class MainViewModel : ViewModelBase
    {
        private readonly IFileSystem _fs;
        private readonly IPreferences _pref;
        private readonly ISecureStorage _secure;

        public MainViewModel(IFileSystem fs, IPreferences pref, ISecureStorage secure)
        {
            _fs = fs;
            _pref = pref;
            _secure = secure;
            Task.Run(DoInit);
        }

        private async Task DoInit()
        {
            await Task.Delay(1500);
            CacheDirectory = _fs.CacheDirectory;
            AppDataDirectory = _fs.AppDataDirectory;
            Preferences = GetPreferences();
            Securities = await GetSecurities();
        }

        [ObservableProperty]
        private string _cacheDirectory;

        [ObservableProperty]
        private string _appDataDirectory;

        [ObservableProperty]
        private string _preferences;

        [ObservableProperty]
        private string _securities;

        [ObservableProperty]
        private IImage? _lastScreenImg;

        [ObservableProperty]
        private string? _clippedText;

        [ObservableProperty] 
        private string? _filePickedName;
        
        [ObservableProperty] 
        private string? _filePickedPath;
        
        public record PrefTest(string FirstName, int Age, bool HasPets, double Price,
            float Share, long Count, DateTime Time, bool HasKey1, bool HasKey2);

        private string GetPreferences()
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

        public record SecTest(string? AuthToken, bool Success, string? End, string? Pass);

        private async Task<string> GetSecurities()
        {
            _secure.RemoveAll();
            await _secure.SetAsync("oauth_token", "secret-oauth-token-value");
            await _secure.SetAsync("good_password", "well-beaver-12345");
            var token1 = await _secure.GetAsync("oauth_token");
            var success = _secure.Remove("oauth_token");
            var token2 = await _secure.GetAsync("oauth_token");
            var token3 = await _secure.GetAsync("good_password");
            var r = new SecTest(token1, success, token2, token3);
            return r.ToString();
        }
    }
}