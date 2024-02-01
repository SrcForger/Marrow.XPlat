using Android.App;
using Android.Content.PM;
using Avalonia;
using Avalonia.Android;
using Avalonia.ReactiveUI;
using Android.OS;
using Marrow.XPlat.Setup;
using Marrow.XPlat.Utils;
using Sample.Setup;

namespace Sample.Android
{
    [Activity(
        Label = "Sample.Android",
        Theme = "@style/MyTheme.NoActionBar",
        Icon = "@drawable/icon",
        MainLauncher = true,
        ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.UiMode)]
    public class MainActivity : AvaloniaMainActivity<App>
    {
        protected override AppBuilder CustomizeAppBuilder(AppBuilder builder)
        {
            SvcAppLocator.Bind(svc => svc.AddCore().AddMobile()).Init();

            return base.CustomizeAppBuilder(builder)
                .WithInterFont()
                .UseReactiveUI();
        }

        protected override void OnCreate(Bundle savedState)
        {
            base.OnCreate(savedState);
            this.DoCreate(savedState);
        }

        public override void OnRequestPermissionsResult(int reqCode, string[] perms, Permission[] grants)
        {
            grants.DoRequest(reqCode, perms);
            base.OnRequestPermissionsResult(reqCode, perms, grants);
        }
    }
}