using PhoneBookPoC.Behaviors;
using PhoneBookPoC.Converters;
using PhoneBookPoC.Entities;
using PhoneBookPoC.Validators;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace PhoneBookPoC.Controls.Form
{
    public class PersonForm : ContentView
    {
        private FirstValidationErrorConverter _validationConverter;
        private InverseBoolConverter _inverseBoolConverter;
        private DataTrigger _firstNameErrorDataTrigger;
        private DataTrigger _lastNameErrorDataTrigger;
        private DataTrigger _phoneNameErrorDataTrigger;
        private Grid _rootContainer;
        private Label _firstNameLabel;
        private Label _lastNameLabel;
        private Label _phoneLabel;
        private Label _firstNameErrorLabel;
        private Label _lastNameErrorLabel;
        private Label _phoneErrorLabel;
        private Entry _firstNameEntry;
        private Entry _lastNameEntry;
        private Entry _phoneEntry;
        private Setter _errorSetter;

        public static readonly BindableProperty PersonProperty = BindableProperty.Create(
            propertyName: nameof(Person),
            returnType: typeof(Person),
            declaringType: typeof(PersonForm),
            defaultValue: null,
            propertyChanged: PersonPropertyHasChanged);

        private static void PersonPropertyHasChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is PersonForm control && newValue is Person person)
            {
                control.FirstName.Value = person.FirstName;
                control.LastName.Value = person.LastName;
                control.Phone.Value = person.Phone;
                control.BindingContext = control;
                control.Validate();
                control.SendPackage();
            }
        }

        public Person Person
        {
            get { return (Person)GetValue(PersonProperty); }
            set { SetValue(PersonProperty, value); }
        }

        public static readonly BindableProperty IsAddFormTypeProperty = BindableProperty.Create(
            propertyName: nameof(IsAddFormType),
            returnType: typeof(bool),
            declaringType: typeof(bool),
            defaultValue: false,
            propertyChanged: IsAddFormTypePropertyChanged);

        private static void IsAddFormTypePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is PersonForm control && (bool)newValue)
            {
                control.BindingContext = control;
            }
        }

        public bool IsAddFormType
        {
            get { return (bool)GetValue(IsAddFormTypeProperty); }
            set { SetValue(IsAddFormTypeProperty, value); }
        }

        private ValidatableObject<string> _firstName = new ValidatableObject<string>();
        public ValidatableObject<string> FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        private ValidatableObject<string> _lastName = new ValidatableObject<string>();
        public ValidatableObject<string> LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        private ValidatableObject<string> _phone = new ValidatableObject<string>();
        public ValidatableObject<string> Phone
        {
            get => _phone;
            set
            {
                _phone = value;
                OnPropertyChanged(nameof(Phone));
            }
        }

        public PersonForm()
        {            
            CreateView();
        }

        private void CreateView()
        {
            AddValidations();
            CreateRootContainer();
            CreateControls();
            SendPackage();
        }

        private void CreateRootContainer()
        {
            _rootContainer = new Grid();

            for (int i = 0; i <= 5; i++)
            {
                _rootContainer.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) });
            }

            _rootContainer.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            _rootContainer.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });
        }

        private void CreateControls()
        {
            _validationConverter = new FirstValidationErrorConverter();
            _inverseBoolConverter = new InverseBoolConverter();

            _errorSetter = new Setter
            {
                Property = LineColorBehavior.LineColorProperty,
                Value = Color.Red
            };

            /* FIRST NAME */
            _firstNameErrorDataTrigger = new DataTrigger(typeof(Entry)) { Value = false };
            _firstNameErrorDataTrigger.SetBinding(BindingContextProperty, "FirstName.IsValid");
            _firstNameErrorDataTrigger.Setters.Clear();
            _firstNameErrorDataTrigger.Setters.Add(_errorSetter);

            _firstNameLabel = new Label();
            _firstNameLabel.Text = "First Name";
            _firstNameLabel.SetDynamicResource(StyleProperty, "PrimaryLabelStyle");

            _firstNameErrorLabel = new Label();
            _firstNameErrorLabel.SetBinding(Label.TextProperty, "FirstName.Errors", converter: _validationConverter);
            _firstNameErrorLabel.SetBinding(IsVisibleProperty, "FirstName.IsValid", converter: _inverseBoolConverter);
            _firstNameErrorLabel.SetDynamicResource(StyleProperty, "ValidationErrorLabelStyle");

            _firstNameEntry = new Entry();
            _firstNameEntry.SetBinding(Entry.TextProperty, "FirstName.Value");
            _firstNameEntry.Triggers.Add(_firstNameErrorDataTrigger);
            _firstNameEntry.Completed += FirstNameEntry_Completed;
            _firstNameEntry.Unfocused += Control_Unfocused;
            _firstNameEntry.SetDynamicResource(StyleProperty, "EntryStyle");

            _rootContainer.Children.Add(_firstNameLabel, 0, 0);
            _rootContainer.Children.Add(_firstNameEntry, 1, 0);
            _rootContainer.Children.Add(_firstNameErrorLabel, 1, 1);

            /* LAST NAME */
            _lastNameErrorDataTrigger = new DataTrigger(typeof(Entry)) { Value = false };
            _lastNameErrorDataTrigger.SetBinding(BindingContextProperty, "LastName.IsValid");
            _lastNameErrorDataTrigger.Setters.Clear();
            _lastNameErrorDataTrigger.Setters.Add(_errorSetter);

            _lastNameLabel = new Label();
            _lastNameLabel.Text = "Last Name";
            _lastNameLabel.SetDynamicResource(StyleProperty, "PrimaryLabelStyle");

            _lastNameErrorLabel = new Label();
            _lastNameErrorLabel.SetBinding(Label.TextProperty, "LastName.Errors", converter: _validationConverter);
            _lastNameErrorLabel.SetBinding(IsVisibleProperty, "LastName.IsValid", converter: _inverseBoolConverter);
            _lastNameErrorLabel.SetDynamicResource(StyleProperty, "ValidationErrorLabelStyle");

            _lastNameEntry = new Entry();
            _lastNameEntry.SetBinding(Entry.TextProperty, "LastName.Value");
            _lastNameEntry.Triggers.Add(_firstNameErrorDataTrigger);
            _lastNameEntry.Completed += LastNameEntry_Completed;
            _lastNameEntry.Unfocused += Control_Unfocused;
            _lastNameEntry.SetDynamicResource(StyleProperty, "EntryStyle");

            _rootContainer.Children.Add(_lastNameLabel, 0, 2);
            _rootContainer.Children.Add(_lastNameEntry, 1, 2);
            _rootContainer.Children.Add(_lastNameErrorLabel, 1, 3);

            /* PHONE NAME */
            _phoneNameErrorDataTrigger = new DataTrigger(typeof(Entry)) { Value = false };
            _phoneNameErrorDataTrigger.SetBinding(BindingContextProperty, "Phone.IsValid");
            _phoneNameErrorDataTrigger.Setters.Clear();
            _phoneNameErrorDataTrigger.Setters.Add(_errorSetter);

            _phoneLabel = new Label();
            _phoneLabel.Text = "Phone";
            _phoneLabel.SetDynamicResource(StyleProperty, "PrimaryLabelStyle");

            _phoneErrorLabel = new Label();
            _phoneErrorLabel.SetBinding(Label.TextProperty, "Phone.Errors", converter: _validationConverter);
            _phoneErrorLabel.SetBinding(IsVisibleProperty, "Phone.IsValid", converter: _inverseBoolConverter);
            _phoneErrorLabel.SetDynamicResource(StyleProperty, "ValidationErrorLabelStyle");

            _phoneEntry = new Entry();
            _phoneEntry.SetBinding(Entry.TextProperty, "Phone.Value");
            _phoneEntry.Triggers.Add(_firstNameErrorDataTrigger);
            _phoneEntry.Completed += PhoneNumberEntry_Completed;
            _phoneEntry.Unfocused += Control_Unfocused;
            _phoneEntry.SetDynamicResource(StyleProperty, "EntryStyle");

            _rootContainer.Children.Add(_phoneLabel, 0, 4);
            _rootContainer.Children.Add(_phoneEntry, 1, 4);
            _rootContainer.Children.Add(_phoneErrorLabel, 1, 5);

            this.Content = _rootContainer;
        }

        private void Control_Unfocused(object sender, FocusEventArgs e)
        {
            SwitchFocus(FocusedElement.Default);

            SendPackage();
        }

        private void FirstNameEntry_Completed(object sender, EventArgs e)
        {
            _firstName.Validate();

            SwitchFocus(FocusedElement.LastNameEntry);

            SendPackage();
        }

        private void LastNameEntry_Completed(object sender, EventArgs e)
        {
            _lastName.Validate();

            SwitchFocus(FocusedElement.PhoneEntry);

            SendPackage();
        }

        private void PhoneNumberEntry_Completed(object sender, EventArgs e)
        {
            _phone.Validate();

            SwitchFocus(FocusedElement.Default);

            SendPackage();
        }

        private void SendPackage()
        {
            var args = new PersonFormEventArgs
            {
                IsValid = IsValid()
            };

            if (args.IsValid && Person != null)
            {
                var person = new Person
                {
                    Id = Person.Id,
                    FirstName = _firstName.Value,
                    LastName = _lastName.Value,
                    Phone = _phone.Value
                };

                args.Person = person;
            }
            else
            {
                var person = new Person
                {
                    Id = Guid.NewGuid(),
                    FirstName = _firstName.Value,
                    LastName = _lastName.Value,
                    Phone = _phone.Value
                };

                args.Person = person;
            }

            MessagingCenter.Send(this, "FormValidationHasChanged", args);
        }

        private bool IsValid()
        {
            return _firstName.Validate() && _lastName.Validate() && _phone.Validate();
        }

        private void AddValidations()
        {
            _firstName.Validations.AddRange(new List<IValidationRule<string>>()
            {
                new IsNotNullOrEmptyRule<string> { ValidationMessage = "First Name is required." },
                new DoesNotContainNumberRule<string> { ValidationMessage = "First Name should not contain a digit." },
                new IsLengthGreaterThenRule<string> { ValidationMessage = "Maximum length is '50' characters.", MaxLength = 50 }
            });

            _lastName.Validations.AddRange(new List<IValidationRule<string>>()
            {
                new IsNotNullOrEmptyRule<string> { ValidationMessage = "Last Name is required." },
                new DoesNotContainNumberRule<string> { ValidationMessage = "Last Name should not contain a digit." },
                new IsLengthGreaterThenRule<string> { ValidationMessage = "Maximum length is '80' characters.", MaxLength = 80 }
            });

            _phone.Validations.AddRange(new List<IValidationRule<string>>()
            {
                new IsNotNullOrEmptyRule<string> { ValidationMessage = "Phone Number is required." },
                new IsValidPhoneNumberRule<string> { ValidationMessage = "Invalid Number. Example: '+48 721 310 342'" }
            });
        }

        private void SwitchFocus(FocusedElement focusedElement)
        {
            switch (focusedElement)
            {
                case FocusedElement.FirstNameEntry:
                    _firstNameEntry.Focus();
                    _lastNameEntry.Unfocus();
                    _phoneEntry.Unfocus();
                    break;
                case FocusedElement.LastNameEntry:
                    _firstNameEntry.Unfocus();
                    _lastNameEntry.Focus();
                    _phoneEntry.Unfocus();
                    break;
                case FocusedElement.PhoneEntry:
                    _firstNameEntry.Unfocus();
                    _lastNameEntry.Unfocus();
                    _phoneEntry.Focus();
                    break;
                default:
                    _firstNameEntry.Unfocus();
                    _lastNameEntry.Unfocus();
                    _phoneEntry.Unfocus();
                    break;
            }
        }

        private enum FocusedElement
        {
            Default,
            FirstNameEntry,
            LastNameEntry,
            PhoneEntry
        }

        private void Validate()
        {
            _firstName.Validate();
            _lastName.Validate();
            _phone.Validate();
        }

        public void OnAttached()
        {
            SwitchFocus(FocusedElement.FirstNameEntry);
        }

        public void Unsubscribe()
        {
            _firstNameEntry.Completed -= FirstNameEntry_Completed;
            _firstNameEntry.Unfocused -= Control_Unfocused;
            _lastNameEntry.Completed -= LastNameEntry_Completed;
            _lastNameEntry.Unfocused -= Control_Unfocused;
            _phoneEntry.Completed -= PhoneNumberEntry_Completed;
            _phoneEntry.Unfocused -= Control_Unfocused;
        }
    }
}
