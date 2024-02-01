using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Marrow.XPlat.Avalonia.Tools;
using Marrow.XPlat.Storage;
using APS = Avalonia.Platform.Storage;

namespace Marrow.XPlat.Avalonia.Storage
{
    public sealed class AvalFilePicker : IFilePicker
    {
        private static APS.IStorageProvider Storage => UiHelper.GetTopLevel()?.StorageProvider!;

        private static IFileResult? Convert(APS.IStorageFile? file)
        {
            if (file == null) return null;
            var res = new FileResult(file);
            return res;
        }

        public async Task<IFileResult?> PickAsync(PickOptions? options = null)
        {
            var config = new APS.FilePickerOpenOptions { AllowMultiple = false };
            var all = await Pick(config, options);
            var res = all.SingleOrDefault();
            return res;
        }

        public async Task<IEnumerable<IFileResult>?> PickMultipleAsync(PickOptions? options = null)
        {
            var config = new APS.FilePickerOpenOptions { AllowMultiple = true };
            var res = await Pick(config, options);
            return res;
        }

        private static async Task<IEnumerable<IFileResult>> Pick(APS.FilePickerOpenOptions config, PickOptions? options)
        {
            if (options?.PickerTitle is { } title)
                config.Title = title;
            var res = await Storage.OpenFilePickerAsync(config);
            return res.Select(file => Convert(file)!);
        }

        private sealed class FileResult : IFileResult
        {
            private readonly APS.IStorageFile _wrap;

            public FileResult(APS.IStorageFile wrap)
            {
                _wrap = wrap;
            }

            public Task<Stream> OpenReadAsync() => _wrap.OpenReadAsync();
            public string FileName => _wrap.Name;

            public string FullPath
            {
                get
                {
                    if (_wrap.Path is { IsAbsoluteUri: true, Scheme: "file" } absolute)
                        return absolute.LocalPath;
                    return _wrap.Path.ToString();
                }
            }
        }
    }
}