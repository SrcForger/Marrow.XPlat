using System.Threading.Tasks;
using Marrow.XPlat.Storage;
using MMM = Microsoft.Maui.Media;
using MMS = Microsoft.Maui.Storage;

namespace Marrow.XPlat.Media
{
    public sealed class MobileMediaPicker : IMediaPicker
    {
        private readonly MMM.IMediaPicker _current;

        public MobileMediaPicker(MMM.IMediaPicker? current = null)
        {
            _current = current ?? MMM.MediaPicker.Default;
        }

        private static MMM.MediaPickerOptions Convert(MediaPickerOptions? options)
        {
            var config = new MMM.MediaPickerOptions();
            if (options != null)
            {
                config.Title = options.Title;
            }
            return config;
        }

        private static IFileResult? Convert(MMS.FileResult? file)
        {
            if (file == null) return null;
            var res = new FileResult(file);
            return res;
        }

        public async Task<IFileResult?> PickPhotoAsync(MediaPickerOptions? options = null)
        {
            var config = Convert(options);
            var res = await _current.PickPhotoAsync(config);
            var conv = Convert(res);
            return conv;
        }

        public async Task<IFileResult?> PickVideoAsync(MediaPickerOptions? options = null)
        {
            var config = Convert(options);
            var res = await _current.PickVideoAsync(config);
            var conv = Convert(res);
            return conv;
        }
    }
}