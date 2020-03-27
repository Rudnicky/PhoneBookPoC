using Xamarin.Forms;

namespace PhoneBookPoC.AttachedProperties
{
    public static class FocusExtension
    {
        public static readonly BindableProperty IsFocusedProperty =
  BindableProperty.CreateAttached("IsFocused", typeof(bool), typeof(FocusExtension), false, BindingMode.TwoWay, propertyChanged: OnIsFocusedPropertyChanged);

        private static void OnIsFocusedPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is VisualElement visualElement)
            {
                if ((bool)newValue)
                {
                    visualElement.Focus();
                }
                else
                {
                    visualElement.Unfocus();
                }
            }
        }

        public static bool GetIsFocused(BindableObject view)
        {
            return (bool)view.GetValue(IsFocusedProperty);
        }

        public static void SetIsFocused(BindableObject view, bool value)
        {
            view.SetValue(IsFocusedProperty, value);
        }
    }
}
