using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using Avalonia;
using System.Threading.Tasks;
using System;

namespace Marrow.XPlat.Avalonia
{
    public static class UiHelper
    {
        public static Control? GetMainView()
        {
            var app = Application.Current;
            var life = app?.ApplicationLifetime;
            var main = (life as ISingleViewApplicationLifetime)?.MainView ??
                (life as IClassicDesktopStyleApplicationLifetime)?.MainWindow;
            return main;
        }

        public static IStorageProvider? GetStorageProvider()
        {
            var main = GetMainView();
            var top = TopLevel.GetTopLevel(main);
            var provider = top?.StorageProvider;
            return provider;
        }

        public static async Task<IStorageFolder> GetFolder(WellKnownFolder folder)
        {
            var sp = GetStorageProvider() ?? throw new InvalidOperationException("No provider found!");
            var res = await sp.TryGetWellKnownFolderAsync(folder);
            return res ?? throw new InvalidOperationException("No folder found!");
        }
    }
}