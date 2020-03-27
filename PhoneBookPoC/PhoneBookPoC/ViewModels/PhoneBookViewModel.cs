using PhoneBookPoC.DataAcess.Repositories;
using PhoneBookPoC.Dialogs.YesNoDialog;
using PhoneBookPoC.Entities;
using PhoneBookPoC.Services.Navigation;
using PhoneBookPoC.ViewModels.Base;
using Rg.Plugins.Popup.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PhoneBookPoC.ViewModels
{
    public class PhoneBookViewModel : ViewModelBase
    {
        private YesNoDialog _dialog;

        private ObservableCollection<Person> _people = new ObservableCollection<Person>();
        public ObservableCollection<Person> People
        {
            get => _people;
            set
            {
                _people = value;
                OnPropertyChanged(nameof(People));
            }
        }

        public ICommand AddButtonClickedCommand => new Command(async () => await AddButtonClicked());
        public ICommand DeletePersonMenuItemClickedCommand => new Command(async (object obj) => await DeletePersonMenuItemClicked(obj));
        public ICommand ModifyPersonMenuItemClickedCommand => new Command(async (object obj) => await ModifyPersonMenuItemClicked(obj));

        public PhoneBookViewModel(INavigationService navigationService, IPersonRepository personRepository) 
            : base(navigationService, personRepository)
        {
        }

        public async Task OnAppearing()
        {
            var people = await PersonRepository.GetPeople();
            if (people != null)
            {
                People = new ObservableCollection<Person>(people);
            }
        }

        private async Task AddButtonClicked()
        {
            if (!IsBusy)
            {
                IsBusy = true;

                await NavigationService.NavigateToAsync<CreatePersonViewModel>();

                IsBusy = false;
            }
        }

        private async Task DeletePersonMenuItemClicked(object obj)
        {
            if (!IsBusy)
            {
                IsBusy = true;

                await ShowDialog(obj);

                IsBusy = false;
            }
        }

        private async Task ModifyPersonMenuItemClicked(object obj)
        {
            if (!IsBusy)
            {
                IsBusy = true;

                if (obj is Person person)
                {
                    await NavigationService.NavigateToAsync<UpdatePersonViewModel>(person);
                }

                IsBusy = false;
            }
        }

        private async void DeleteDialogAnswered(object sender, YesNoDialogEventArgs e)
        {
            if (e.Confirmed && e.Obj is Person person)
            {
                await PersonRepository.DeletePerson(person.Id);

                People.Remove(person);

                _dialog.Answered -= DeleteDialogAnswered;
            }
        }

        private async Task ShowDialog(object obj)
        {
            _dialog = new YesNoDialog(obj)
            {
                Title = "Warning",
                Body = "This operation will permamently delete this record.",
                YesButtonText = "Delete",
                NoButtonText = "Close",
                YesButtonStyle = (Style)Application.Current.Resources["DangerButtonStyle"]
            };

            _dialog.Answered += DeleteDialogAnswered;

            await PopupNavigation.Instance.PushAsync(_dialog);
        }
    }
}
