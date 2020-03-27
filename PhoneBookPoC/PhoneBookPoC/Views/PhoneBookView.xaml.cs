
using PhoneBookPoC.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhoneBookPoC.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PhoneBookView : ContentPage
    {
        public PhoneBookView()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is PhoneBookViewModel vm)
            {
                await vm.OnAppearing();
            }
        }
    }
}