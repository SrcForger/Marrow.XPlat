using System;
using System.Threading.Tasks;
using MAM = Microsoft.Maui.ApplicationModel;

namespace Marrow.XPlat.ApplicationModel
{
    public sealed class MobileBrowser : IBrowser
    {
        private readonly MAM.IBrowser _current;

        public MobileBrowser(MAM.IBrowser? current = null)
        {
            _current = current ?? MAM.Browser.Default;
        }

        public Task<bool> OpenAsync(Uri uri)
        {
            var config = new MAM.BrowserLaunchOptions();
            return _current.OpenAsync(uri, config);
        }
    }
}