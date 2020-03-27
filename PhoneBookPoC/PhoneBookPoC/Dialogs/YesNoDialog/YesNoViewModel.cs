using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace PhoneBookPoC.Dialogs.YesNoDialog
{
    public class YesNoViewModel : INotifyPropertyChanged
    {
        #region Properties
        private string _title = string.Empty;
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    NotifyPropertyChanged(nameof(Title));
                }
            }
        }

        private string _body = string.Empty;
        public string Body
        {
            get
            {
                return _body;
            }
            set
            {
                if (_body != value)
                {
                    _body = value;
                    NotifyPropertyChanged(nameof(Body));
                }
            }
        }

        private string _yesButtonText = string.Empty;
        public string YesButtonText
        {
            get
            {
                return _yesButtonText;
            }
            set
            {
                if (_yesButtonText != value)
                {
                    _yesButtonText = value;
                    NotifyPropertyChanged(nameof(YesButtonText));
                }
            }
        }

        private string _noButtonText = string.Empty;
        public string NoButtonText
        {
            get
            {
                return _noButtonText;
            }
            set
            {
                if (_noButtonText != value)
                {
                    _noButtonText = value;
                    NotifyPropertyChanged(nameof(NoButtonText));
                }
            }
        }

        private Style _yesButtonStyle;
        public Style YesButtonStyle
        {
            get
            {
                return _yesButtonStyle;
            }
            set
            {
                if (_yesButtonStyle != value)
                {
                    _yesButtonStyle = value;
                    NotifyPropertyChanged(nameof(YesButtonStyle));
                }
            }
        }
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
