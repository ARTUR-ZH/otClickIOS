using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CompanyIOS
{
	public class CompaniesResource
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
		public SortedList<nint,string> Resource {
			get;
			set;
		}
	}
}


