﻿using PhoneBookPoC.Controls.Form;
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
    public class UpdatePersonViewModel : ViewModelBase
    {
        private Person _person;
        public Person Person
        {
            get => _person;
            set
            {
                _person = value;
                OnPropertyChanged(nameof(Person));
            }

        }

        private bool _isUpdateButtonEnabled;
        public bool IsUpdateButtonEnabled
        {
            get => _isUpdateButtonEnabled;
            set
            {
                _isUpdateButtonEnabled = value;
                OnPropertyChanged(nameof(IsUpdateButtonEnabled));
            }
        }

        public ICommand UpdatePersonButtonClickedCommand => new Command(async () => await UpdatePersonButtonClicked());

        public UpdatePersonViewModel(INavigationService navigationService,
            IPersonRepository personRepository,
            ILogService logger)
            : base(navigationService, personRepository, logger)
        {
            Subscribe();

            // test purpose
            Logger.LogInfo("UpdatePersonViewModel - Started!");
        }

        public override async Task InitializeAsync(object obj)
        {
            Person = (Person)obj;
        }

        private async Task UpdatePersonButtonClicked()
        {
            try
            {
                if (!IsBusy)
                {
                    IsBusy = true;

                    if (_person != null)
                    {
                        await PersonRepository.UpdatePerson(_person);
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
                    _person = args.Person;

                    IsUpdateButtonEnabled = true;
                }
                else
                {
                    IsUpdateButtonEnabled = false;
                }
            });
        }

        public void Unsubscribe()
        {
            MessagingCenter.Unsubscribe<PersonForm, PersonFormEventArgs>(this, "FormValidationHasChanged");
        }
    }
}
