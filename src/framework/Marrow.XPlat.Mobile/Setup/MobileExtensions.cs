using Marrow.XPlat.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.ApplicationModel;

namespace Marrow.XPlat.Mobile
{
    public static class MobileExtensions
    {
        public static IServiceCollection AddMobile(this IServiceCollection builder)
        {
            builder.AddSingleton<IFileSystem, MobileFileSystem>();
            return builder;
        }

#if ANDROID
        public static void DoCreate(this Android.App.Activity activity, Android.OS.Bundle savedState)
            => Platform.Init(activity, savedState);

        public static void DoRequest(this Android.Content.PM.Permission[] grants, int reqCode, string[] perms)
            => Platform.OnRequestPermissionsResult(reqCode, perms, grants);
#endif
    }
}