using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CocoClientApp
{
	public class RegistrationDAO
	{
		private static RegistrationDAO mInstance;

		public static RegistrationDAO Instance
		{
			get
			{
				if (mInstance == null)
				{
					mInstance = new RegistrationDAO();
				}
				return mInstance;
			}
		}

		public RegistrationDAO()
		{
		}

		private Task<HttpResponseMessage> ExecuteRequest(ConnectionHelper.WebServiceCallType calltype, IList<KeyValuePair<string, string>> paramList, string url)
		{
			var uri = new Uri(ConnectionHelper.AppUrl + url);

			var client = new HttpClient();

			if (calltype == ConnectionHelper.WebServiceCallType.Post)
			{
				return client.PostAsync(uri, new System.Net.Http.FormUrlEncodedContent(paramList));
			}
			else if (calltype == ConnectionHelper.WebServiceCallType.Put)
			{
				return client.PutAsync(uri, new System.Net.Http.FormUrlEncodedContent(paramList));
			}
			else
			{
				client.BaseAddress = new Uri("");
				return client.DeleteAsync(url);
			}
		}

		//private Task<HttpResponseMessage> ExecuteRequest(IList<KeyValuePair<string, object>> paramList, string url)
		//{
		//	var uri = new Uri(ConnectionHelper.AppUrl + url);

		//	var client = new HttpClient();
		//	return client.PostAsync(uri, new System.Net.Http.FormUrlEncodedContent(paramList));
		//}

		public WebServiceResponseDTO<object> RegisterUser(string mail, string passw)
		{
			string url = "vendor/sign-up";

			var listParam = new List<KeyValuePair<string, string>>();
			listParam.Add(new KeyValuePair<string, string>("email", mail));
			listParam.Add(new KeyValuePair<string, string>("password", passw));

			Task<HttpResponseMessage> response = null;
			response = ExecuteRequest(ConnectionHelper.WebServiceCallType.Put, listParam, url);

			var ctn = response.GetAwaiter().GetResult().Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<WebServiceResponseDTO<object>>(ctn.Result, new JsonSerializerSettings
			{
				DateParseHandling = DateParseHandling.None
			});
		}

		public WebServiceResponseDTO<UserWebServiceDTO> LoginUser(string mail, string passw)
		{
			string url = "vendor/login";

			var listParam = new List<KeyValuePair<string, string>>();
			listParam.Add(new KeyValuePair<string, string>("email", mail));
			listParam.Add(new KeyValuePair<string, string>("password", passw));

			Task<HttpResponseMessage> response = null;
			response = ExecuteRequest(ConnectionHelper.WebServiceCallType.Post, listParam, url);

			var ctn = response.GetAwaiter().GetResult().Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<WebServiceResponseDTO<UserWebServiceDTO>>(ctn.Result, new JsonSerializerSettings
			{
				DateParseHandling = DateParseHandling.None
			});
		}
	}
}
