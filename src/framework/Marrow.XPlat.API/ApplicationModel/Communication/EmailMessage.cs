using System.Collections.Generic;

namespace Marrow.XPlat.ApplicationModel.Communication
{
    public record EmailMessage(
        string? Subject = null,
        string? Body = null,
        List<string>? To = null,
        List<string>? Cc = null,
        List<string>? Bcc = null)
    {
    }
}