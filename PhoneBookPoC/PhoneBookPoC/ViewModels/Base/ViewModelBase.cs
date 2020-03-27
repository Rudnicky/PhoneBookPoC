using PhoneBookPoC.DataAcess.Repositories;
using PhoneBookPoC.Services;
using PhoneBookPoC.Services.Navigation;
using System.Threading.Tasks;

namespace PhoneBookPoC.ViewModels.Base
{
    public abstract class ViewModelBase : ExtendedBindableObject
    {
        protected readonly INavigationService NavigationService;
        protected readonly IPersonRepository PersonRepository;
        protected readonly ILogService Logger;

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

        public ViewModelBase(INavigationService navigationService, 
            IPersonRepository personRepository,
            ILogService logger)
        {
            NavigationService = navigationService;
            PersonRepository = personRepository;
            Logger = logger;
        }

        public virtual Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }
    }
}
