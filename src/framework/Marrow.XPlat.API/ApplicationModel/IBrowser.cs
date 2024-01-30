using System.Threading.Tasks;
using System;

namespace Marrow.XPlat.ApplicationModel
{
    public interface IBrowser
    {
        Task<bool> OpenAsync(Uri uri);
    }
}