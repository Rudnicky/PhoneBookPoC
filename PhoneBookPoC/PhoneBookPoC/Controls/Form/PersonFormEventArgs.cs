using PhoneBookPoC.Entities;
using System;

namespace PhoneBookPoC.Controls.Form
{
    public class PersonFormEventArgs : EventArgs
    {
        public Person Person { get; set; }
        public bool IsValid { get; set; }
    }
}
