using System;
using Newtonsoft.Json;

namespace CompanyIOS
{
	public class TokenValidation
	{
		[JsonProperty("token")]
		public string Token {
			get;
			set;
		}
		[JsonProperty("ondate")]
		public string DateTime {
			get;
			set;
		}
		[JsonProperty("objid")]
		public string Objid {
			get;
			set;
		}
	}
}

