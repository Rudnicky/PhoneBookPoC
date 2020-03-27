using System;

namespace PhoneBookPoC.Dialogs.YesNoDialog
{
    public class YesNoDialogEventArgs : EventArgs
    {
        public bool Confirmed { get; set; }

        public object Obj { get; set; }

        public YesNoDialogEventArgs(bool confirmed, object obj = null)
        {
            Confirmed = confirmed;
            Obj = obj;
        }
    }
}
