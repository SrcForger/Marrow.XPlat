using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Marrow.XPlat.Avalonia.Tools;
using Marrow.XPlat.Storage;
using APS = Avalonia.Platform.Storage;

namespace Marrow.XPlat.Avalonia.Storage
{
    public class AvalFilePicker : IFilePicker
    {
        protected static APS.IStorageProvider Storage => UiHelper.GetTopLevel()?.StorageProvider!;

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

        protected static async Task<IEnumerable<IFileResult>> Pick(APS.FilePickerOpenOptions config, PickOptions? opt)
        {
            if (opt?.PickerTitle is { } title)
                config.Title = title;
            var res = await Storage.OpenFilePickerAsync(config);
            return res.Select(file => Convert(file)!);
        }
    }
}