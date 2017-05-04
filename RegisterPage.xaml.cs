using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CocoClientApp
{
	public partial class RegisterPage : ContentPage
	{
		public RegisterPage()
		{
			InitializeComponent();

			var tapGesture = new TapGestureRecognizer
			{
				Command = new Command(() =>
				{
					var currUri = new Uri("https://cocoapp.it/termini-e-condizioni/");
					Device.OpenUri(currUri);
				}),
				NumberOfTapsRequired = 1
			};

			lblProvacy.GestureRecognizers.Add(tapGesture);
		}

		async void Handle_Clicked(object sender, System.EventArgs e)
		{
			var email = txtEmail.Text;
			var password = txtPassword1.Text;
			var nome = txtNome.Text;
			var cognome = txtCognome.Text;

			if (string.IsNullOrEmpty(email) || !email.Contains("@"))
			{
				await DisplayAlert("Errore", "Email non valida! Controlla i dati e riprova.", "Chiudi");
				return;
			}

			if (string.IsNullOrEmpty(password))
			{
				await DisplayAlert("Errore", "Inserire una password valida.", "Chiudi");
				return;
			}

			if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(cognome))
			{
				await DisplayAlert("Errore", "Inserire Nome e Cognome validi.", "Chiudi");
				return;
			}

			if (txtEmail.Text != txtEmail2.Text)
			{
				await DisplayAlert("Errore", "La mail inserita non combacia. Controlla i dati e riprova.", "Chiudi");
				return;
			}

			if (txtPassword1.Text != txtPassword2.Text)
			{
				await DisplayAlert("Errore", "La password inserita non combacia. Controlla i dati e riprova.", "Chiudi");
				return;
			}

			if (!switchPrivacy.IsToggled)
			{
				await DisplayAlert("Errore", "Accetta i termini e condizioni per registrarti a COCO.", "Chiudi");
				return;
			}

			var response = RegistrationDAO.Instance.RegisterUser(email, password);

			if (response == null)
			{
				await DisplayAlert("Errore", "Errore contattando il servizio!", "Chiudi");
			}
			else
			{
				if (!response.error)
				{
					try
					{
						var result = RegistrationDAO.Instance.LoginUser(email, password);

						if (!result.error)
						{
							ConnectionHelper.StoreUserInfo(new UserInfoDTO { mail = email, password = password, apiKey = result.data.api_key });
						}
					}
					catch (Exception ex)
					{
						var a = 10;
					}

				}
				else
				{
					await DisplayAlert("Errore", response.message, "Chiudi");
				}
			}
		}
	}
}
