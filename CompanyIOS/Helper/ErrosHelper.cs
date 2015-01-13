using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CompanyIOS
{
	public class ErrosHelper
	{
		[JsonProperty("errorCode")]
		public string ErrorCode {
			get;
			set;
		}
		[JsonProperty("errorDescription")]
		public string ErrorDescription {
			get;
			set;
		}
		[JsonProperty("resource")]
		public string Resource {
			get;
			set;
		}
	}

}

