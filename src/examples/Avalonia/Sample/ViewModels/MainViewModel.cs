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
}