using Marrow.XPlat.ApplicationModel;
using Marrow.XPlat.ApplicationModel.Communication;
using Marrow.XPlat.ApplicationModel.DataTransfer;
using Marrow.XPlat.Devices;
using Marrow.XPlat.Media;
using Marrow.XPlat.Storage;
using Microsoft.Extensions.DependencyInjection;
using MAM = Microsoft.Maui.ApplicationModel;

namespace Marrow.XPlat.Setup
{
    public static class MobileExtensions
    {
        public static IServiceCollection AddMobile(this IServiceCollection builder)
        {
            builder.AddSingleton<IFileSystem, MobileFileSystem>();
            builder.AddSingleton<IPreferences, MobilePreferences>();
            builder.AddSingleton<IBrowser, MobileBrowser>();
            builder.AddSingleton<IEmail, MobileEmail>();
            builder.AddSingleton<ISecureStorage, MobileSecure>();
            builder.AddSingleton<IScreenshot, MobileScreenshot>();
            builder.AddSingleton<IClipboard, MobileClipboard>();
            builder.AddSingleton<IFilePicker, MobileFilePicker>();
            builder.AddSingleton<IAppInfo, MobileAppInfo>();
            builder.AddSingleton<IDeviceInfo, MobileDeviceInfo>();
            return builder;
        }

#if ANDROID
        public static void DoCreate(this Android.App.Activity activity, Android.OS.Bundle savedState)
            => MAM.Platform.Init(activity, savedState);

        public static void DoRequest(this Android.Content.PM.Permission[] grants, int reqCode, string[] perms)
            => MAM.Platform.OnRequestPermissionsResult(reqCode, perms, grants);
#endif
    }
}