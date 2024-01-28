using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform.Storage;

namespace Sample.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public string Greeting => string.Join(" \r\n ",
        [
            GetFolder(WellKnownFolder.Desktop),
            GetFolder(WellKnownFolder.Documents),
            GetFolder(WellKnownFolder.Downloads),
            GetFolder(WellKnownFolder.Music),
            GetFolder(WellKnownFolder.Pictures),
            GetFolder(WellKnownFolder.Videos)
        ]);

        private Control? GetMainWindow()
        {
            var app = Application.Current;
            var life = app?.ApplicationLifetime;
            var main = (life as ISingleViewApplicationLifetime)?.MainView ??
                (life as IClassicDesktopStyleApplicationLifetime)?.MainWindow;
            return main;
        }

        private IStorageProvider? GetStorageProvider()
        {
            var main = GetMainWindow();
            var top = TopLevel.GetTopLevel(main);
            var provider = top?.StorageProvider;
            return provider;
        }

        private string? GetFolder(WellKnownFolder folder)
        {
            var sp = GetStorageProvider();
            var task = sp?.TryGetWellKnownFolderAsync(folder);
            var res = task?.GetAwaiter().GetResult();
            return res?.Path.ToString();
        }
    }
}