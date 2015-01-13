using System;
using Newtonsoft.Json;

namespace CompanyIOS
{
	public class TokensHelper
	{
		[JsonProperty("token")]
		public string Token {
			get;
			set;
		}
	}

}

