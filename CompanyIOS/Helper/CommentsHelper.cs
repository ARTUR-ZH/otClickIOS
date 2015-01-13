using System;
using Newtonsoft.Json;
using System.Collections.Generic;


namespace CompanyIOS
{
	public class CommentsHelper
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
		public List<DataResource> Resource {
			get;
			set;
		}
	}

}

