using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Marrow.XPlat.ApplicationModel;
using Marrow.XPlat.ApplicationModel.Communication;
using Marrow.XPlat.ApplicationModel.DataTransfer;
using Marrow.XPlat.Media;
using Marrow.XPlat.Storage;
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

        private async void SetClipTxtClick(object? sender, RoutedEventArgs e)
        {
            var clip = SvcAppLocator.Get<IClipboard>();

            await clip.SetTextAsync($"This text was created at {DateTime.Now:u}");
        }
        
        private async void ReadClipTxtClick(object? sender, RoutedEventArgs e)
        {
            var clip = SvcAppLocator.Get<IClipboard>();

            var text = await clip.GetTextAsync();
            if (text != null)
            {
                Model.ClippedText = text;
                await ClearClipboard();
            }
            else
                Model.ClippedText = "Clipboard is empty";
        }

        private static async Task ClearClipboard()
        {
            var clip = SvcAppLocator.Get<IClipboard>();

            await clip.SetTextAsync(null);
        }

        private async void PickFileClick(object? sender, RoutedEventArgs e)
        {
            var filePicker = SvcAppLocator.Get<IFilePicker>();

            var options = new PickOptions { PickerTitle = "Please select a JPG/PNG image file!" };
            if (await PickAndShow(() => filePicker.PickAsync(options)) is { } result)
            {
                Model.FilePickedName = result.FileName;
                Model.FilePickedPath = result.FullPath;
            }
        }

        private async void PickMediaClick(object? sender, RoutedEventArgs e)
        {
            var mediaPicker = SvcAppLocator.Get<IMediaPicker>();

            var options = new MediaPickerOptions { Title = "Select some image file or what?" };
            if (await PickAndShow(() => mediaPicker.PickPhotoAsync(options)) is { } result)
            {
                Model.FilePickedName = result.FileName;
                Model.FilePickedPath = result.FullPath;
            }
        }

        private async Task<IFileResult?> PickAndShow(Func<Task<IFileResult?>> func)
        {
            try
            {
                var result = await func();
                if (result != null)
                {
                    if (result.FileName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase) ||
                        result.FileName.EndsWith("png", StringComparison.OrdinalIgnoreCase))
                    {
                        await using var stream = await result.OpenReadAsync();
                        Model.LastScreenImg = new Bitmap(stream);
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return null;
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