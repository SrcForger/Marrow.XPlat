using System.Linq;
using System.Threading.Tasks;
using Marrow.XPlat.Avalonia.Storage;
using Marrow.XPlat.Media;
using Marrow.XPlat.Storage;
using APS = Avalonia.Platform.Storage;

namespace Marrow.XPlat.Avalonia.Media
{
    public sealed class AvalMediaPicker : AvalFilePicker, IMediaPicker
    {
        private static PickOptions Convert(MediaPickerOptions? options)
        {
            return new PickOptions { PickerTitle = options?.Title };
        }

        public async Task<IFileResult?> PickPhotoAsync(MediaPickerOptions? options = null)
        {
            var root = await Storage.TryGetWellKnownFolderAsync(APS.WellKnownFolder.Pictures);
            var types = new[] { PickerFileTypes.AllImages };
            var config = new APS.FilePickerOpenOptions
            {
                AllowMultiple = false, FileTypeFilter = types, SuggestedStartLocation = root
            };
            var opt = Convert(options);
            var res = await Pick(config, opt);
            var one = res.SingleOrDefault();
            return one;
        }

        public async Task<IFileResult?> PickVideoAsync(MediaPickerOptions? options = null)
        {
            var root = await Storage.TryGetWellKnownFolderAsync(APS.WellKnownFolder.Videos);
            var types = new[] { PickerFileTypes.AllVideos };
            var config = new APS.FilePickerOpenOptions
            {
                AllowMultiple = false, FileTypeFilter = types, SuggestedStartLocation = root
            };
            var opt = Convert(options);
            var res = await Pick(config, opt);
            var one = res.SingleOrDefault();
            return one;
        }
    }
}