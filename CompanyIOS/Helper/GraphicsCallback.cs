using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CompanyIOS
{
	public struct GraphicsCallback
	{
		[JsonProperty ("status")]
		public string Status {
			get;
			set;
		}

		[JsonProperty ("error")]
		public ErrosHelper Error {
			get;
			set;
		}

		[JsonProperty ("resource")]
		public Dictionary<string, Dictionary<string, nfloat>> Resource {
			get;
			set;
		}
	}
}

