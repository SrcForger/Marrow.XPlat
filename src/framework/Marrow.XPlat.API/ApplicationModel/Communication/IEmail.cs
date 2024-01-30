using System.Threading.Tasks;

namespace Marrow.XPlat.ApplicationModel.Communication
{
    public interface IEmail
    {
        bool IsComposeSupported { get; }

        Task ComposeAsync(EmailMessage? message);
    }
}