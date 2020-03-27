using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhoneBookPoC.Dialogs.YesNoDialog
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class YesNoDialog : Rg.Plugins.Popup.Pages.PopupPage
    {
        private readonly object _obj;
        private YesNoViewModel _viewModel;
        public EventHandler<YesNoDialogEventArgs> Answered;

        #region Bindable Properties
        public new static readonly BindableProperty TitleProperty = BindableProperty.Create(
            propertyName: nameof(Title),
            returnType: typeof(string),
            declaringType: typeof(YesNoDialog),
            defaultValue: string.Empty,
            propertyChanged: TitlePropertyHasChanged);

        private static void TitlePropertyHasChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is YesNoDialog control)
            {
                control.TitleHasChanged((string)newValue);
            }
        }

        private void TitleHasChanged(string title)
        {
            if (!string.IsNullOrEmpty(title) && _viewModel != null)
            {
                _viewModel.Title = title;
            }
        }

        public new string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly BindableProperty BodyProperty = BindableProperty.Create(
            propertyName: nameof(Body),
            returnType: typeof(string),
            declaringType: typeof(YesNoDialog),
            defaultValue: string.Empty,
            propertyChanged: BodyPropertyHasChanged);

        private static void BodyPropertyHasChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is YesNoDialog control)
            {
                control.BodyHasChanged((string)newValue);
            }
        }

        private void BodyHasChanged(string body)
        {
            if (!string.IsNullOrEmpty(body) && _viewModel != null)
            {
                _viewModel.Body = body;
            }
        }

        public string Body
        {
            get { return (string)GetValue(BodyProperty); }
            set { SetValue(BodyProperty, value); }
        }

        public static readonly BindableProperty YesButtonTextProperty = BindableProperty.Create(
            propertyName: nameof(YesButtonText),
            returnType: typeof(string),
            declaringType: typeof(YesNoDialog),
            defaultValue: "Yes",
            propertyChanged: YesButtonTextPropertyChanged);

        private static void YesButtonTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is YesNoDialog control)
            {
                control.YesButtonTextChanged((string)newValue);
            }
        }

        private void YesButtonTextChanged(string yesButtonText)
        {
            if (!string.IsNullOrEmpty(yesButtonText) && _viewModel != null)
            {
                _viewModel.YesButtonText = yesButtonText;
            }
        }

        public string YesButtonText
        {
            get { return (string)GetValue(YesButtonTextProperty); }
            set { SetValue(YesButtonTextProperty, value); }
        }

        public static readonly BindableProperty NoButtonTextProperty = BindableProperty.Create(
            propertyName: nameof(NoButtonText),
            returnType: typeof(string),
            declaringType: typeof(YesNoDialog),
            defaultValue: "Close",
            propertyChanged: NoButtonTextPropertyChanged);

        private static void NoButtonTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is YesNoDialog control)
            {
                control.NoButtonTextChanged((string)newValue);
            }
        }

        private void NoButtonTextChanged(string noButtonText)
        {
            if (!string.IsNullOrEmpty(noButtonText) && _viewModel != null)
            {
                _viewModel.NoButtonText = noButtonText;
            }
        }

        public string NoButtonText
        {
            get { return (string)GetValue(NoButtonTextProperty); }
            set { SetValue(NoButtonTextProperty, value); }
        }

        public static readonly BindableProperty YesButtonStyleProperty = BindableProperty.Create(
            propertyName: nameof(YesButtonStyle),
            returnType: typeof(Style),
            defaultValue: (Style)Application.Current.Resources["ButtonStyle"],
            declaringType: typeof(YesNoDialog),
            propertyChanged: YesButtonStylePropertyChanged);

        private static void YesButtonStylePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is YesNoDialog control)
            {
                control.YesButtonStyleChanged((Style)newValue);
            }
        }

        private void YesButtonStyleChanged(Style yesButtonStyle)
        {
            _viewModel.YesButtonStyle = yesButtonStyle;
        }

        public Style YesButtonStyle
        {
            get { return (Style)GetValue(YesButtonStyleProperty); }
            set { SetValue(YesButtonStyleProperty, value); }
        }
        #endregion

        #region Ctor
        public YesNoDialog()
        {
            InitializeComponent();
            InitializeViewModel();
            InitializeDefaultValues();
        }

        public YesNoDialog(object obj)
        {
            InitializeComponent();
            InitializeViewModel();
            InitializeDefaultValues();

            _obj = obj;
        }
        #endregion

        #region Private Methods
        private void InitializeViewModel()
        {
            if (_viewModel == null)
                _viewModel = new YesNoViewModel();

            this.BindingContext = _viewModel;
        }

        private void InitializeDefaultValues()
        {
            _viewModel.YesButtonText = YesButtonText;
            _viewModel.NoButtonText = NoButtonText;
            _viewModel.YesButtonStyle = YesButtonStyle;
        }
        #endregion

        #region Private Events
        private async void CloseButton_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();

            Answered?.Invoke(sender, new YesNoDialogEventArgs(false, _obj));
        }

        private async void UpdateButton_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();

            Answered?.Invoke(sender, new YesNoDialogEventArgs(true, _obj));
        }
        #endregion
    }
}