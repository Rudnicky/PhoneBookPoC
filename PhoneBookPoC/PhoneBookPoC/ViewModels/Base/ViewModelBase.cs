using PhoneBookPoC.DataAcess.Repositories;
using PhoneBookPoC.Services.Navigation;
using System.Threading.Tasks;

namespace PhoneBookPoC.ViewModels.Base
{
    public abstract class ViewModelBase : ExtendedBindableObject
    {
        protected readonly INavigationService NavigationService;
        protected readonly IPersonRepository PersonRepository;

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                RaisePropertyChanged(() => IsBusy);
            }
        }

        public ViewModelBase(INavigationService navigationService, IPersonRepository personRepository)
        {
            NavigationService = navigationService;
            PersonRepository = personRepository;
        }

        public virtual Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }
    }
}
