using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MMS = Microsoft.Maui.Storage;

namespace Marrow.XPlat.Storage
{
    public sealed class MobileFilePicker : IFilePicker
    {
        private readonly MMS.IFilePicker _current;

        public MobileFilePicker(MMS.IFilePicker? current = null)
        {
            _current = current ?? MMS.FilePicker.Default;
        }

        private static MMS.PickOptions Convert(PickOptions? options)
        {
            var config = new MMS.PickOptions();
            if (options != null)
            {
                config.PickerTitle = options.PickerTitle;
            }
            return config;
        }

        private static IFileResult? Convert(MMS.FileResult? file)
        {
            if (file == null) return null;
            var res = new FileResult(file);
            return res;
        }

        public async Task<IFileResult?> PickAsync(PickOptions? options = null)
        {
            var config = Convert(options);
            var res = await _current.PickAsync(config);
            var conv = Convert(res);
            return conv;
        }

        public async Task<IEnumerable<IFileResult>?> PickMultipleAsync(PickOptions? options = null)
        {
            var config = Convert(options);
            var res = await _current.PickMultipleAsync(config);
            var conv = res.Select(result => Convert(result)!);
            return conv;
        }
    }
}
