using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Marrow.XPlat.ApplicationModel;
using Marrow.XPlat.Utils;

namespace Sample.Views
{
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
        }

        private void RefreshClick(object? sender, RoutedEventArgs e)
        {
        }

        private async void OpenUrlClick(object? sender, RoutedEventArgs e)
        {
            var browser = SvcAppLocator.Get<IBrowser>();

            var uri = new Uri("https://www.microsoft.com");
            await browser.OpenAsync(uri);
        }
    }
}