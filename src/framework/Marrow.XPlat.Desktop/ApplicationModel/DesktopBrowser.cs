using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Marrow.XPlat.ApplicationModel
{
    public sealed class DesktopBrowser : IBrowser
    {
        public Task<bool> OpenAsync(Uri uri)
        {
            var url = uri.ToString();
            _ = OpenUrl(url);
            return Task.FromResult(true);
        }

        private static Process OpenUrl(string url)
        {
            var processInfo = new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            };
            var proc = Process.Start(processInfo);
            return proc!;
        }
    }
}