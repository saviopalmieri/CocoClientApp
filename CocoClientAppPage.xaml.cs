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

		async void Handle_Clicked(object sender, System.EventArgs e)
		{
			await Navigation.PushAsync(new LoginOrRegisterPage());
		}
	}
}
