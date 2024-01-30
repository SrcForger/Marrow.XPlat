using System.Threading.Tasks;
using MAC = Microsoft.Maui.ApplicationModel.Communication;

namespace Marrow.XPlat.ApplicationModel.Communication
{
    public sealed class MobileEmail : IEmail
    {
        private readonly MAC.IEmail _current;

        public MobileEmail(MAC.IEmail? current = null)
        {
            _current = current ?? MAC.Email.Default;
        }

        public bool IsComposeSupported => _current.IsComposeSupported;

        public Task ComposeAsync(EmailMessage? message)
        {
            var copy = new MAC.EmailMessage();
            if (message is not null)
            {
                copy.Subject = message.Subject;
                copy.Body = message.Body;
                copy.To = message.To;
                copy.Cc = message.Cc;
                copy.Bcc = message.Bcc;
            }
            copy.BodyFormat = MAC.EmailBodyFormat.PlainText;
            return _current.ComposeAsync(copy);
        }
    }
}