using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CompanyIOS
{
	public class NewsData
	{
		[JsonProperty("date")]
		public string Date { get; set; }
		[JsonProperty("header")]
		public string Header { get;	set; }
		[JsonProperty("detail")]
		public string Details { get; set; }
		[JsonProperty("textURL")]
		public string TextUrl { get; set; }
	}


	
}

