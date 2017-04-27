using Xamarin.Forms;

namespace CocoClientApp
{
	public partial class CocoClientAppPage : CarouselPage
	{
		public CocoClientAppPage()
		{
			InitializeComponent();

			NavigationPage.SetHasNavigationBar(this, false);
		}
	}
}
