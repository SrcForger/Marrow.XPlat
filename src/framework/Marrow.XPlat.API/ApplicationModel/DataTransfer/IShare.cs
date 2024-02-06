using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marrow.XPlat.ApplicationModel.DataTransfer
{
    public interface IShare
    {
        Task RequestAsync(ShareTextRequest request);

        Task RequestAsync(ShareFileRequest request);

        Task RequestAsync(ShareMultipleFilesRequest request);
    }

    public record ShareTextRequest
    {
        public string? Text { get; init; }

        public string? Uri { get; init; }

        public string? Title { get; init; }
    }

    public class ShareFileRequest
    {
        public string? Title { get; init; }

        public ShareFile? File { get; init; }
    }

    public class ShareMultipleFilesRequest
    {
        public string? Title { get; init; }

        public List<ShareFile>? Files { get; init; }
    }

    public record ShareFile(string Path)
    {
    }
}