using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Marrow.XPlat.ApplicationModel.Communication
{
    public sealed class DesktopEmail : IEmail
    {
        public bool IsComposeSupported => true;

        public Task ComposeAsync(EmailMessage? message)
        {
            var url = new StringBuilder();
            url.Append($"mailto:{GetAsStr(message?.To)}?");
            if (GetAsStr(message?.Subject) is { } subject)
                url.Append($"subject={subject}&");
            if (GetAsStr(message?.Body) is { } body)
                url.Append($"body={body}&");
            if (GetAsStr(message?.Cc) is { } cc && !string.IsNullOrWhiteSpace(cc))
                url.Append($"cc={cc}&");
            if (GetAsStr(message?.Bcc) is { } bcc && !string.IsNullOrWhiteSpace(bcc))
                url.Append($"bcc={bcc}&");
            var mailTo = url.ToString().Trim('?', '&');

            var pInfo = new ProcessStartInfo(mailTo) { UseShellExecute = true };
            Process.Start(pInfo);
            return Task.CompletedTask;
        }

        private static string? GetAsStr(ICollection<string>? addresses)
            => addresses is not { Count: > 0 } ? null : string.Join(",", addresses);

        private static string? GetAsStr(string? raw)
            => string.IsNullOrWhiteSpace(raw) ? null : Uri.EscapeDataString(raw.Trim());
    }
}