using System;
using System.Linq;
using Xamarin.Auth;

namespace CocoClientApp
{
	public static class ConnectionHelper
	{
		public static string AppUrl
		{
			get
			{
				#if DEBUG
					return "https://cocoapp.it/api/v2/web/app_dev.php/";
				#else
					return "https://cocoapp.it/api/v2/web/";
				#endif
			}
		}

		public static string AppName
		{
			get
			{
				return "CocoAppClient";
			}
		}

		public static void StoreUserInfo(UserInfoDTO userInfo)
		{
			Account account = new Account
			{
				Username = userInfo.mail
			};

			account.Properties.Add("password", userInfo.password);
			account.Properties.Add("apikey", userInfo.apiKey);
			AccountStore.Create().Save(account, ConnectionHelper.AppName);
		}

		public static void ClearUserInfo()
		{
			var acc = AccountStore.Create();

			acc.Delete(acc.FindAccountsForService(ConnectionHelper.AppName).FirstOrDefault(), ConnectionHelper.AppName);
		}

		public static UserInfoDTO RetrieveUserInfo()
		{
			var account = AccountStore.Create().FindAccountsForService(ConnectionHelper.AppName).FirstOrDefault();
			if (account == null)
			{
				return null;
			}
			else
			{
				return new UserInfoDTO
				{
					mail = account.Username,
					password = account.Properties["password"],
					apiKey = account.Properties["apikey"]
				};
			}
		}

		public enum WebServiceCallType
		{
			Post,
			Put,
			Get,
			Delete
		}
	}
}
