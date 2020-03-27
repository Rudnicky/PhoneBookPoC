using Autofac;
using PhoneBookPoC.Controls.Form;
using PhoneBookPoC.DataAcess;
using PhoneBookPoC.DataAcess.DbContexts;
using PhoneBookPoC.DataAcess.Repositories;
using PhoneBookPoC.Services.Navigation;
using System;
using System.Globalization;
using System.Reflection;
using Xamarin.Forms;

namespace PhoneBookPoC.ViewModels.Base
{
    public static class ViewModelLocator
    {
        private static IContainer _container;

        public static readonly BindableProperty AutoWireViewModelProperty = BindableProperty.CreateAttached("AutoWireViewModel", 
                typeof(bool), 
                typeof(ViewModelLocator), 
                default(bool), 
                propertyChanged: OnAutoWireViewModelChanged);

        public static bool GetAutoWireViewModel(BindableObject bindable)
        {
            return (bool)bindable.GetValue(AutoWireViewModelProperty);
        }

        public static void SetAutoWireViewModel(BindableObject bindable, bool value)
        {
            bindable.SetValue(AutoWireViewModelProperty, value);
        }

        static ViewModelLocator()
        {
            var builder = new ContainerBuilder();

            // View models
            builder.RegisterType<PhoneBookViewModel>();
            builder.RegisterType<CreatePersonViewModel>();
            builder.RegisterType<UpdatePersonViewModel>();

            // Services
            builder.RegisterType<DbConfiguration>().As<IDbConfiguration>().SingleInstance();
            builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
            builder.RegisterType<PersonRepository>().As<IPersonRepository>().SingleInstance();

            if (_container != null)
            {
                _container.Dispose();
            }

            _container = builder.Build();
        }

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        private static void OnAutoWireViewModelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is Element view))
            {
                return;
            }

            var viewType = view.GetType();
            var viewName = viewType.FullName.Replace(".Views.", ".ViewModels.");
            var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
            var viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}Model, {1}", viewName, viewAssemblyName);

            var viewModelType = Type.GetType(viewModelName);
            if (viewModelType == null)
            {
                return;
            }
            var viewModel = _container.Resolve(viewModelType);
            view.BindingContext = viewModel;
        }
    }
}
