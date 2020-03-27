using PhoneBookPoC.Droid.Implementations;
using PhoneBookPoC.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(ContextManager))]
namespace PhoneBookPoC.Droid.Implementations
{
    public class ContextManager : IContextManager
    {
        public static Android.Support.V7.View.ActionMode LastActionMode { get; set; }

        public void CloseLastContextMenu()
        {
            LastActionMode?.Finish();
        }
    }
}