using PhoneBookPoC.Controls.Form;
using PhoneBookPoC.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhoneBookPoC.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdatePersonView : ContentPage
    {
        public UpdatePersonView()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            PersonForm.OnAttached();
        }

        protected override bool OnBackButtonPressed()
        {
            if (BindingContext is UpdatePersonViewModel vm)
            {
                vm.Unsubscribe();

                PersonForm.Unsubscribe();
            }

            return base.OnBackButtonPressed();
        }
    }
}