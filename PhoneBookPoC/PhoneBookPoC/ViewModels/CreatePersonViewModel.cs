using PhoneBookPoC.Controls.Form;
using PhoneBookPoC.DataAcess.Repositories;
using PhoneBookPoC.Entities;
using PhoneBookPoC.Services;
using PhoneBookPoC.Services.Navigation;
using PhoneBookPoC.ViewModels.Base;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PhoneBookPoC.ViewModels
{
    public class CreatePersonViewModel : ViewModelBase
    {
        private Person _entityToAdd;

        private bool _isAddButtonEnabled;
        public bool IsAddButtonEnabled
        {
            get => _isAddButtonEnabled;
            set
            {
                _isAddButtonEnabled = value;
                OnPropertyChanged(nameof(IsAddButtonEnabled));
            }
        }

        public ICommand AddPersonButtonClickedCommand => new Command(async () => await AddPersonButtonClicked());

        public CreatePersonViewModel(INavigationService navigationService, 
            IPersonRepository personRepository,
            ILogService logger) 
            : base(navigationService, personRepository, logger)
        {
            Subscribe();
        }

        private async Task AddPersonButtonClicked()
        {
            try
            {
                if (!IsBusy)
                {
                    IsBusy = true;

                    if (_entityToAdd != null)
                    {
                        await PersonRepository.AddPerson(_entityToAdd);
                    }

                    await NavigationService.PopAsync();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void Subscribe()
        {
            MessagingCenter.Subscribe<PersonForm, PersonFormEventArgs>(this, "FormValidationHasChanged", (sender, args) =>
            {
                if (args.IsValid)
                {
                    _entityToAdd = args.Person;

                    IsAddButtonEnabled = true;
                }
                else
                {
                    IsAddButtonEnabled = false;
                }
            });
        }

        public void Unsubscribe()
        {
            MessagingCenter.Unsubscribe<PersonForm, PersonFormEventArgs>(this, "FormValidationHasChanged");
        }
    }
}
