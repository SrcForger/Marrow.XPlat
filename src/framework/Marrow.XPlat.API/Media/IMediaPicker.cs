using System.Threading.Tasks;
using Marrow.XPlat.Storage;

namespace Marrow.XPlat.Media
{
    public interface IMediaPicker
    {
        Task<IFileResult?> PickPhotoAsync(MediaPickerOptions? options = null);

        Task<IFileResult?> PickVideoAsync(MediaPickerOptions? options = null);
    }
}