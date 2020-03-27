using PhoneBookPoC.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhoneBookPoC.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreatePersonView : ContentPage
    {
        public CreatePersonView()
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
            if (BindingContext is CreatePersonViewModel vm)
            {
                vm.Unsubscribe();

                PersonForm.Unsubscribe();
            }

            return base.OnBackButtonPressed();
        }
    }
}