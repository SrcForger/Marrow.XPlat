using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marrow.XPlat.Storage
{
    public interface IFilePicker
    {
        Task<IFileResult?> PickAsync(PickOptions? options = null);

        Task<IEnumerable<IFileResult>?> PickMultipleAsync(PickOptions? options = null);
    }
}