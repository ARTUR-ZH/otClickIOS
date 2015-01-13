using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CompanyIOS
{
	public class ServiceCallbackHelper
	{
		[JsonProperty("status")]
		public string Status {
			get;
			set;
		}
		[JsonProperty("error")]
		public ErrosHelper Error {
			get;
			set;
		}
		[JsonProperty("resource")]
		public TokensHelper Resource {
			get;
			set;
		}
	}
}

