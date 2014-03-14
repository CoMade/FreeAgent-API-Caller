using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using FreeAgentApiCaller.Properties;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FreeAgentDataDownloader.OAuth
{
	public class OAuthFreeAgentAccess
	{

		// public static string ApiUrl = "https://api.freeagent.com/v2/";
		public static string ApiUrl = "https://api.sandbox.freeagent.com/v2/";

		// This can be anything, it is recommended to use an application name and version number to help with debugging
		public static string UserAgentString = "FreeAgent API Caller by Co Made version " + Assembly.GetCallingAssembly().GetName().Version;

		private static string GetNewAccessToken()
		{
			string url = ApiUrl + "token_endpoint?client_secret=" + Settings.Default.FaClientSecret +
						 "&grant_type=refresh_token&refresh_token=" + Settings.Default.FaRefreshToken + "&client_id=" +
						 Settings.Default.FaClientId;
			var wr = (HttpWebRequest)WebRequest.Create(url);
			wr.UserAgent = UserAgentString;
			wr.ContentType = "application/json";
			wr.Method = "POST";
			var resp = wr.GetResponse();
			using (var rs = resp.GetResponseStream())
			{
				var sr = new StreamReader(rs);
				var accessToken = JsonConvert.DeserializeObject<AccessToken>(sr.ReadToEnd());
				return accessToken.access_token;
			}
		}

		private static string GetJsonFromFa(string url)
		{
			string fullUrl = ApiUrl + url;
			var wr = (HttpWebRequest)WebRequest.Create(fullUrl);
			wr.UserAgent = UserAgentString;
			wr.ContentType = "application/json";
			// Access token could be stored but this guarantees that it will never be out of date. 
			wr.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + GetNewAccessToken());
			wr.Method = "Get";
			var resp = wr.GetResponse();
			using (var responseStream = resp.GetResponseStream())
			{
				var sr = new StreamReader(responseStream);
				return sr.ReadToEnd();
			}
		}

		public static T GetData<T>(string url, Func<T, T> resultModifier) where T : IList, new()
		{
			var page = 1;
			var perPage = 100;
			var lastThingCount = 0;
			var thingsToReturn = new T();
			do
			{
				var urlForThisPage = String.Format("{0}page={1}&per_page={2}", url, page, perPage);
				var responseAsJson = GetJsonFromFa(urlForThisPage);
				var arrayOfObjectsAsJson = JObject.Parse(responseAsJson).First.First.ToString(); // get the first element of the first element as json (this returns just the array of objects)
				var listOfThings = JsonConvert.DeserializeObject<T>(arrayOfObjectsAsJson);
				lastThingCount = ((ICollection)listOfThings).Count;
				foreach (var thing in listOfThings)
				{
					thingsToReturn.Add(thing);
				}
				page++;
			} while (lastThingCount != 0);
			if (resultModifier != null)
				return resultModifier(thingsToReturn);
			return thingsToReturn;
		}

		class AccessToken
		{
			public string access_token { get; set; }
			public string token_type { get; set; }
			public string expires_in { get; set; }
		}
	}
}
