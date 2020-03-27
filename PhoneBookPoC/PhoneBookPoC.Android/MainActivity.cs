using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using PhoneBookPoC.Droid.Implementations;

namespace PhoneBookPoC.Droid
{
    [Activity(Label = "PhoneBookPoC", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public override Android.Support.V7.View.ActionMode StartSupportActionMode(Android.Support.V7.View.ActionMode.ICallback callback)
        {
            return ContextManager.LastActionMode = base.StartSupportActionMode(callback);
        }

        public override void OnSupportActionModeFinished(Android.Support.V7.View.ActionMode mode)
        {
            ContextManager.LastActionMode = null;
            base.OnSupportActionModeFinished(mode);
        }
    }
}