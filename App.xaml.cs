using Xamarin.Forms;

namespace CocoClientApp
{
	public partial class App : Application
	{
		public string AppName
		{
			get { return ConnectionHelper.AppName; }
		}

		public App()
		{
			InitializeComponent();

			var userInfo = ConnectionHelper.RetrieveUserInfo();
			if (userInfo != null)
			{
				var result = RegistrationDAO.Instance.LoginUser(userInfo.mail, userInfo.password);
				//if (result.data.lido != null)
				//{
				//	result.data.lido.booking_array = result.data.booking_array;

				//	MainPage = new NavigationPage(new HomePage(result.data.lido));	
				//}
				//else
				//{
				//	MainPage = new NavigationPage(new CocoVendorAppPage(null));
				//}
				MainPage = new NavigationPage(new CocoClientAppPage());
			}
			else
			{
				MainPage = new CocoClientAppPage();
			}
		}

		protected override void OnStart()
		{
			// Handle when your app starts
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
