using System.IO;
using System.Threading.Tasks;

namespace Marrow.XPlat.Storage
{
    public interface IFileResult
    {
        string FileName { get; }

        string FullPath { get; }

        Task<Stream> OpenReadAsync();
    }
}