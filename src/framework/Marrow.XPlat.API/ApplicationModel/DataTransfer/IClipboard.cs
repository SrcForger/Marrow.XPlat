using System.Threading.Tasks;

namespace Marrow.XPlat.ApplicationModel.DataTransfer
{
    public interface IClipboard
    {
        Task SetTextAsync(string? text);

        Task<string?> GetTextAsync();
    }
}