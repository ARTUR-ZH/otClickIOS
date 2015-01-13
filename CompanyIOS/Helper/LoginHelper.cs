using System;
using Newtonsoft.Json;

namespace CompanyIOS
{
	public class LoginHelper
	{
		[JsonProperty("login")]
		public string Login {
			get;
			set;
		}
		[JsonProperty("password")]
		public string Password {
			get;
			set;
		}
	}
}

