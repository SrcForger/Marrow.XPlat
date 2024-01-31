using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Marrow.XPlat.ApplicationModel;
using Marrow.XPlat.ApplicationModel.Communication;
using Marrow.XPlat.Media;
using Marrow.XPlat.Utils;
using Sample.ViewModels;

namespace Sample.Views
{
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
        }

        private MainViewModel Model => (MainViewModel)DataContext!;

        private void RefreshClick(object? sender, RoutedEventArgs e)
        {
        }

        private async void DoScreenClick(object? sender, RoutedEventArgs e)
        {
            var shot = SvcAppLocator.Get<IScreenshot>();

            if (shot.IsCaptureSupported)
            {
                var screen = await shot.CaptureAsync();
                var stream = await screen.OpenReadAsync();
                var bitmap = new Bitmap(stream);
                Model.LastScreenImg = bitmap;
            }
        }

        private async void OpenUrlClick(object? sender, RoutedEventArgs e)
        {
            var browser = SvcAppLocator.Get<IBrowser>();

            var uri = new Uri("https://www.microsoft.com");
            await browser.OpenAsync(uri);
        }

        private async void SendMailClick(object? sender, RoutedEventArgs e)
        {
            var email = SvcAppLocator.Get<IEmail>();

            if (email.IsComposeSupported)
            {
                var subject = "Hello friends!";
                var body = "It was great to see you last weekend.";
                var recipients = new[] { "john@contoso.com", "jane@contoso.com" };
                var message = new EmailMessage
                {
                    Subject = subject,
                    Body = body,
                    To = new List<string>(recipients),
                    Bcc = new List<string> { "nsa@gov.com" },
                    Cc = new List<string> { "joe.biden@democrats.com.uk", "pizza@rat.nyc.org" }
                };
                await email.ComposeAsync(message);
            }
        }
    }
}