using PhoneBookPoC.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(NavigationPage), typeof(CustomNavigationPageRenderer))]
namespace PhoneBookPoC.iOS.Renderers
{
    public class CustomNavigationPageRenderer : NavigationRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var att = new UITextAttributes
                {
                    Font = UIFont.FromName("Montserrat-Bold", 20)
                };

                UINavigationBar.Appearance.SetTitleTextAttributes(att);
            }
        }
    }
}