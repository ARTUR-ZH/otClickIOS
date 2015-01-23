using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CompanyIOS
{
	public class NewsHelper
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
		public Dictionary<int, NewsData> Resource {
			get;
			set;
		}
	}

	public class QuestionHelper
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
		public List<QuestionData> Resource {
			get;
			set;
		}
	}
}

