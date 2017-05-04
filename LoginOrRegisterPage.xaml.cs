using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CocoClientApp
{
	public partial class LoginOrRegisterPage : ContentPage
	{
		public LoginOrRegisterPage()
		{
			InitializeComponent();

			NavigationPage.SetHasNavigationBar(this, false);

			var tapGesture = new TapGestureRecognizer
			{
				Command = new Command(() =>
				{
					var currUri = new Uri("https://cocoapp.it/termini-e-condizioni/");
					Device.OpenUri(currUri);
				}),
				NumberOfTapsRequired = 1
			};

			lblPrivacy.GestureRecognizers.Add(tapGesture);
		}

		async void Handle_Clicked(object sender, System.EventArgs e)
		{
		await Navigation.PushAsync(new LoginPage());
		}

		async void Handle_Clicked1(object sender, System.EventArgs e)
		{
		await Navigation.PushAsync(new LoginPage());
		}

		async void Handle_Clicked2(object sender, System.EventArgs e)
		{
		await Navigation.PushAsync(new RegisterPage());
		}

		async void Handle_Clicked3(object sender, System.EventArgs e)
		{
			await Navigation.PushAsync(new LoginPage());
		}
	}
}
