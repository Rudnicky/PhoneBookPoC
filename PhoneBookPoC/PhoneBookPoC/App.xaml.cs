using PhoneBookPoC.DataAcess;
using PhoneBookPoC.DataAcess.DbContexts;
using PhoneBookPoC.Services.Navigation;
using PhoneBookPoC.ViewModels.Base;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PhoneBookPoC
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        private async Task InitDatabase()
        {
            var dbConf = ViewModelLocator.Resolve<IDbConfiguration>();

            using (var context = new PhoneBookDbContext(dbConf))
            {
                await context.Database.EnsureCreatedAsync();
            }
        }

        private Task InitNavigation()
        {
            var navigationService = ViewModelLocator.Resolve<INavigationService>();

            return navigationService.InitializeAsync();
        }

        protected override async void OnStart()
        {
            base.OnStart();

            await InitDatabase();

            await InitNavigation();

            base.OnResume();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
